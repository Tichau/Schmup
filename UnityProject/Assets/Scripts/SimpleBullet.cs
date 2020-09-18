// <copyright file="SimpleBullet.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

public class SimpleBullet : Bullet
{
    protected Vector2 Speed;

    public override void Initialize(Vector2 startDirection, float speed, float damage)
    {
        base.Initialize(startDirection, speed, damage);

        this.Speed = startDirection * speed;
    }

    protected override void UpdatePosition()
    {
        base.UpdatePosition();

        this.Position += this.Speed * Time.deltaTime;
    }
}
