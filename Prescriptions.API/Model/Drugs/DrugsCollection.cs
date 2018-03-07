using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Prescriptions.API.Model.Drugs
{
    [Serializable]
    [XmlRoot("Leki")]
    public class DrugsCollection
    {
        [XmlElement("Lek")]
        public List<XmlDrug> Drugs { get; set; }
    }
}
