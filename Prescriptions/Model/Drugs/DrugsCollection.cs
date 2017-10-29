using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Prescriptions.Model.Drugs
{
    [Serializable]
    [XmlRoot("Leki")]
    public class DrugsCollection
    {
        [XmlElement("Lek")]
        public List<Drug> Drugs { get; set; }
    }
}
