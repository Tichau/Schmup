// <copyright file="BulletsFactory.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BulletsFactory : MonoBehaviour
{
    private int bulletCount = 0;
    private Queue<Bullet> availableBullets = new Queue<Bullet>();

    [SerializeField]
    private GameObject bulletsPrefab;

    private static BulletsFactory Instance
    {
        get;
        set;
    }

    public static Bullet GetBullet(Vector2 position)
    {
        Bullet bullet = null;
        if (BulletsFactory.Instance.availableBullets.Count > 0)
        {
            bullet = BulletsFactory.Instance.availableBullets.Dequeue();
        }

        if (bullet == null)
        {
            // Instantiate a new bullet.
            GameObject gameObject = (GameObject)GameObject.Instantiate(Instance.bulletsPrefab, position, Quaternion.identity);
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
        bullet.gameObject.SetActive(false);
        BulletsFactory.Instance.availableBullets.Enqueue(bullet);
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

    private void Start()
    {
        if (this.bulletsPrefab == null)
        {
            Debug.LogError("The bullet prefab is not set.");
            return;
        }
    }
}
