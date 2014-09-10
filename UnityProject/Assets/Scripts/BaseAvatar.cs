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
    private float energyRegenRate;
    
    [SerializeField]
    private BulletType bulletType;

    private BulletGun[] bulletGuns;

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
        // Energy regen.
        bool isSomeBulletGunFiring = false;
        for (int index = 0; index < this.bulletGuns.Length; index++)
        {
            isSomeBulletGunFiring |= this.bulletGuns[index].IsFiring;
        }

        if (!isSomeBulletGunFiring)
        {
            // The energy regen only if no gun are firing.
            this.Energy += this.EnergyRegenRate * Time.deltaTime;
            this.Energy = Mathf.Clamp(this.Energy, 0f, this.MaximumEnergy);
        }
    }

    private void Start()
    {
        this.HealthPoint = this.MaximumHealthPoint;
        this.Energy = this.MaximumEnergy;

        // Retrieve the bullet guns of the game object.
        this.bulletGuns = this.GetComponents<BulletGun>();

        if (this.bulletGuns == null || this.bulletGuns.Length == 0)
        {
            Debug.LogWarning("There is no bullet guns on the avatar.");
        }
    }
}
