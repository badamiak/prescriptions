using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.API.Model
{
    public class Doctor
    {
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string PermissionId { get; set; }
    }
}
