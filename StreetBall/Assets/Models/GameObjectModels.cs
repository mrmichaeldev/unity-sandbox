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
        [DataMember]
        public Button[] Buttons { get; set; }

        [DataMember]
        public Teleporter[] Teleporters { get; set; }

        [DataMember]
        public Trap[] Traps { get; set; }
    }
}
