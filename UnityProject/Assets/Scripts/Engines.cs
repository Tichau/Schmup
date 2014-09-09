// <copyright file="Engines.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class Engines : MonoBehaviour
{
    [SerializeField]
    private float maximumSpeed = 1f;

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

    private void Update()
    {
        this.Position += this.Speed * this.maximumSpeed * Time.deltaTime;
    }
}
