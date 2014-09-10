// <copyright file="BaseAvatar.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class BaseAvatar : MonoBehaviour
{
    [SerializeField]
    private float maximumHealthPoint;

    [SerializeField]
    private float maximumEnergy;

    [SerializeField]
    private float maximumSpeed;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float rateOfFire;

    [SerializeField]
    private float bulletDamage;

    [SerializeField]
    private float energyRegenRate;
    
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

    public float Energy
    {
        get;
        set;
    }

    public float EnergyRegenRate
    {
        get
        {
            return this.energyRegenRate;
        }

        private set
        {
            this.energyRegenRate = value;
        }
    }

    public float MaximumEnergy
    {
        get
        {
            return this.maximumEnergy;
        }

        private set
        {
            this.maximumEnergy = value;
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
        this.Energy += this.EnergyRegenRate * Time.deltaTime;
        this.Energy = Mathf.Clamp(this.Energy, 0f, this.MaximumEnergy);
    }

    private void Start()
    {
        this.HealthPoint = this.MaximumHealthPoint;
        this.Energy = this.MaximumEnergy;
    }
}
