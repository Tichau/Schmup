// <copyright file="LevelDescription.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

namespace Data
{
    using System.Xml.Serialization;

    [XmlRoot("LevelDescription")]
    [XmlType("LevelDescription")]
    public class LevelDescription
    {
        [XmlAttribute]
        public float Duration
        {
            get;
            set;
        }

        [XmlElement("EnemyDescription", typeof(EnemyDescription))]
        public EnemyDescription[] Enemies
        {
            get;
            set;
        }

        [XmlElement]
        public string Scene
        {
            get;
            set;
        }

        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }
    }
}
