﻿using FluentNHibernate.Mapping;
using Prescriptions.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.ModelMappings
{
    public class DoctorMap : ClassMap<Doctor>
    {
        public DoctorMap()
        {
            Table("Doctors");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.PermissionId);
        }
    }
}