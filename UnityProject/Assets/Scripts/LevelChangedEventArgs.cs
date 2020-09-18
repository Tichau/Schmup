// <copyright file="DamageTakenEventArgs.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using System;

public class LevelChangedEventArgs : EventArgs
{
    public LevelChangedEventArgs(Level level)
    {
        this.Level = level;
    }

    public Level Level
    {
        get;
    }
}
