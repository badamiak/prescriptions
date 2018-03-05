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

namespace Prescriptions.GUI.Views
{
    /// <summary>
    /// Interaction logic for CreatePrescription.xaml
    /// </summary>
    public partial class CreatePrescriptionWindow : Window
    {
        Prescription Prescription { get; set; } = new Prescription();
        public CreatePrescriptionWindow()
        {
            InitializeComponent();
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
                print.PrintVisual(PrescriptionPaper, "Printing presscription");
            }
        }
    }
}
