// <copyright file="BaseAvatar.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class BaseAvatar : MonoBehaviour
{
    [SerializeField]
    private float maximumHealthPoint;

    [SerializeField]
    private float maximumSpeed;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float rateOfFire;

    [SerializeField]
    private float bulletDamage;

    [SerializeField]
    private BulletType bulletType;

    //// Statistics.
    public float HealthPoint
    {
        get; 
        private set;
    }

    public float MaximumHealthPoint
    {
        get
        {
            return this.maximumHealthPoint;
        }

        private set
        {
            this.maximumHealthPoint = value;
        }
    }

    public float MaximumSpeed
    {
        get
        {
            return this.maximumSpeed;
        }

        private set
        {
            this.maximumSpeed = value;
        }
    }

    public float BulletSpeed
    {
        get
        {
            return this.bulletSpeed;
        }

        private set
        {
            this.bulletSpeed = value;
        }
    }

    public float RateOfFire
    {
        get
        {
            return this.rateOfFire;
        }

        private set
        {
            this.rateOfFire = value;
        }
    }

    public float BulletDamage
    {
        get
        {
            return this.bulletDamage;
        }

        private set
        {
            this.bulletDamage = value;
        }
    }

    //// Properties.
    public BulletType BulletType
    {
        get
        {
            return bulletType;
        }

        private set
        {
            bulletType = value;
        }
    }

    public Vector2 Position
    {
        get
        {
            return this.transform.position;
        }

        set
        {
            this.transform.position = value;
        }
    }

    public void TakeDamage(float damage)
    {
        this.HealthPoint -= damage;

        if (this.HealthPoint <= 0f)
        {
            this.Die();
        }
    }

    protected virtual void Die()
    {
        GameObject.Destroy(this.gameObject);
    }

    protected virtual void Update()
    {
    }

    private void Start()
    {
        this.HealthPoint = this.maximumHealthPoint;
    }
}
