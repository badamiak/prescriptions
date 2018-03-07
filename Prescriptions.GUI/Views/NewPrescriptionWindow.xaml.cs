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
using Autofac;
using IContainer = Autofac.IContainer;
using Prescriptions.API.Model.Drugs;

namespace Prescriptions.GUI.Views
{
    /// <summary>
    /// Interaction logic for NewPrescriptionWindow.xaml
    /// </summary>
    public partial class NewPrescriptionWindow : Window
    {
        IContainer container;
        IDatabaseAccess database;

        List<Drug> drugs;

        public NewPrescription Prescription
        {
            get
            {
                return DataContext as NewPrescription;
            }
            set
            {
                DataContext = value;
            }
        }

        public NewPrescriptionWindow()
        {
            InitializeComponent();
            Prescription = new NewPrescription();
        }

        public NewPrescriptionWindow(IContainer container) : this()
        {
            this.container = container;
            this.database = container.Resolve<IDatabaseAccess>();
        }

        private void SelectPatientClick(object sender, RoutedEventArgs e)
        {
            var selector = new PatientSelector(database.GetAllEntitiesOfType<Patient>().ToList());
            selector.ShowDialog();

            Prescription.For = selector.SelectedPatient;
        }

        private void RemoveDrug(object sender, RoutedEventArgs e)
        {

        }

        private void AddDrugButtonClick(object sender, RoutedEventArgs e)
        {
            if (drugs == null)
            {
                drugs = database.GetAllEntitiesOfType<Drug>().Take(10).ToList();
            }
            var selector = new DrugSelector(drugs);
            selector.ShowDialog();

            if (selector.SelectedDrug != null)
            {
                Prescription.Drugs.Add(selector.SelectedDrug);
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void PrintButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
