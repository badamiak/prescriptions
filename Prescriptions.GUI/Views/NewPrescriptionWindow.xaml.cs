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
            Prescription.Drugs.Remove(DrugsList.SelectedItem as PrescribedDrug);
            Prescription.DrugsListChanged();
        }

        private void AddDrugButtonClick(object sender, RoutedEventArgs e)
        {
            if (drugs == null)
            {
                drugs = database.GetAllEntitiesOfType<Drug>().Where(x=>x.Refunds.Count > 0).Take(10).ToList();
            }
            var selector = new DrugSelector(drugs);
            selector.ShowDialog();

            if (selector.SelectedDrug != null)
            {
                Prescription.Drugs.Add(selector.SelectedDrug);
                Prescription.DrugsListChanged();
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void PrintButtonClick(object sender, RoutedEventArgs e)
        {
            var prescriptionPaper = new PrescriptionControl(CreatePrescription(Prescription));
            var print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                prescriptionPaper.Width = 4 * 96;
                prescriptionPaper.Height = 8 * 96;
                print.PrintVisual(prescriptionPaper, "Printing presscription");
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectDoctorButtonClicked(object sender, RoutedEventArgs e)
        {
            var selector = new DoctorSelector(database.GetAllEntitiesOfType<Doctor>().ToList());
            selector.ShowDialog();

            Prescription.By = selector.SelectedDoctor;
        }

        private Prescription CreatePrescription(NewPrescription data)
        {
            return new Prescription
            {
                IdNumber = data.PresciptionId,
                CreationDate = DateTime.Now,
                Drugs = data.Drugs,
                ForPatient = data.For,
                IdNumberBarcode = BarcodeService.GetBarcode(data.PresciptionId),
                NfzWardId = data.NfzWardId,
                Permission = data.Permission,
                PrescribedByCompany = data.Company,
                PrescribedByCompanyBarcode = BarcodeService.GetBarcode(data.Company),
                PrescribedByDoctor = data.By,
                ValidFrom = data.ValidFrom
                PlacedBy = data.PlacedBy
            };
        }
    }
}
