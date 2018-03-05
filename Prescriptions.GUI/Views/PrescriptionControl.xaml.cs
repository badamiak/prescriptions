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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prescriptions.GUI.Views
{
    /// <summary>
    /// Interaction logic for PrescriptionControl.xaml
    /// </summary>
    public partial class PrescriptionControl : UserControl
    {
        Prescription Prescription { get { return this.DataContext as Prescription; } set { this.DataContext = value; } }

        public PrescriptionControl()
        {
            InitializeComponent();
        }

        public PrescriptionControl(Prescription data) : this()
        {
            this.Prescription = data;

        }
    }
}
