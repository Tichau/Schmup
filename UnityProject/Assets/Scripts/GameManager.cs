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

    private double lastEnemySpawnTime;

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

        this.Score = 0;

        // Kill all enemies before starting new level.
        EnemyAvatar[] enemies = GameObject.FindObjectsOfType<EnemyAvatar>();
        foreach (EnemyAvatar enemy in enemies)
        {
            EnemyFactory.ReleaseEnemy(enemy);
        }

        // Spawn the player.
        var player = (GameObject)GameObject.Instantiate(Instance.playerPrefab, new Vector3(0f, 0f), Quaternion.identity);
        this.PlayerAvatar = player.GetComponent<PlayerAvatar>();
        if (this.PlayerAvatar == null)
        {
            Debug.LogError("Can't retrieve the PlayerAvatar script.");
        }

        this.State = GameState.Playing;

        yield break;
    }
    
    private void Update()
    {
        switch (this.State)
        {
            case GameState.Initializing:
                break;

            case GameState.Playing:
                this.RandomSpawn();

                ////this.ExecuteCurrentLevel();

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
