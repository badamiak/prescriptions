using FluentNHibernate.Mapping;
using Prescriptions.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.ModelMappings
{
    public class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Table("Patients");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.Address);
            Map(x => x.Pesel).CustomSqlType("varchar(13)");
            Map(x => x.DateOfBirth);
        }
    }
}
