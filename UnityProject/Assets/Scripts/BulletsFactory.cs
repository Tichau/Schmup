// <copyright file="BulletsFactory.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BulletsFactory : MonoBehaviour
{
    private int bulletCount = 0;

    private Dictionary<BulletType, Queue<Bullet>> availableBulletsByType = new Dictionary<BulletType, Queue<Bullet>>();

    [SerializeField]
    private GameObject playerBulletPrefab;

    [SerializeField]
    private GameObject enemyBulletPrefab;

    private static BulletsFactory Instance
    {
        get;
        set;
    }

    public static Bullet GetBullet(Vector2 position, BulletType bulletType)
    {
        Queue<Bullet> availableBullets = BulletsFactory.Instance.availableBulletsByType[bulletType];

        Bullet bullet = null;
        if (availableBullets.Count > 0)
        {
            bullet = availableBullets.Dequeue();
        }

        if (bullet == null)
        {
            // Instantiate a new bullet.
            GameObject gameObject = null;

            switch (bulletType)
            {
                case BulletType.EnemyBullet:
                    gameObject = (GameObject)GameObject.Instantiate(Instance.enemyBulletPrefab, position, Quaternion.identity);
                    break;

                    case BulletType.PlayerBullet:
                    gameObject = (GameObject)GameObject.Instantiate(Instance.playerBulletPrefab, position, Quaternion.identity);
                    break;
            }

            gameObject.transform.parent = BulletsFactory.Instance.gameObject.transform;
            bullet = gameObject.GetComponent<Bullet>();
            BulletsFactory.Instance.bulletCount++;
        }

        bullet.Position = position;
        bullet.gameObject.SetActive(true);

        Debug.Log("Number of bullet instantiated = " + BulletsFactory.Instance.bulletCount);

        return bullet;
    }

    public static void ReleaseBullet(Bullet bullet)
    {
        Queue<Bullet> availableBullets = BulletsFactory.Instance.availableBulletsByType[bullet.Type];
        bullet.gameObject.SetActive(false);
        availableBullets.Enqueue(bullet);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is multiple instance of singleton BulletsFactory");
            return;
        }

        Instance = this;

        foreach (object value in System.Enum.GetValues(typeof(BulletType)))
        {
            this.availableBulletsByType.Add((BulletType)value, new Queue<Bullet>());
        }
    }

    private void Start()
    {
        if (this.playerBulletPrefab == null)
        {
            Debug.LogError("A bullet prefab is not set.");
            return;
        }

        if (this.enemyBulletPrefab == null)
        {
            Debug.LogError("A bullet prefab is not set.");
            return;
        }
    }
}
