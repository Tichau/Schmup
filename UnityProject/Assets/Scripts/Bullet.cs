// <copyright file="Bullet.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private BulletType type;

    public BulletType Type
    {
        get
        {
            return this.type;
        }

        private set
        {
            this.type = value;
        }
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

    public void Initialize(Vector2 speed, float damage)
    {
        this.Speed = speed;
        this.Damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseAvatar avatar = null;
        switch (this.type)
        {
            case BulletType.PlayerBullet:
                avatar = other.gameObject.GetComponent<EnemyAvatar>();
                break;

            case BulletType.EnemyBullet:
                avatar = other.gameObject.GetComponent<PlayerAvatar>();
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
