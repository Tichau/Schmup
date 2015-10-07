// <copyright file="GameManager.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    
    [SerializeField]
    private float rateOfEnemySpawn = 0.2f;

    [SerializeField]
    private float maximumRateOfEnemySpawn = 1f;

    [SerializeField]
    private float rateOfEnemySpawnIncreaseStep = 0.02f;

    [SerializeField]
    private TextAsset levelsDatabase;

    private double lastEnemySpawnTime;

    public GameObject currentLevelRoot;
    private int currentLevelIndex = -1;

    private List<LevelDescription> levelDatabase;

    public static GameManager Instance
    {
        get;
        private set;
    }

    public Level CurrentLevel
    {
        get;
        private set;
    }

    public PlayerAvatar PlayerAvatar
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is multiple instance of singleton GameManager");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Spawn the player.
        GameObject player = (GameObject)GameObject.Instantiate(Instance.playerPrefab, new Vector3(0f, 0f), Quaternion.identity);
        this.PlayerAvatar = player.GetComponent<PlayerAvatar>();
        if (this.PlayerAvatar == null)
        {
            Debug.LogError("Can't retrieve the PlayerAvatar script.");
        }

        this.levelDatabase = XmlHelpers.LoadFromTextAsset<LevelDescription>(this.levelsDatabase);
        if (this.levelDatabase != null && this.levelDatabase.Count > 0)
        {
            this.NextLevel();
        }
    }

    private IEnumerator StartLevel(LevelDescription levelDescription)
    {
        Debug.Log("Start level " + levelDescription.Name);
        
        // Load level scene.
        Application.LoadLevelAdditive(levelDescription.Scene);

        while (this.currentLevelRoot == null)
        {
            yield return null;
            this.currentLevelRoot = GameObject.FindWithTag("LevelRoot");
        }

        // Retrieve level component and initialize it.
        this.CurrentLevel = this.currentLevelRoot.GetComponent<Level>();
        if (this.CurrentLevel == null)
        {
            Debug.Log("Can't retrieve Level component from the level root game object.");
            yield break;
        }

        this.CurrentLevel.Load(levelDescription);
        this.CurrentLevel.Start();
    }
    
    private void Update()
    {
        // this.RandomSpawn();

        this.ExecuteCurrentLevel();
    }

    private void ExecuteCurrentLevel()
    {
        if (this.CurrentLevel == null)
        {
            return;
        }

        if (this.CurrentLevel.IsFinished())
        {
            this.NextLevel();
        }
    }

    private void NextLevel()
    {
        this.ReleaseCurrentLevel();

        this.currentLevelIndex++;

        if (this.currentLevelIndex < 0)
        {
            return;
        }

        if (this.levelDatabase == null)
        {
            Debug.LogWarning("No levels in database");
            return;
        }

        if (this.currentLevelIndex >= this.levelDatabase.Count)
        {
            Debug.Log("No remaining level.");
            return;
        }

        this.StartCoroutine(this.StartLevel(this.levelDatabase[this.currentLevelIndex]));
    }

    private void ReleaseCurrentLevel()
    {
        this.CurrentLevel = null;

        if (this.currentLevelRoot != null)
        {
            GameObject.Destroy(this.currentLevelRoot);
        }

        this.currentLevelRoot = null;
    }

    private void RandomSpawn()
    {
        if (this.rateOfEnemySpawn <= 0f)
        {
            return;
        }

        float durationBetweenTwoEnemySpawn = 1f / this.rateOfEnemySpawn;

        if (Time.time < this.lastEnemySpawnTime + durationBetweenTwoEnemySpawn)
        {
            // The bullet gun is in cooldown, it can't fire.
            return;
        }

        // Spawn an enemy.
        string prefabPath = "Prefabs/Enemy_01";
        if (Random.value < 0.2f)
        {
            prefabPath = "Prefabs/Enemy_02";
        }

        float randomY = Random.Range(-4f, 4f);
        EnemyFactory.GetEnemy(new Vector3(10f, randomY), Quaternion.Euler(0f, 0f, 180f), prefabPath);
        this.lastEnemySpawnTime = Time.time;

        // Up the difficulty.
        this.rateOfEnemySpawn += this.rateOfEnemySpawn > this.maximumRateOfEnemySpawn ? 0f : this.rateOfEnemySpawnIncreaseStep;
    }
}
