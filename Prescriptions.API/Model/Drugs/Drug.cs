using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Prescriptions.API.Model.Drugs
{
    public class Drug : XmlDrug
    {
        public virtual long Id { get; set; }
        public virtual bool IsActive { get; set; } = true;
        public virtual string InactiveSince { get; set; }
        public virtual IList<Refund> Refunds { get; set; }

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
