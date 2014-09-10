// <copyright file="BulletGun.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class BulletGun : MonoBehaviour
{
    [SerializeField]
    private Vector2 bulletSpawnOffsetPosition;

    [SerializeField]
    private bool drawAllowedZoneGizmo;

    [SerializeField]
    private float energyConsumedPerBullet;

    private BaseAvatar baseAvatar;

    private float lastFireTime = 0f;

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

    private Vector2 BulletSpawnPosition
    {
        get
        {
            return (Vector2)this.transform.position + this.bulletSpawnOffsetPosition;
        }
    }

    private float Angle
    {
        get
        {
            // Warning ! Euler angles are in degree.
            return this.transform.eulerAngles.z * Mathf.Deg2Rad;
        }
    }

    public void Fire()
    {
        if (this.baseAvatar.RateOfFire <= 0f)
        {
            // The avatar has a nul rate of fire, The bullet gun can't fire.
            return;
        }

        float durationBetweenTwoBullets = 1f / this.baseAvatar.RateOfFire;

        if (Time.time < this.lastFireTime + durationBetweenTwoBullets)
        {
            // The bullet gun is in cooldown, it can't fire.
            return;
        }

        if (this.baseAvatar.Energy < this.EnergyConsumedPerBullet)
        {
            // Not enough energy to fire a bullet.
            return;
        }

        this.baseAvatar.Energy -= this.EnergyConsumedPerBullet;

        Vector2 speed = new Vector2(this.baseAvatar.BulletSpeed * Mathf.Cos(this.Angle), this.baseAvatar.BulletSpeed * Mathf.Sin(this.Angle));

        // Fire a bullet !
        Bullet bullet = BulletsFactory.GetBullet(this.BulletSpawnPosition, this.baseAvatar.BulletType);
        bullet.Initialize(speed, this.baseAvatar.BulletDamage);
        this.lastFireTime = Time.time;
    }

    private void Start()
    {
        this.baseAvatar = this.GetComponent<BaseAvatar>();
        if (this.baseAvatar == null)
        {
            Debug.LogWarning(string.Format("Can't retrieve a base avatar on the gameobject {0}.", this.gameObject.name));
        }
    }
    
    private void Update()
    {
#if UNITY_EDITOR
        if (this.drawAllowedZoneGizmo)
        {
            Debug.DrawLine(this.BulletSpawnPosition, this.BulletSpawnPosition + new Vector2(0.1f, 0f), Color.red);
        }
#endif
    }
}
