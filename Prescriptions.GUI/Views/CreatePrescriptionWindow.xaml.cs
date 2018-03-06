using Autofac;
using Prescriptions.API.Model;
using Prescriptions.API.Services;
using Prescriptions.GUI.Views.Selectors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using IContainer = Autofac.IContainer;

namespace Prescriptions.GUI.Views
{
    /// <summary>
    /// Interaction logic for CreatePrescription.xaml
    /// </summary>
    public partial class CreatePrescriptionWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Prescription _pres;
        Prescription Pres { get { return this._pres; } set { this._pres = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pres))); } }
        public IContainer Container { get; }

        public CreatePrescriptionWindow()
        {
            InitializeComponent();
        }

        public CreatePrescriptionWindow(IContainer container) : this()
        {
            this.Container = container;
        }

        public CreatePrescriptionWindow(IContainer container, Prescription p) : this(container)
        {
            this.Pres = p;
            this.PrescriptionPaper.DataContext = Pres;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void PrintButtonClick(object sender, RoutedEventArgs e)
        {
            var print = new PrintDialog();
            if(print.ShowDialog() == true)
            {
                this.PrescriptionPaper.Width = 4 * 96;
                this.PrescriptionPaper.Height = 8 * 96;
                print.PrintVisual(this.PrescriptionPaper, "Printing presscription");
            }
        }

        private void SelectPatient(object sender, RoutedEventArgs e)
        {
            var selector = new PatientSelector(this.Container.Resolve<IDatabaseAccess>().GetAllEntitiesOfType<Patient>().ToList());
            selector.ShowDialog();
            Pres.ForPatient = selector.SelectedPatient;
        }
    }
}
