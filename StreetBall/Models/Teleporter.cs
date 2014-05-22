using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class Teleporter : GameData
    {
        //Id of the Teleporter object this teleporter links to
        public int TargetId { get; set; }
    }
}
