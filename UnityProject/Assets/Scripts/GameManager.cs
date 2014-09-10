﻿// <copyright file="GameManager.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    
    [SerializeField]
    private float rateOfEnemySpawn = 0.2f;

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
    }
    
    private void Update()
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
        EnemyType enemyType = EnemyType.Default;
        if (Random.value < 0.2f)
        {
            enemyType = EnemyType.TripleGuns;
        }

        float randomY = Random.Range(-4f, 4f);
        EnemyFactory.GetEnemy(new Vector3(10f, randomY), Quaternion.Euler(0f, 0f, 180f), enemyType);
        this.lastEnemySpawnTime = Time.time;

        // Up the difficulty.
        this.rateOfEnemySpawn += this.rateOfEnemySpawn > 1f ? 0f : 0.02f;
    }
}
