using FluentNHibernate.Mapping;
using Prescriptions.API.Model.Drugs;

namespace Prescriptions.ModelMappings
{
    public class DrugMap : ClassMap<Drug>
    {
        public DrugMap()
        {
            Table("Drugs");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.BL7);
            Map(x => x.Dosage);
            Map(x => x.EAN);
            Map(x => x.Form);
            Map(x => x.InternationalName);
            Map(x => x.Name);
            Map(x => x.Packaging);
            Map(x => x.Price);
            Map(x => x.Psychotrope);
            Map(x => x.Senior);
            Map(x => x.Vaccine);
            Map(x => x.IsActive);
            Map(x => x.InactiveSince);
            HasMany(x => x.Refunds).Cascade.SaveUpdate();
        }
    }
}
