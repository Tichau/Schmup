// <copyright file="DamageTakenEventArgs.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using System;

public class DamageTakenEventArgs : EventArgs
{
    public DamageTakenEventArgs(float damage)
    {
        this.Damage = damage;
    }

    public float Damage
    {
        get;
        private set;
    }
}