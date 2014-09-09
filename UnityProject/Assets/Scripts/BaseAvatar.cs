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

    public int HealthPoint
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

    private void Start()
    {
        this.HealthPoint = this.maximumHealthPoint;
    }
    
    private void Update()
    {
    }
}
