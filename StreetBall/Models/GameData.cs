using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Models
{
    [DataContract]
    public class GameData
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "position")]
        public Vector2 Position { get; set; }

        [DataMember(Name = "direction")]
        public Direction Direction { get; set; }

        [DataMember(Name = "type")]
        public Type Type { get; set; }
    }
}
