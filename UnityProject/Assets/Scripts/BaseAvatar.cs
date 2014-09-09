// <copyright file="BaseAvatar.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class BaseAvatar : MonoBehaviour
{
    [SerializeField]
    private int maximumHealthPoint;

    [SerializeField]
    private float initialMaximumSpeed = 1f;

    public int HealthPoint
    {
        get; 
        private set;
    }

    public float MaximumSpeed
    {
        get;
        private set;
    }

    private void Start()
    {
        this.HealthPoint = this.maximumHealthPoint;
        this.MaximumSpeed = this.initialMaximumSpeed;
    }
    
    private void Update()
    {
    }
}
