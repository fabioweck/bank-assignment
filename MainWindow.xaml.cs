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
        public AccountController accountController;

        public MainWindow()
        {
            currencyController = new CurrencyController();
            accountController = new AccountController();

            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            PopulateComboBox();
        }

        private void PopulateComboBox()
        { 
            cmbCurrency.ItemsSource = currencyController.GetCodes();
            cmbCurrency.SelectedIndex = 0;
        }

        private void cmbCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string code = cmbCurrency.SelectedItem.ToString();
            string rate = currencyController.GetRate(code).ToString();
            txtRate.Text = $"Exchange rate: 1 CAD to ${rate} {code}";
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            double amount;
            try
            {
                amount = Convert.ToDouble(amountInput.Text);
            }
            catch
            {
                CustomPopup popup = new CustomPopup();
                popup.Show();
                return;
            }

            string code = cmbCurrency.SelectedItem.ToString();
            double amountConverted = currencyController.ExchangeToCAD(amount, code);

            if(accountController.CanWithdraw(amountConverted))
            {
                accountController.Withdraw(amountConverted);
                lblBalance.Content = accountController.GetBalance();
            }
            else
            {
                   
            }
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            double amount = Convert.ToDouble(amountInput.Text);
            string code = cmbCurrency.SelectedItem.ToString();
            double amountConverted = currencyController.ExchangeToCAD(amount, code);
            accountController.Deposit(amountConverted);
            lblBalance.Content = accountController.GetBalance();
        }
    }
}