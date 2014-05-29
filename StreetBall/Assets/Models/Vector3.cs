using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

namespace Assets.Models
{
    [DataContract]
    public class Vector3
    {
        [DataMember]
        public float X { get; set; }

        [DataMember]
        public float Y { get; set; }

        [DataMember]
        public float Z { get; set; }

        public UnityEngine.Vector3 GetVector()
        {
            return new UnityEngine.Vector3(X, Y, Z);
        }
    }
}
