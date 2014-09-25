// <copyright file="Level.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using System.Collections;

using UnityEngine;

public class Level
{
    private LevelDescription description;
    private EnemyState[] isEnemySpawned;

    public Level(LevelDescription description)
    {
        this.description = description;
        this.isEnemySpawned = new EnemyState[this.description.Enemies != null ? this.description.Enemies.Length : 0];
        for (int index = 0; index < this.isEnemySpawned.Length; index++)
        {
            this.isEnemySpawned[index] = EnemyState.NotSpawned;
        }
    }

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

    public bool IsFinished(float timePassedSinceBeginning)
    {
        if (timePassedSinceBeginning >= this.description.Duration)
        {
            return true;
        }

        return false;
    }

    public void Update(float timePassedSinceBeginning)
    {
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
