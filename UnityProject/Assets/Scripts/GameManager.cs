// <copyright file="GameManager.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Data;
using Random = UnityEngine.Random;

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

    private Level currentLevel;
    private int currentLevelIndex = -1;

    private List<LevelDescription> levelDatabase;

    public event System.EventHandler<LevelChangedEventArgs> LevelChanged;

    public static GameManager Instance
    {
        get;
        private set;
    }

    public PlayerAvatar PlayerAvatar
    {
        get;
        private set;
    }

    public GameState State
    {
        get;
        private set;
    }

    public int Score
    {
        get;
        private set;
    }

    public int BestScore
    {
        get;
        private set;
    }

    public void AddScoreGain(int gain)
    {
        this.Score += gain;
        if (this.Score > this.BestScore)
        {
            this.BestScore = this.Score;
            PlayerPrefs.SetInt("BestScore", this.BestScore);
        }
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

    private IEnumerator Start()
    {
        this.BestScore = PlayerPrefs.GetInt("BestScore", 0);

        yield return this.StartCoroutine(this.StartNewGame());
    }

    private IEnumerator StartNewGame()
    {
        // Reset game if needed.
        if (this.PlayerAvatar != null)
        {
            GameObject.Destroy(this.PlayerAvatar.gameObject);
            this.PlayerAvatar = null;
        }

        if (this.currentLevel != null)
        {
            this.StartCoroutine(this.currentLevel.Unload());
            this.currentLevel = null;
        }

        this.currentLevelIndex = -1;
        this.Score = 0;

        // TODO: Kill all enemies.
        
        // Spawn the player.
        var player = (GameObject)GameObject.Instantiate(Instance.playerPrefab, new Vector3(0f, 0f), Quaternion.identity);
        this.PlayerAvatar = player.GetComponent<PlayerAvatar>();
        if (this.PlayerAvatar == null)
        {
            Debug.LogError("Can't retrieve the PlayerAvatar script.");
        }

        this.levelDatabase = XmlHelpers.DeserializeDatabaseFromXML<LevelDescription>(this.levelsDatabase);
        if (this.levelDatabase != null && this.levelDatabase.Count > 0)
        {
            yield return this.StartCoroutine(this.NextLevel());
        }

        this.State = GameState.Playing;
    }
    
    private void Update()
    {
        switch (this.State)
        {
            case GameState.Initializing:
                break;

            case GameState.Playing:
                //// this.RandomSpawn();

                this.ExecuteCurrentLevel();

                if (this.PlayerAvatar.IsDead)
                {
                    this.State = GameState.Dead;
                }

                break;

            case GameState.Dead:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Start a new game.
                    this.State = GameState.Initializing;

                    this.StartCoroutine(this.StartNewGame());
                }
                break;
        }
    }

    private IEnumerator NextLevel()
    {
        // Release current level.
        Level level = this.currentLevel;
        if (level != null)
        {
            this.currentLevel = null;
            yield return this.StartCoroutine(level.Unload());
        }

        // Get next level description.
        this.currentLevelIndex++;

        Debug.Assert(this.currentLevelIndex >= 0 );

        if (this.levelDatabase == null)
        {
            Debug.LogWarning("No levels in database");
            yield break;
        }

        if (this.currentLevelIndex >= this.levelDatabase.Count)
        {
            Debug.Log("No remaining level.");
            yield break;
        }

        LevelDescription levelDescription = this.levelDatabase[this.currentLevelIndex];
        
        // Load next level.
        Debug.Log("Start level " + levelDescription.Name);

        level = new Level();

        yield return this.StartCoroutine(level.Load(levelDescription));

        if (this.LevelChanged != null)
        {
            this.LevelChanged.Invoke(this, new LevelChangedEventArgs(level));
        }

        level.Start();
        this.currentLevel = level;
    }

    private void ExecuteCurrentLevel()
    {
        if (this.currentLevel == null)
        {
            return;
        }

        this.currentLevel.Execute();

        if (this.currentLevel.IsFinished())
        {
            this.StartCoroutine(this.NextLevel());
        }
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
        EnemyFactory.GetEnemy(new Vector3(10f, randomY), Quaternion.Euler(0f, 0f, 0f), prefabPath);
        this.lastEnemySpawnTime = Time.time;

        // Up the difficulty.
        this.rateOfEnemySpawn += this.rateOfEnemySpawn > this.maximumRateOfEnemySpawn ? 0f : this.rateOfEnemySpawnIncreaseStep;
    }
}
