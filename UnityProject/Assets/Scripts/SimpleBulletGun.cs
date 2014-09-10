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

        Vector2 direction = new Vector2(Mathf.Cos(this.GameObjectAngle), Mathf.Sin(this.GameObjectAngle));

        // Fire a bullet !
        Bullet bullet = BulletsFactory.GetBullet(this.BulletSpawnPosition, this.BulletType);
        bullet.Initialize(direction, this.BulletSpeed, this.BulletDamage);
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
