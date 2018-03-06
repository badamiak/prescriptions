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
    /// Interaction logic for PatientSelector.xaml
    /// </summary>
    public partial class PatientSelector : Window
    {
        public Patient SelectedPatient { get; private set; }
        public PatientSelector()
        {
            InitializeComponent();
        }

        public PatientSelector(List<Patient> patients) : this()
        {
            this.DataContext = patients;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedPatient = this.Data.SelectedItem as Patient;
        }

        private void SelectButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}