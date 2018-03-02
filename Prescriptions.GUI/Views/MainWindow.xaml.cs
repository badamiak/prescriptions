using Autofac;
using Prescriptions.API.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
