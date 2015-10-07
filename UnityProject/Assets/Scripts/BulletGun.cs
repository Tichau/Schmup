// <copyright file="BulletGun.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class BulletGun : MonoBehaviour
{
    protected BaseAvatar baseAvatar;

    [SerializeField]
    private string weaponName;

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

    [SerializeField]
    private bool drawCanonsGizmo;

    [SerializeField]
    private CanonDescription[] canonDescriptions;

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

    public string WeaponName
    {
        get
        {
            return weaponName;
        }

        private set
        {
            weaponName = value;
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

    public virtual void TryToFire()
    {
        if (this.CanFire())
        {
            this.Fire();
        }
    }

    protected virtual bool CanFire()
    {
        if (!this.enabled)
        {
            return false;
        }

        if (this.baseAvatar != null && !this.baseAvatar.CanFire())
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

    protected Vector2 GetBulletSpawnPosition(int canonIndex)
    {
        Vector2 worldOffset = (this.transform.localToWorldMatrix * this.canonDescriptions[canonIndex].BulletSpawnOffsetPosition);
        return (Vector2)this.transform.position + worldOffset;
    }

    protected float GetBulletSpawnAngle(int canonIndex)
    {
        return this.GameObjectAngle + (this.canonDescriptions[canonIndex].BulletSpawnOffsetAngle * Mathf.Deg2Rad);
    }

    protected virtual void Fire()
    {
        if (this.baseAvatar != null)
        {
            this.baseAvatar.Energy -= this.EnergyConsumedPerBullet;
        }

        this.lastFireTime = Time.time;

        for (int index = 0; index < this.canonDescriptions.Length; index++)
        {
            float bulletSpawnAngle = this.GetBulletSpawnAngle(index);
            Vector2 direction = new Vector2(Mathf.Cos(bulletSpawnAngle), Mathf.Sin(bulletSpawnAngle));

            // Fire a bullet !
            Bullet bullet = BulletsFactory.GetBullet(this.GetBulletSpawnPosition(index), this.BulletType);
            bullet.Initialize(direction, this.BulletSpeed, this.BulletDamage);
        }
    }

    protected virtual void Start()
    {
        this.baseAvatar = this.GetComponent<BaseAvatar>();
        if (this.baseAvatar == null)
        {
            Debug.LogWarning(string.Format("Can't retrieve a base avatar on the gameobject {0}.", this.gameObject.name));
        }
    }

    protected void OnDrawGizmos()
    {
        if (this.drawCanonsGizmo)
        {
            for (int index = 0; index < this.canonDescriptions.Length; index++)
            {
                float bulletSpawnAngle = this.GetBulletSpawnAngle(index);
                Vector2 speed = new Vector2(this.BulletSpeed * Mathf.Cos(bulletSpawnAngle), this.BulletSpeed * Mathf.Sin(bulletSpawnAngle));

                Debug.DrawLine(this.GetBulletSpawnPosition(index), this.GetBulletSpawnPosition(index) + speed, Color.red);
            }
        }
    }
}