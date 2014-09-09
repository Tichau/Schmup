// <copyright file="Bullet.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Speed
    {
        get;
        set;
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
