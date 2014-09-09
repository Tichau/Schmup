// <copyright file="BulletGun.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class BulletGun : MonoBehaviour
{
    [SerializeField]
    private Vector2 bulletSpawnOffsetPosition;

    [SerializeField]
    private bool drawAllowedZoneGizmo;

    private BaseAvatar baseAvatar;

    private float lastFireTime = 0f;

    private Vector2 BulletSpawnPosition
    {
        get
        {
            return (Vector2)this.transform.position + this.bulletSpawnOffsetPosition;
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

        // Fire a bullet !
        Bullet bullet = BulletsFactory.GetBullet(this.BulletSpawnPosition);
        bullet.Speed = new Vector2(this.baseAvatar.BulletSpeed, 0f);
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
