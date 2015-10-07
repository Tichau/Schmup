// <copyright file="Level.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class Level : UnityEngine.MonoBehaviour
{
    private LevelDescription description;
    private EnemyState[] isEnemySpawned;
    private float currentLevelStartDate;

    public enum EnemyState
    {
        NotSpawned,
        Spawned,
    }

    public string Name
    {
        get
        {
            return this.description.Name;
        }
    }
    
    public void Load(LevelDescription description)
    {
        this.description = description;
        this.isEnemySpawned = new EnemyState[this.description.Enemies != null ? this.description.Enemies.Length : 0];
        for (int index = 0; index < this.isEnemySpawned.Length; index++)
        {
            this.isEnemySpawned[index] = EnemyState.NotSpawned;
        }
    }
    
    public void Release()
    {
        this.description = null;
        this.isEnemySpawned = null;
    }
    
    public void Start()
    {
        this.currentLevelStartDate = Time.time; 
    }

    public bool IsFinished()
    {
        float timePassedSinceBeginning = Time.time - this.currentLevelStartDate;
        if (timePassedSinceBeginning >= this.description.Duration)
        {
            return true;
        }

        return false;
    }

    public void Update()
    {
        float timePassedSinceBeginning = Time.time - this.currentLevelStartDate;
        
        if (this.description == null)
        {
            return;
        }
        
        if (this.description.Enemies == null)
        {
            return;
        }

        for (int index = 0; index < this.description.Enemies.Length; index++)
        {
            EnemyDescription enemyDescription = this.description.Enemies[index];
            if (this.isEnemySpawned[index] == EnemyState.Spawned)
            {
                continue;
            }

            if (timePassedSinceBeginning < enemyDescription.SpawnDate)
            {
                continue;
            }

            // Spawn !
            EnemyFactory.GetEnemy(enemyDescription.SpawnPosition, Quaternion.Euler(0f, 0f, 180f), enemyDescription.PrefabPath);
            this.isEnemySpawned[index] = EnemyState.Spawned;
        }
    }
}
