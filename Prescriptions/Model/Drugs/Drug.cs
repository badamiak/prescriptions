using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Prescriptions.Model.Drugs
{
    [Serializable]
    public class Drug
    {
        [XmlAttribute]
        public string BL7 { get; set; }

        [XmlAttribute]
        public string EAN { get; set; }

        [XmlAttribute("psychotrop")]
        public string Psychotrope { get; set; }

        [XmlAttribute("senior")]
        public string Senior { get; set; }

        [XmlAttribute("szczepionka")]
        public string Vaccine { get; set; }

        [XmlAttribute("cena")]
        public string Price { get; set; }

        [XmlElement("Nazwa")]
        public string Name { get; set; }

        [XmlElement("NazwaInt")]
        public string InternationalName { get; set; }

        [XmlElement("Postać")]
        public string Form { get; set; }

        [XmlElement("Dawka")]
        public string Dosage { get; set; }

        [XmlElement("Opakowanie")]
        public string Packaging { get; set; }

        [XmlArray("Refundacja")]
        [XmlArrayItem("Poziom")]
        public List<Refund> Refunds { get; set; } = new List<Refund>();
    }
}
