using FluentNHibernate.Mapping;
using Prescriptions.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.ModelMappings
{
    public class PrescribedDrugMap : ClassMap<PrescribedDrug>
    {
        public PrescribedDrugMap()
        {
            Table("PrescribedDrugs");
            Id(x => x.Id).GeneratedBy.Identity();
            HasOne(x => x.Drug);
            Map(x => x.AppliedRefund);
        }
    }
}
