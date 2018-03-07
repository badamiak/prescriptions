using Prescriptions.API.Model;
using Prescriptions.API.Model.Drugs;
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

namespace Prescriptions.GUI.Views.Selectors
{
    /// <summary>
    /// Interaction logic for DrugSelector.xaml
    /// </summary>
    public partial class DrugSelector : Window, INotifyPropertyChanged
    {
        private PrescribedDrug _selectedDrug = new PrescribedDrug();
        public PrescribedDrug SelectedDrug
        {
            get
            {
                return _selectedDrug;
            }
            set
            {
                _selectedDrug = value;
                NotifyPropertyChanged(nameof(SelectedDrug));
            }
        }

        public DrugSelector()
        {
            InitializeComponent();
        }

        public DrugSelector(List<Drug> drugs) : this()
        {
            this.DataContext = drugs;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            var ev = PropertyChanged;
            ev?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SelectButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectedDrugChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedDrug.Drug = SelectionList.SelectedItem as Drug;
            if(SelectedDrug.Drug.Refunds?.Count != 0)
            {
                RefundSelector.ItemsSource = SelectedDrug.Drug.Refunds;
            }
            else
            {
                RefundSelector.ItemsSource = new List<Refund> { new Refund { Level = RefundLevel.Full, Value = "Zawsze" } };
            }
        }

        private void SelectedRefundChanged(object sender, SelectionChangedEventArgs e)
        {
            var refund = RefundSelector.SelectedItem as Refund;
            SelectedDrug.AppliedRefund = refund.Level;
            InCase.Text = refund.Value;
        }
    }
}
