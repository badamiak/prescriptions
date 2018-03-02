using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescriptions.API.Model
{
    public class Patient : IVerifiable, INotifyPropertyChanged
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        private DateTime _dateOfBirth;

        public virtual DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; var ev = PropertyChanged; ev?.Invoke(this, new PropertyChangedEventArgs(nameof(DateOfBirth))); }
        }

        public virtual string Pesel { get; set; }
        public virtual string Address { get; set;}

        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual bool Verify()
        {
            var pesel = this.Pesel?.Select(x => int.Parse(new string(new[] { x }))).ToArray();
            if (pesel != null)
            {
                var controlsum =
                    pesel[3] + pesel[7]
                    + 3 * (pesel[2] + pesel[6])
                    + 7 * (pesel[1] + pesel[5] + pesel[9])
                    + 9 * (pesel[0] + pesel[4] + pesel[8]);
                controlsum %= 10;

                return controlsum == pesel[10];
            }
            else return false;
        }
    }
}
