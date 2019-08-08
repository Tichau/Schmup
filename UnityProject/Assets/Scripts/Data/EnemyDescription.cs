// <copyright file="EnemyDescription.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

namespace Data
{
    using System.Xml.Serialization;
    using UnityEngine;

    [XmlRoot("EnemyDescription")]
    [XmlType("EnemyDescription")]
    public class EnemyDescription
    {
        [XmlElement]
        public float SpawnDate
        {
            get;
            set;
        }

        [XmlElement]
        public Vector2 SpawnPosition
        {
            get;
            set;
        }

        [XmlElement]
        public string PrefabPath
        {
            get;
            set;
        }
    }
}
