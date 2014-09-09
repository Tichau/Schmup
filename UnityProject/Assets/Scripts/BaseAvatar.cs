// <copyright file="BaseAvatar.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class BaseAvatar : MonoBehaviour
{
    [SerializeField]
    private int maximumHealthPoint;

    [SerializeField]
    private float maximumSpeed;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private int rateOfFire;

    [SerializeField]
    private float bulletDamage;

    public float HealthPoint
    {
        get; 
        private set;
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


    public int RateOfFire
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

    public void TakeDamage(float damage)
    {
        this.HealthPoint -= damage;

        if (this.HealthPoint <= 0f)
        {
            this.Die();
        }
    }

    private void Die()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void Start()
    {
        this.HealthPoint = this.maximumHealthPoint;
    }
    
    private void Update()
    {
    }
}
