// <copyright file="Level.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using System.Collections;
using UnityEngine;

using Data;
using UnityEngine.SceneManagement;

public class Level
{
    private LevelDescription description;
    private EnemyState[] isEnemySpawned;
    private float currentLevelStartDate = -1;

    public enum EnemyState
    {
        NotSpawned,
        Spawned,
    }

    public string Name => this.description.Name;

    public bool IsLevelStarted => this.currentLevelStartDate >= 0f;

    public IEnumerator Load(LevelDescription levelDescription)
    {
        this.description = levelDescription;
        Debug.Assert(this.description != null);

        // Load level scene.
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(this.description.Scene, LoadSceneMode.Additive);

        while (!loadSceneAsync.isDone)
        {
            // Wait for the level to be loaded.
            yield return null;
        }

        this.isEnemySpawned = new EnemyState[this.description.Enemies != null ? this.description.Enemies.Length : 0];
        for (int index = 0; index < this.isEnemySpawned.Length; index++)
        {
            this.isEnemySpawned[index] = EnemyState.NotSpawned;
        }
    }
    
    public IEnumerator Unload()
    {
        AsyncOperation unloadSceneAsync = SceneManager.UnloadSceneAsync(this.description.Scene);
        while (!unloadSceneAsync.isDone)
        {
            // Wait for the level to be unloaded.
            yield return null;
        }

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

    public void Execute()
    {
        if (!this.IsLevelStarted)
        {
            return;
        }

        float timePassedSinceBeginning = Time.time - this.currentLevelStartDate;
        
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
            EnemyFactory.GetEnemy(enemyDescription.SpawnPosition, Quaternion.Euler(0f, 0f, 0f), enemyDescription.PrefabPath);
            this.isEnemySpawned[index] = EnemyState.Spawned;
        }
    }
}
