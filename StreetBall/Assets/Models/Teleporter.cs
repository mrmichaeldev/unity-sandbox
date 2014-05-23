using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class Teleporter : GameData
    {
        //Id of the Teleporter object this teleporter links to
        [DataMember(Name="targetId")]
        public int TargetId { get; set; }
    }
}
