using Autofac;
using Prescriptions.API.Model;
using Prescriptions.API.Services;
using Prescriptions.Database;
using Prescriptions.GUI.ViewModels;
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
    /// Interaction logic for CreatePatientWindow.xaml
    /// </summary>
    public partial class CreatePatientWindow : Window
    {
        private readonly IContainer container;

        public CreatePatientWindow()
        {
            InitializeComponent();
        }

        public CreatePatientWindow(IContainer container) : this()
        {
            this.DataContext = new Patient();
            this.container = container;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = this.DataContext as Patient;
                var value = sender as TextBox;
                var pesel = value.Text.Select(x => int.Parse(new string(new[] { x }))).ToArray();

                var year = 1900 + (pesel[2] / 2) * 100 + pesel[0] * 10 + pesel[1];
                var month = (pesel[2] * 10 + pesel[3]) % 20;
                var day = pesel[4] * 10 + pesel[5];

                var dateOfBirth = new DateTime(year, month, day);

                context.DateOfBirth = dateOfBirth;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as Patient;
            if(context.Verify())
            {
                this.container.Resolve<IDatabaseAccess>().Save(context); 
            }
            this.Close();

        }
    }
}