using Prescriptions.API.Model.Drugs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Prescriptions.API.Model
{
    public class Prescription : INotifyPropertyChanged
    {
        public virtual string Regon { get; set; }
        public virtual string IdNumber { get; set; }
        private Patient _forPatient;
        public virtual Patient ForPatient { get { return this._forPatient; } set { this._forPatient = value; Notify(nameof(ForPatient)); } }
        public virtual Doctor PrescribedBy { get; set; }
        public virtual int NfzWardId { get; set; }
        public virtual PermissionType Permission { get; set; }
        public virtual int PermissionNumber {get;set;}
        public virtual IList<PrescribedDrug> Drugs { get; set; }
        public virtual DateTime CreationDate { get; set; } = DateTime.Now;
        public virtual DateTime ValidFrom { get; set; } = DateTime.Now;
        public virtual Doctor PrescribedByDoctor { get; set; }
        public virtual string PrescribedByCompany { get; set; }
        public virtual BitmapImage PrescribedByCompanyBarcode { get; set; }
        public BitmapImage IdNumberBarcode { get; set; }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        private void Notify(string propertyName)
        {
            var e = PropertyChanged;
            e?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
