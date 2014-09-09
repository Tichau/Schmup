// <copyright file="Engines.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class Engines : MonoBehaviour
{
    [SerializeField]
    private Rect allowedZone;

    [SerializeField]
    private bool drawAllowedZoneGizmo;

    private BaseAvatar baseAvatar;

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

    private float MaximumSpeed
    {
        get
        {
            if (this.baseAvatar == null)
            {
                return 0f;
            }

            return this.baseAvatar.MaximumSpeed;
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
        this.baseAvatar = this.GetComponent<BaseAvatar>();
        if (this.baseAvatar == null)
        {
            Debug.LogWarning(string.Format("Can't retrieve a base avatar on the gameobject {0}.", this.gameObject.name));
        }

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

        Vector2 newPosition = this.Position + (this.Speed * this.MaximumSpeed * Time.deltaTime);

        if (!this.allowedZone.Contains(newPosition))
        {
            return;
        }

        this.Position = newPosition;
    }
}
