// <copyright file="EnemyAvatar.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class EnemyAvatar : BaseAvatar
{
    [SerializeField]
    private EnemyType type;

    public EnemyType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    protected override void Die()
    {
        EnemyFactory.ReleaseBullet(this);
    }

    protected override void Update()
    {
        base.Update();

        // Very simple test if out of bound bullet.
        if (this.Position.x > 14 || this.Position.x < -14 || this.Position.y > 20 || this.Position.y < -20)
        {
            EnemyFactory.ReleaseBullet(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseAvatar avatar = avatar = other.gameObject.GetComponent<BaseAvatar>();
        Debug.Log(this.name);
        if (avatar != null)
        {
            avatar.TakeDamage(this.DamageDealthAtCollision);
            this.TakeDamage(avatar.DamageDealthAtCollision);
        }
    }
}
