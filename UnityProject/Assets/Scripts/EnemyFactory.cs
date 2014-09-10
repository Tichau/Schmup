// <copyright file="EnemyFactory.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using System.Collections.Generic;

using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private int enemyCount = 0;

    private Dictionary<EnemyType, Queue<EnemyAvatar>> availableEnemiesByType = new Dictionary<EnemyType, Queue<EnemyAvatar>>();

    [SerializeField]
    private GameObject defaultEnemyPrefab;

    [SerializeField]
    private GameObject tripleGunsEnemyPrefab;

    private static EnemyFactory Instance
    {
        get;
        set;
    }

    public static EnemyAvatar GetEnemy(Vector2 position, Quaternion rotation, EnemyType enemyType)
    {
        Queue<EnemyAvatar> availableEnemies = EnemyFactory.Instance.availableEnemiesByType[enemyType];

        EnemyAvatar enemy = null;
        if (availableEnemies.Count > 0)
        {
            enemy = availableEnemies.Dequeue();
        }

        if (enemy == null)
        {
            // Instantiate a new bullet.
            GameObject gameObject = null;

            switch (enemyType)
            {
                case EnemyType.Default:
                    gameObject = (GameObject)GameObject.Instantiate(Instance.defaultEnemyPrefab, position, rotation);
                    break;

                case EnemyType.TripleGuns:
                    gameObject = (GameObject)GameObject.Instantiate(Instance.tripleGunsEnemyPrefab, position, rotation);
                    break;
            }

            gameObject.transform.parent = EnemyFactory.Instance.gameObject.transform;
            enemy = gameObject.GetComponent<EnemyAvatar>();
            EnemyFactory.Instance.enemyCount++;
            Debug.Log("Number of enemies instantiated = " + EnemyFactory.Instance.enemyCount);
        }

        enemy.Position = position;
        enemy.gameObject.SetActive(true);

        return enemy;
    }

    public static void ReleaseBullet(EnemyAvatar enemyAvatar)
    {
        Queue<EnemyAvatar> availableEnemies = EnemyFactory.Instance.availableEnemiesByType[enemyAvatar.Type];
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

        foreach (object value in System.Enum.GetValues(typeof(EnemyType)))
        {
            this.availableEnemiesByType.Add((EnemyType)value, new Queue<EnemyAvatar>());
        }
    }

    private void Start()
    {
        if (this.defaultEnemyPrefab == null)
        {
            Debug.LogError("An enemy prefab is not set.");
            return;
        }
    }
}
