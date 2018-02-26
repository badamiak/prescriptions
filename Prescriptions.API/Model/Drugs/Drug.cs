using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Prescriptions.API.Model.Drugs
{
    [Serializable]
    public class Drug
    {
        [XmlIgnore]
        public virtual long Id { get; set; }

        [XmlIgnore]
        public virtual bool IsActive { get; set; } = true;

        [XmlIgnore]
        public virtual string InactiveSince { get; set; }

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

        [XmlIgnore]
        private IList<Refund> _refund = new List<Refund>();

        [XmlArray("Refundacja")]
        [XmlArrayItem("Poziom")]
        public virtual List<Refund> XmlRefunds { get { return this._refund.ToList(); } set { this._refund = value; } }

        [XmlIgnore]
        public virtual IList<Refund> Refunds { get { return this._refund; } set { this._refund = value; } }


        
        public virtual bool HasChangedAccordingTo(Drug x)
        {
            var changed = false;
            changed |= (this.BL7 != x.BL7);
            changed |= (this.EAN != x.EAN);
            changed |= (this.Psychotrope != x.Psychotrope);
            changed |= (this.Senior != x.Senior);
            changed |= (this.Vaccine != x.Vaccine);
            changed |= (this.Price != x.Price);
            changed |= (this.Name != x.Name);
            changed |= (this.InternationalName != x.InternationalName);
            changed |= (this.Form != x.Form);
            changed |= (this.Dosage != x.Dosage);
            changed |= (this.Packaging != x.Packaging);

            var refundsChanged = false;
            var thisRefundLevels = this.Refunds.Select(refund => refund.Level).ToList();
            var xRefundLevels = x.Refunds.Select(refund => refund.Level);

            refundsChanged |= xRefundLevels.Any(level => !thisRefundLevels.Contains(level));
            refundsChanged |= thisRefundLevels.Any(level => !xRefundLevels.Contains(level));

            return changed || refundsChanged;
        }
    }
}
