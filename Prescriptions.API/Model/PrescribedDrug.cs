using Prescriptions.API.Model.Drugs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.API.Model
{
    public class PrescribedDrug : Drug
    {
        public virtual RefundLevel AppliedRefund { get; set; }
    }
}
