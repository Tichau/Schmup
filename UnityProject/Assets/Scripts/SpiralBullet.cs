// <copyright file="SpiralBullet.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

public class SpiralBullet : Bullet
{
    /// <summary>
    /// Set the distance between successive turnings.
    /// </summary>
    [SerializeField]
    private float distanceBeetweenSuccessiveTurnings;

    /// <summary>
    /// Pulsation in radian per seconds.
    /// </summary>
    private float pulsation;

    private float startTime;
    private Vector2 startPosition;

    private float startAngleCosinus;
    private float startAngleSinus;

    public override void Initialize(Vector2 startDirection, float speed, float damage)
    {
        base.Initialize(startDirection, speed, damage);

        // Compute the start angle parameters.
        float startAngle = Mathf.Atan2(startDirection.y, startDirection.x);
        this.startAngleCosinus = Mathf.Cos(startAngle);
        this.startAngleSinus = Mathf.Sin(startAngle);

        // Set the pulsation.
        this.pulsation = speed;

        this.startPosition = this.Position;
        this.startTime = Time.time;
    }

    protected override void UpdatePosition()
    {
        base.UpdatePosition();

        // Compute the spiral position.
        float t = Time.time - this.startTime;
        float theta = this.pulsation * t;

        float dx = (this.distanceBeetweenSuccessiveTurnings * theta) * Mathf.Cos(theta);
        float dy = (this.distanceBeetweenSuccessiveTurnings * theta) * Mathf.Sin(theta);
        Vector2 spiralPosition = new Vector2(dx, dy);

        // Rotation and offset.
        float x = (spiralPosition.x * this.startAngleCosinus) - (spiralPosition.y * this.startAngleSinus);
        float y = (spiralPosition.x * this.startAngleSinus) + (spiralPosition.y * this.startAngleCosinus);
        this.Position = this.startPosition + new Vector2(x, y);
    }
}
