using Autofac;
using Prescriptions.API.Model;
using Prescriptions.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

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
                ForPatient = new Patient { Name = "Jan", Surname = "Kowalski", Address=$"al. Bakaliowa 13/4{Environment.NewLine}00-220 Warszsawa", Pesel = "87030501420" },
                IdNumber = "0215010000014221845781",
                NfzWardId = 15,
                Permission = PermissionType.X,
                PermissionNumber = 1252424,
                PrescribedBy = new Doctor { Name = "Rafał", Surname = "Wilczur", PermissionId = "12512415" },
                PrescribedByCompany = "Twój lekarz sp. z o.o.",
                PrescribedByDoctor = new Doctor { Name = "Rafał", Surname = "Wilczur", PermissionId = "NPWZ 12345678", PermissionIdBarcode = GetImageFromBase64(riverOx_barcode128.Barcode128.generateBarcode("NPWZ 12345678", false)) },
                Regon = "23627236",
                ValidFrom = DateTime.Now,
                PrescribedByCompanyBarcode = GetImageFromBase64(riverOx_barcode128.Barcode128.generateBarcode("0123456123456", false)),
                IdNumberBarcode = GetImageFromBase64(riverOx_barcode128.Barcode128.generateBarcode("0215010000014221845781", false))
            };
            var createPrescriptionView = new CreatePrescriptionWindow(this.Container, prescription);
            createPrescriptionView.ShowDialog();
        }

        private BitmapImage GetImageFromBase64(string base64EncodedImage)
        {
            var bytes = Convert.FromBase64String(base64EncodedImage.Remove(0, "data:image/png;base64,".Length));
            var ms = new MemoryStream(bytes);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
            
        }

    }
}
