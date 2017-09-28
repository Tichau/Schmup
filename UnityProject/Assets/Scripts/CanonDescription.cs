// <copyright file="CanonDescription.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

[System.Serializable]
public struct CanonDescription
{
    [SerializeField]
    public Vector2 BulletSpawnOffsetPosition;

    [SerializeField]
    public float BulletSpawnOffsetAngle;
}