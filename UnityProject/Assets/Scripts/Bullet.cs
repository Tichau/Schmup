// <copyright file="Bullet.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    private BulletType type;

    public BulletType Type
    {
        get => this.type;
        private set => this.type = value;
    }

    public float Damage
    {
        get;
        protected set;
    }

    public Vector2 Position
    {
        get => this.transform.position;
        set => this.transform.position = value;
    }

    public virtual void Initialize(Vector2 startDirection, float speed, float damage)
    {
        this.Damage = damage;
    }

    protected virtual void UpdatePosition()
    {
    }

    private void Update()
    {
        this.UpdatePosition();

        // Very simple test if out of bound bullet.
        if (this.Position.x > 20 || this.Position.x < -20 || this.Position.y > 20 || this.Position.y < -20)
        {
            BulletsFactory.ReleaseBullet(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseAvatar avatar = null;
        switch (this.type)
        {
            case BulletType.PlayerSpiralBullet:
            case BulletType.PlayerBullet:
                avatar = other.gameObject.GetComponent<EnemyAvatar>();
                break;

            case BulletType.EnemyBullet:
                avatar = other.gameObject.GetComponent<PlayerAvatar>();
                break;
                
            default:
                Debug.LogError("Unknown bullet type " + this.type);
                break;
        }

        if (avatar != null)
        {
            avatar.TakeDamage(this.Damage);
            BulletsFactory.ReleaseBullet(this);
        }
    }
}
