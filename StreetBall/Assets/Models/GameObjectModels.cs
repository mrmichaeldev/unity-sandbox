using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Assets.Models
{
    [DataContract]
    public class GameObjectModels
    {
        [DataMember(Name = "button")]
        public Button[] Buttons { get; set; }

        [DataMember(Name = "teleporter")]
        public Teleporter[] Teleporters { get; set; }

        [DataMember(Name = "trap")]
        public Trap[] Traps { get; set; }
    }
}
