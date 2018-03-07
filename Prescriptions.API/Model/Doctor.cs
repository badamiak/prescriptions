using Prescriptions.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Prescriptions.API.Model
{
    public class Doctor
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string PermissionId { get; set; }
        public virtual BitmapImage PermissionIdBarcode { get { return BarcodeService.GetBarcode(PermissionId); } }
    }
}
