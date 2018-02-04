using System;
using System.Xml.Serialization;

namespace Prescriptions.API.Model.Drugs
{
    [Serializable]
    public class Refund
    {
        [XmlIgnore]
        public virtual long Id { get; set; }

        [XmlAttribute("poziom")]
        public virtual RefundLevel Level { get; set; }

        [XmlText]
        public virtual string Value{ get; set; }

    }
}
