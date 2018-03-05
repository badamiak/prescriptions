using Prescriptions.API.Model.Drugs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.API.Model
{
    public class Prescription
    {
        public virtual string Regon { get; set; }
        public virtual string IdNumber { get; set; }
        public virtual Patient ForPatient { get; set; }
        public virtual Doctor PrescribedBy { get; set; }
        public virtual int NfzWardId { get; set; }
        public virtual PermissionType Permission { get; set; }
        public virtual int PermissionNumber {get;set;}
        public virtual IList<PrescribedDrug> Drugs { get; set; }
        public virtual DateTime CreationDate { get; set; } = DateTime.Now;
        public virtual DateTime ValidFrom { get; set; } = DateTime.Now;
        public virtual Doctor PrescribedByDoctor { get; set; }
        public virtual string PrescribedByCompany { get; set; }
    }
}
