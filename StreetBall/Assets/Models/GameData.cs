using Assets.Models;
using System;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class GameData
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Vector3 Position { get; set; }

        [DataMember]
        public int Rotation { get; set; }
    }
}