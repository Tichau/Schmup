// <copyright file="Engines.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class Engines : MonoBehaviour
{
    [SerializeField]
    private float maximumSpeed = 1f;

    [SerializeField]
    private Rect allowedZone;

    [SerializeField]
    private bool drawAllowedZoneGizmo;

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

        private set
        {
            this.transform.position = value;
        }
    }

    public void SetSpeed(Vector2 speed)
    {
        this.Speed = speed;
    }

    public void AddSpeed(Vector2 speed)
    {
        this.Speed += speed;
    }

    private void Start()
    {
        // Positioning the gameobject at the center of the allowed zone.
        this.Position = this.allowedZone.center;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (this.drawAllowedZoneGizmo)
        {
            Helper.DrawRect(this.allowedZone, Color.red);
        }
#endif

        Vector2 newPosition = this.Position + (this.Speed * this.maximumSpeed * Time.deltaTime);

        if (!this.allowedZone.Contains(newPosition))
        {
            return;
        }

        this.Position = newPosition;
    }
}
