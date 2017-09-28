// <copyright file="EnemyFactory.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using System.Collections.Generic;

using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private int enemyCount = 0;

    private Dictionary<string, Queue<EnemyAvatar>> availableEnemiesByType = new Dictionary<string, Queue<EnemyAvatar>>();
    
    private static EnemyFactory Instance
    {
        get;
        set;
    }

    public static EnemyAvatar GetEnemy(Vector2 position, Quaternion rotation, string prefabPath)
    {
        if (!EnemyFactory.Instance.availableEnemiesByType.ContainsKey(prefabPath))
        {
            EnemyFactory.Instance.availableEnemiesByType.Add(prefabPath, new Queue<EnemyAvatar>());
        }

        Queue<EnemyAvatar> availableEnemies = EnemyFactory.Instance.availableEnemiesByType[prefabPath];

        EnemyAvatar enemy = null;
        if (availableEnemies.Count > 0)
        {
            enemy = availableEnemies.Dequeue();
        }

        if (enemy == null)
        {
            // Instantiate a new bullet.
            GameObject gameObject = null;

            GameObject prefab = (GameObject)Resources.Load(prefabPath);
            gameObject = (GameObject)GameObject.Instantiate(prefab, position, rotation);

            gameObject.transform.parent = EnemyFactory.Instance.gameObject.transform;
            enemy = gameObject.GetComponent<EnemyAvatar>();
            enemy.PrefabPath = prefabPath;
            EnemyFactory.Instance.enemyCount++;
            Debug.Log("Number of enemies instantiated = " + EnemyFactory.Instance.enemyCount);
        }

        enemy.Position = position;
        enemy.gameObject.SetActive(true);

        return enemy;
    }

    public static void ReleaseEnemy(EnemyAvatar enemyAvatar)
    {
        Queue<EnemyAvatar> availableEnemies = EnemyFactory.Instance.availableEnemiesByType[enemyAvatar.PrefabPath];
        enemyAvatar.gameObject.SetActive(false);
        availableEnemies.Enqueue(enemyAvatar);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is multiple instance of singleton BulletsFactory");
            return;
        }

        Instance = this;
    }
}
