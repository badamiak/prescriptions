using Prescriptions.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prescriptions.GUI.Views.Selectors
{
    /// <summary>
    /// Interaction logic for DoctorSelector.xaml
    /// </summary>
    public partial class DoctorSelector : Window
    {
        public Doctor SelectedDoctor { get; set; }
        public DoctorSelector()
        {
            InitializeComponent();
        }
        public DoctorSelector(List<Doctor> doctors) : this()
        {
            DataContext = doctors;
        }

        private void SelectedDoctorChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDoctor = DoctorsList.SelectedItem as Doctor;
            this.Close();
        }

        private void SelectDoctorButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
