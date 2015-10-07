// <copyright file="BaseAvatar.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using System;

using UnityEngine;

public class BaseAvatar : MonoBehaviour
{
    protected BulletGun[] bulletGuns;

    private const float EnergyEpsilon = 1f;

    [SerializeField]
    private float maximumHealthPoint;

    [SerializeField]
    private float maximumEnergy;

    [SerializeField]
    private float maximumSpeed;

    [SerializeField]
    private float energyRegenRate;

    [SerializeField]
    private float damageDealthAtCollision;
    
    [SerializeField]
    private float energyRegenEfficiencyDuringRestoringProcess;

    private float energyRegenEfficiency = 1f;
    private float energy;

    public event EventHandler<DamageTakenEventArgs> OnDamageTaken;

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
        get
        {
            return this.energy;
        }

        set
        {
            this.energy = value;
            if (this.energy < BaseAvatar.EnergyEpsilon)
            {
                this.StartEnergyRestoringProcess();
            }
        }
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

    public float DamageDealthAtCollision
    {
        get
        {
            return this.damageDealthAtCollision;
        }

        private set
        {
            this.damageDealthAtCollision = value;
        }
    }

    //// Properties.
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

    public bool IsEnergyRestoring
    {
        get;
        private set;
    }

    public void TakeDamage(float damage)
    {
        if (this.OnDamageTaken != null && damage > 0)
        {
            this.OnDamageTaken.Invoke(this, new DamageTakenEventArgs(damage));
        }

        this.HealthPoint -= damage;

        if (this.HealthPoint <= 0f)
        {
            this.Die();
        }
    }

    public bool CanFire()
    {
        if (this.IsEnergyRestoring)
        {
            return false;
        }

        return true;
    }

    protected virtual void Die()
    {
        this.Invoke("Release", 0.3f);
    }

    protected virtual void Release()
    {
        GameObject.Destroy(this.gameObject);
    }

    protected virtual void Start()
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
            this.Energy += this.EnergyRegenRate * this.energyRegenEfficiency * Time.deltaTime;
            this.Energy = Mathf.Clamp(this.Energy, 0f, this.MaximumEnergy);

            if (this.IsEnergyRestoring && Math.Abs(this.Energy - this.MaximumEnergy) < BaseAvatar.EnergyEpsilon)
            {
                this.EndEnergyRestoringProcess();
            }
        }
    }

    private void StartEnergyRestoringProcess()
    {
        this.IsEnergyRestoring = true;
        this.energyRegenEfficiency = this.energyRegenEfficiencyDuringRestoringProcess;
    }

    private void EndEnergyRestoringProcess()
    {
        this.IsEnergyRestoring = false;
        this.energyRegenEfficiency = 1f;
    }
}