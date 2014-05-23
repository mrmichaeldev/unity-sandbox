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
        public Button Button { get; set; }

        [DataMember(Name = "teleporter")]
        public Teleporter Teleporter { get; set; }

        [DataMember(Name = "trap")]
        public Trap Trap { get; set; }
    }
}
