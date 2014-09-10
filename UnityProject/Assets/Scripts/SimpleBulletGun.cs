    // <copyright file="SimpleBulletGun.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class SimpleBulletGun : BulletGun
{
    [SerializeField]
    private bool drawAllowedZoneGizmo;

    [SerializeField]
    private Vector2 bulletSpawnOffsetPosition;

    protected Vector2 BulletSpawnPosition
    {
        get
        {
            return (Vector2)this.transform.position + this.bulletSpawnOffsetPosition;
        }
    }

    protected override void Fire()
    {
        base.Fire();

        Vector2 speed = new Vector2(this.BulletSpeed * Mathf.Cos(this.GameObjectAngle), this.BulletSpeed * Mathf.Sin(this.GameObjectAngle));

        // Fire a bullet !
        Bullet bullet = BulletsFactory.GetBullet(this.BulletSpawnPosition, this.baseAvatar.BulletType);
        bullet.Initialize(speed, this.BulletDamage);
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
