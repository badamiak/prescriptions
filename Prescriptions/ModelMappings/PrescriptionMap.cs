using FluentNHibernate.Mapping;
using Prescriptions.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.ModelMappings
{
    public class PrescriptionMap : ClassMap<Prescription>
    {
        public PrescriptionMap()
        {
            Table("Prescriptions");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.IdNumber);
            Map(x => x.PrescribedByCompany);
            HasOne(x => x.PrescribedByDoctor);
            HasOne(x => x.ForPatient);
            Map(x => x.NfzWardId);
            Map(x => x.Permission);
            HasMany(x => x.Drugs).Cascade.All();
            Map(x => x.CreationDate);
            Map(x => x.ValidFrom);
            Map(x => x.PlacedBy);
        }

    }
}
