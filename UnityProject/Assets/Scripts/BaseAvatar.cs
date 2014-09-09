// <copyright file="BaseAvatar.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class BaseAvatar : MonoBehaviour
{
    [SerializeField]
    private int maximumHealthPoint;

    public int HealthPoint
    {
        get; 
        private set;
    }

    private void Start()
    {
        this.HealthPoint = this.maximumHealthPoint;
    }
    
    private void Update()
    {
    }
}
