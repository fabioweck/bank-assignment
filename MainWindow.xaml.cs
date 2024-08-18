using System.Windows;
using System.Windows.Controls;
using BankAssignment.Controller;

namespace BankAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public CurrencyController currencyController;

        public MainWindow()
        {
            currencyController = new CurrencyController();

            InitializeComponent();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        { 
            cmbCurrency.ItemsSource = currencyController.GetCodes();
            cmbCurrency.SelectedIndex = 0;
        }
    }
}