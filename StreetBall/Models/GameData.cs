using System.Runtime.Serialization;
using UnityEngine;

namespace Models
{
    [DataContract]
    public abstract class GameData
    {
        [DataMember(Name = "id")]
        int Id;

        [DataMember(Name = "position")]
        Vector2 Position;

        [DataMember(Name = "direction")]
        Direction Direction;
    }
}
