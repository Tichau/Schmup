// <copyright file="BulletsFactory.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using System.Collections.Generic;

using UnityEngine;

public class BulletsFactory : MonoBehaviour
{
    private int bulletCount = 0;

    private Dictionary<BulletType, Queue<Bullet>> availableBulletsByType = new Dictionary<BulletType, Queue<Bullet>>();

    [SerializeField]
    private GameObject playerBulletPrefab;

    [SerializeField]
    private int numberOfPlayerBulletToPreinstantiate;

    [SerializeField]
    private GameObject enemyBulletPrefab;

    [SerializeField]
    private int numberOfEnemyBulletToPreinstantiate;

    [SerializeField]
    private GameObject playerSpiralBulletPrefab;

    [SerializeField]
    private int numberOfPlayerSpiralBulletToPreinstantiate;

    private static BulletsFactory Instance
    {
        get;
        set;
    }

#if UNITY_EDITOR
    public static int Debug_BulletCount => BulletsFactory.Instance.bulletCount;
#endif

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
            bullet = InstantiateBullet(bulletType);
            Debug.Log("Number of bullet instantiated = " + BulletsFactory.Instance.bulletCount + "\n" + bulletType.ToString());
        }

        bullet.Position = position;
        bullet.gameObject.SetActive(true);

        return bullet;
    }

    public static void ReleaseBullet(Bullet bullet)
    {
        Queue<Bullet> availableBullets = BulletsFactory.Instance.availableBulletsByType[bullet.Type];
        bullet.gameObject.SetActive(false);
        availableBullets.Enqueue(bullet);
    }

    private static void PreinstantiateBullets(BulletType bulletType, int numberOfBulletsToPreinstantiate)
    {
        Queue<Bullet> bullets = BulletsFactory.Instance.availableBulletsByType[bulletType];
        for (int index = 0; index < numberOfBulletsToPreinstantiate; index++)
        {
            Bullet bullet = InstantiateBullet(bulletType);
            if (bullet == null)
            {
                Debug.LogError(string.Format("Failed to instantiate {0} bullets.", numberOfBulletsToPreinstantiate));
                break;
            }

            bullets.Enqueue(bullet);
        }
    }

    private static Bullet InstantiateBullet(BulletType bulletType)
    {
        GameObject gameObject = null;

        switch (bulletType)
        {
            case BulletType.EnemyBullet:
                gameObject = (GameObject)GameObject.Instantiate(Instance.enemyBulletPrefab);
                break;

            case BulletType.PlayerBullet:
                gameObject = (GameObject)GameObject.Instantiate(Instance.playerBulletPrefab);
                break;

            case BulletType.PlayerSpiralBullet:
                gameObject = (GameObject)GameObject.Instantiate(Instance.playerSpiralBulletPrefab);
                break;
        }

        gameObject.SetActive(false);
        gameObject.transform.parent = BulletsFactory.Instance.gameObject.transform;
        Bullet bullet = gameObject.GetComponent<Bullet>();
        BulletsFactory.Instance.bulletCount++;
        return bullet;
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

        PreinstantiateBullets(BulletType.PlayerBullet, this.numberOfPlayerBulletToPreinstantiate);
        PreinstantiateBullets(BulletType.EnemyBullet, this.numberOfEnemyBulletToPreinstantiate);
        PreinstantiateBullets(BulletType.PlayerSpiralBullet, this.numberOfPlayerSpiralBulletToPreinstantiate);
    }
}
