using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Prescriptions.Model.Drugs
{
    [Serializable]
    public class Refund
    {
        [XmlAttribute("poziom")]
        public RefundLevel Level { get; set; }

        [XmlText]
        public string Value{ get; set; }

    }
}
