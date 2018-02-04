using FluentNHibernate.Mapping;
using Prescriptions.API.Model.Drugs;

namespace Prescriptions.ModelMappings
{
    public class RefundMap : ClassMap<Refund>
    {
        public RefundMap()
        {
            Table("Refunds");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Value);
            Map(x => x.Level);
        }
    }
}
