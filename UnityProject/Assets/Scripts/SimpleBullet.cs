// <copyright file="SimpleBullet.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class SimpleBullet : Bullet
{
    protected Vector2 speed;

    public override void Initialize(Vector2 startDirection, float speed, float damage)
    {
        base.Initialize(startDirection, speed, damage);

        this.speed = startDirection * speed;
    }

    protected override void UpdatePosition()
    {
        base.UpdatePosition();

        this.Position += this.speed * Time.deltaTime;
    }
}
