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
using Autofac;
using IContainer = Autofac.IContainer;
using Prescriptions.API.Model;
using Prescriptions.API.Services;

namespace Prescriptions.GUI.Views
{
    /// <summary>
    /// Interaction logic for CreateDoctorWidnow.xaml
    /// </summary>
    public partial class CreateDoctorWidnow : Window
    {
        private IContainer container;
        public CreateDoctorWidnow()
        {
            InitializeComponent();
        }

        public CreateDoctorWidnow(IContainer container) : this()
        {
            DataContext = new Doctor();
            this.container = container;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            container.Resolve<IDatabaseAccess>().Save(DataContext as Doctor);
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
