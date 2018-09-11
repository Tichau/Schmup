// <copyright file="CanonDescription.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

namespace Data
{
    using UnityEngine;

    [System.Serializable]
    public struct CanonDescription
    {
        [SerializeField]
        public Vector2 BulletSpawnOffsetPosition;

        [SerializeField]
        public float BulletSpawnOffsetAngle;
    }
}
