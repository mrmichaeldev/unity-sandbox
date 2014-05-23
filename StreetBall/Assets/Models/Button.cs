using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class Button : GameData
    {
        [DataMember(Name="isOn")]
        public bool IsOn { get; set; }

        [DataMember(Name = "triggers")]
        public int[] Triggers { get; set; }
    }
}
