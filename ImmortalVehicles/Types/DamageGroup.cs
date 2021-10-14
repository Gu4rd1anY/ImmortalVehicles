using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImmortalVehicles.Types
{
    public class DamageGroup
    {
        [XmlAttribute]
        public string Id { get; set; }
        [XmlAttribute]
        public bool CanDamage { get; set; }
        [XmlAttribute]
        public bool CanDamageTires { get; set; }
    }
}
