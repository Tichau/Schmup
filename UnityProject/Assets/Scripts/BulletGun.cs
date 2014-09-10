// <copyright file="BulletGun.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public abstract class BulletGun : MonoBehaviour
{
    public string WeaponName;

    protected BaseAvatar baseAvatar;

    [SerializeField]
    private float energyConsumedPerBullet;

    [SerializeField]
    private float rateOfFire;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float bulletDamage;

    [SerializeField]
    private BulletType bulletType;

    private float lastFireTime = 0f;

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

    public float EnergyConsumedPerBullet
    {
        get
        {
            return this.energyConsumedPerBullet;
        }

        private set
        {
            this.energyConsumedPerBullet = value;
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

    public virtual bool IsFiring
    {
        get
        {
            if (this.RateOfFire > 0f)
            {
                float durationBetweenTwoBullets = 1f / this.RateOfFire;

                if (Time.time < this.lastFireTime + durationBetweenTwoBullets)
                {
                    // The bullet gun is in cooldown.
                    return true;
                }
            }

            return false;
        }
    }

    protected float GameObjectAngle
    {
        get
        {
            // Warning ! Euler angles are in degree.
            return this.transform.eulerAngles.z * Mathf.Deg2Rad;
        }
    }

    public virtual bool CanFire()
    {
        if (!this.enabled)
        {
            return false;
        }

        if (!this.baseAvatar.CanFire())
        {
            return false;
        }

        if (this.RateOfFire <= 0f)
        {
            // The avatar has a nul rate of fire, The bullet gun can't fire.
            return false;
        }

        if (this.IsFiring)
        {
            // The bullet gun is in cooldown, it can't fire.
            return false;
        }

        // We don't want this anymore because of the rule of energy restoring.
        ////if (this.baseAvatar.Energy < this.EnergyConsumedPerBullet)
        ////{
        ////    // Not enough energy to fire a bullet.
        ////    return false;
        ////}

        return true;
    }

    public virtual void TryToFire()
    {
        if (this.CanFire())
        {
            this.Fire();
        }
    }

    protected virtual void Fire()
    {
        this.baseAvatar.Energy -= this.EnergyConsumedPerBullet;
        this.lastFireTime = Time.time;
    }

    protected virtual void Start()
    {
        this.baseAvatar = this.GetComponent<BaseAvatar>();
        if (this.baseAvatar == null)
        {
            Debug.LogWarning(string.Format("Can't retrieve a base avatar on the gameobject {0}.", this.gameObject.name));
        }
    }
}