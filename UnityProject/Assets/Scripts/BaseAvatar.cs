// <copyright file="BaseAvatar.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using System;

using UnityEngine;

public class BaseAvatar : MonoBehaviour
{
    protected BulletGun[] BulletGuns;

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
        get => this.maximumHealthPoint;
        private set => this.maximumHealthPoint = value;
    }

    public float Energy
    {
        get => this.energy;

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
        get => this.energyRegenRate;
        private set => this.energyRegenRate = value;
    }

    public float MaximumEnergy
    {
        get => this.maximumEnergy;
        private set => this.maximumEnergy = value;
    }

    public float MaximumSpeed
    {
        get => this.maximumSpeed;
        private set => this.maximumSpeed = value;
    }

    public float DamageDealthAtCollision
    {
        get => this.damageDealthAtCollision;
        private set => this.damageDealthAtCollision = value;
    }

    //// Properties.
    public Vector2 Position
    {
        get => this.transform.position;
        set => this.transform.position = value;
    }

    public bool IsEnergyRestoring
    {
        get;
        private set;
    }

    public virtual void Reset()
    {
        this.HealthPoint = this.MaximumHealthPoint;
        this.Energy = this.MaximumEnergy;
    }

    public virtual void TakeDamage(float damage)
    {
        if (this.HealthPoint <= 0)
        {
            return; // Already dead.
        }
        
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
        this.Reset();

        // Retrieve the bullet guns of the game object.
        this.BulletGuns = this.GetComponents<BulletGun>();

        if (this.BulletGuns == null || this.BulletGuns.Length == 0)
        {
            Debug.LogWarning("There is no bullet guns on the avatar.");
        }
    }

    protected virtual void Update()
    {
        // Energy regen.
        bool isSomeBulletGunFiring = false;
        for (int index = 0; index < this.BulletGuns.Length; index++)
        {
            isSomeBulletGunFiring |= this.BulletGuns[index].IsFiring;
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
