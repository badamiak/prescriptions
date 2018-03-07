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

        private void NewPatientButtonClick(object sender, RoutedEventArgs e)
        {
            new CreatePatientWindow(this.Container).ShowDialog();
        }

        private void NewPrescriptionButtonClick(object sender, RoutedEventArgs e)
        {
            new NewPrescriptionWindow(this.Container).ShowDialog();
        }

        private void NewDoctorButtonClick(object sender, RoutedEventArgs e)
        {
            new CreateDoctorWidnow(Container).ShowDialog();
            
        }
    }
}
