using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class Trap : GameData
    {
        //button to control if the trap is enabled or disabled
        [DataMember(Name = "buttonId")]
        public int ButtonId { get; set; }

        //List of fields that comprise the trap
        [DataMember(Name = "fields")]
        public GameData[] Fields { get; set; }
    }
}
