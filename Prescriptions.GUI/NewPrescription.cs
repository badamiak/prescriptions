using Prescriptions.API.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.GUI
{
    public class NewPrescription : INotifyPropertyChanged
    {
        private List<PrescribedDrug> _drugs;
        private Patient _for;
        private Doctor _by;

        public List<PermissionType> Permissions { get; set; } = Enum.GetValues(typeof(PermissionType)).Cast<PermissionType>().ToList();
        public string Company { get; set; }
        public Patient For
        {
            get { return _for; }
            set
            {
                _for = value;
                NotifyPropertyChanged(nameof(For));
            }
        }
        public int NfzWardId { get; set; }
        public string Id { get; set; }

        public List<PrescribedDrug> Drugs
        {
            get { return _drugs; }
            set
            {
                _drugs = value;
                NotifyPropertyChanged(nameof(Drugs));
            }
        }

        public DateTime ValidFrom { get; set; } = DateTime.Now;
        public Doctor By
        {
            get { return _by; }
            set
            {
                _by = value;
                NotifyPropertyChanged(nameof(By));
            }
        }
        public string PlacedBy { get; set; }
        public PermissionType Permission { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            var ev = PropertyChanged;
            ev?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
