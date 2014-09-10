// <copyright file="MultipleBulletGun.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class MultipleBulletGun : BulletGun
{
    [SerializeField]
    private bool drawAllowedZoneGizmo;

    [SerializeField]
    private CanonDescription[] canonDescriptions;

    protected Vector2 GetBulletSpawnPosition(int canonIndex)
    {
        return (Vector2)this.transform.position + this.canonDescriptions[canonIndex].BulletSpawnOffsetPosition;
    }

    protected float GetBulletSpawnAngle(int canonIndex)
    {
        return this.GameObjectAngle + this.canonDescriptions[canonIndex].BulletSpawnOffsetAngle * Mathf.Deg2Rad;
    }

    protected override void Fire()
    {
        base.Fire();

        for (int index = 0; index < this.canonDescriptions.Length; index++)
        {
            float bulletSpawnAngle = this.GetBulletSpawnAngle(index);
            Vector2 direction = new Vector2(Mathf.Cos(bulletSpawnAngle), Mathf.Sin(bulletSpawnAngle));

            // Fire a bullet !
            Bullet bullet = BulletsFactory.GetBullet(this.GetBulletSpawnPosition(index), this.BulletType);
            bullet.Initialize(direction, this.BulletSpeed, this.BulletDamage);
        }
    }
    
    protected void Update()
    {
#if UNITY_EDITOR
        if (this.drawAllowedZoneGizmo)
        {
            for (int index = 0; index < this.canonDescriptions.Length; index++)
            {
                float bulletSpawnAngle = this.GetBulletSpawnAngle(index);
                Vector2 speed = new Vector2(this.BulletSpeed * Mathf.Cos(bulletSpawnAngle), this.BulletSpeed * Mathf.Sin(bulletSpawnAngle));

                Debug.DrawLine(this.GetBulletSpawnPosition(index), this.GetBulletSpawnPosition(index) + speed, Color.red);
            }
        }
#endif
    }
}
