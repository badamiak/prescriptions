using Autofac;
using Prescriptions.API.Model;
using Prescriptions.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Prescriptions.GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IContainer Container;
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow AddContainer(IContainer container)
        {
            this.Container = container;
            return this;
        }

        public MainWindow(MainWindowContext context) : this()
        {
            this.DataContext = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var createPatientView = new CreatePatientWindow(this.Container, new Patient());
            createPatientView.ShowDialog();
        }

        private void NewPrescriptionButtonClick(object sender, RoutedEventArgs e)
        {
            var createPrescriptionView = new CreatePrescriptionWindow(this.Container);
            createPrescriptionView.ShowDialog();
        }

        private void POCPrescriptionPrintButtonClick(object sender, RoutedEventArgs e)
        {
            var prescription = new Prescription()
            {
                CreationDate = DateTime.Now,
                Drugs = new List<PrescribedDrug>
                {
                    new PrescribedDrug{Name = "lek 1", AppliedRefund = API.Model.Drugs.RefundLevel.Bezpłatny},
                    new PrescribedDrug{Name = "lek 2", AppliedRefund = API.Model.Drugs.RefundLevel.FiftyPercent}
                },
                ForPatient = new Patient { Name = "Jan", Surname = "Kowalski" },
                IdNumber = "112542",
                NfzWardId = 15,
                Permission = PermissionType.S,
                PermissionNumber = 1252424,
                PrescribedBy = new Doctor { Name = "Rafał", Surname = "Wilczur", PermissionId = "12512415" },
                PrescribedByCompany = "Twój lekarz sp. z o.o.",
                PrescribedByDoctor = new Doctor { Name = "Rafał", Surname = "Wilczur", PermissionId = "12512415" },
                Regon = "23627236",
                ValidFrom = DateTime.Now
            };
            var createPrescriptionView = new CreatePrescriptionWindow(this.Container, prescription);
            createPrescriptionView.ShowDialog();
        }

    }
}
