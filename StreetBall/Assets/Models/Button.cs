using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class Button : GameData
    {
        [DataMember]
        public bool IsOn { get; set; }

        [DataMember]
        public int[] Triggers { get; set; }
    }
}
