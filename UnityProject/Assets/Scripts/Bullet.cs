// <copyright file="Bullet.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        DamageThePlayer,
        DamageTheEnemies,
        DamageBoth,
    }

    public BulletType Type
    {
        get;
        private set;
    }

    public float Damage
    {
        get;
        private set;
    }

    public Vector2 Speed
    {
        get;
        private set;
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

    public void Initialize(Vector2 speed, float damage, BulletType bulletType)
    {
        this.Speed = speed;
        this.Damage = damage;
        this.Type = bulletType;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseAvatar avatar = null;
        switch (this.Type)
        {
            case BulletType.DamageTheEnemies:
                avatar = other.gameObject.GetComponent<EnemyAvatar>();
                break;

            case BulletType.DamageThePlayer:
                avatar = other.gameObject.GetComponent<PlayerAvatar>();
                break;

            case BulletType.DamageBoth:
                avatar = other.gameObject.GetComponent<BaseAvatar>();
                break;
        }

        if (avatar != null)
        {
            avatar.TakeDamage(this.Damage);
            BulletsFactory.ReleaseBullet(this);
        }
    }

    private void Start()
    {
    }
    
    private void Update()
    {
        this.Position += this.Speed * Time.deltaTime;

        // Very simple test if out of bound bullet.
        if (this.Position.x > 20 || this.Position.x < -20 || this.Position.y > 20 || this.Position.y < -20)
        {
            BulletsFactory.ReleaseBullet(this);
        }
    }
}
