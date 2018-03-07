using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Prescriptions.API.Model.Drugs
{
    [Serializable]
    public class XmlDrug
    {
        [XmlAttribute]
        public virtual string BL7 { get; set; }

        [XmlAttribute]
        public virtual string EAN { get; set; }

        [XmlAttribute("psychotrop")]
        public virtual string Psychotrope { get; set; }

        [XmlAttribute("senior")]
        public virtual string Senior { get; set; }

        [XmlAttribute("szczepionka")]
        public virtual string Vaccine { get; set; }

        [XmlAttribute("cena")]
        public virtual string Price { get; set; }

        [XmlElement("Nazwa")]
        public virtual string Name { get; set; }

        [XmlElement("NazwaInt")]
        public virtual string InternationalName { get; set; }

        [XmlElement("Postać")]
        public virtual string Form { get; set; }

        [XmlElement("Dawka")]
        public virtual string Dosage { get; set; }

        [XmlElement("Opakowanie")]
        public virtual string Packaging { get; set; }

        [XmlArray("Refundacja")]
        [XmlArrayItem("Poziom")]
        public virtual List<Refund> XmlRefunds { get; set; }

        public virtual Drug ToDbDrug()
        {
            return new Drug
            {
                BL7 = this.BL7,
                Dosage = this.Dosage,
                EAN = this.EAN,
                Form = this.Form,
                InternationalName = this.InternationalName,
                Name = this.Name,
                Packaging = this.Packaging,
                Price = this.Price,
                Psychotrope = this.Psychotrope,
                Refunds = this.XmlRefunds,
                Senior = this.Senior,
                Vaccine = this.Vaccine
            };
        }
    }
}
