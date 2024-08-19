using System.ComponentModel;
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

        //Controllers for currency and account model
        public CurrencyController currencyController;
        public AccountController accountController;

        public string rateText = String.Empty;
        public string transactionDetails = String.Empty;

        public MainWindow()
        {
            //Instantiate both currency and account controllers
            currencyController = new CurrencyController();
            accountController = new AccountController();

            InitializeComponent();

            //Center main window on the screen when initialized
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Populate both combo boxes
            PopulateCmbCurrency();
            PopulateCmbAccount();
        }

        //Get all codes to be displayed in the currency combo box
        private void PopulateCmbCurrency()
        { 
            cmbCurrency.ItemsSource = currencyController.GetCodes();
        }

        //Get all codes to be displayed in the account combo box
        private void PopulateCmbAccount()
        {
            cmbAccount.ItemsSource = accountController.GetAccountsID();
        }

        //Get balance to display on the screen
        private void DisplayAccountDetails()
        {
            int id = (int)cmbAccount.SelectedItem;
            lblBalance.Content = $"${accountController.GetBalance(id)}";
        }

        //Get the amount converted and account id to pass to transaction
        private (int id, double amount) TransactionDetails()
        {
            double amountConverted = ConvertInput();
            int id = (int)cmbAccount.SelectedItem;
            return (id, amountConverted);
        }

        private void cmbCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string code = cmbCurrency.SelectedItem.ToString();
            string rate = currencyController.GetRate(code).ToString();
            rateText = $"Exchange rate: 1 CAD to ${rate} {code}";
        }

        private string ConfirmTransaction(string operation)
        { 
            TransactionPopup transactionScreen = new TransactionPopup(operation, rateText);
            string optionSelected = String.Empty;
            transactionScreen.Option += (o) => { optionSelected = o; };
            transactionScreen.ShowDialog();
            return optionSelected;
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            var transaction = TransactionDetails();
            string operation = btnDeposit.Content.ToString();

            string confirmTransaction = ConfirmTransaction(operation);

            if (confirmTransaction == "Ok") 
            {
                accountController.Deposit(transaction.id, transaction.amount);
                DisplayAccountDetails();
            }
        } 

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        { 
            var transaction = TransactionDetails();
            string operation = btnWithdraw.Content.ToString();

            string confirmTransaction = ConfirmTransaction(operation);

            if (confirmTransaction == "Ok")
            {
                if (accountController.CanWithdraw(transaction.id, transaction.amount))
                {
                    accountController.Withdraw(transaction.id, transaction.amount);
                    DisplayAccountDetails();
                }
            }  
        }

        private Double ConvertInput()
        {

            double amount;
            
            try
            {
                amount = Convert.ToDouble(amountInput.Text);
            }
            catch
            {
                return 0;
            }

            string code = cmbCurrency.SelectedItem.ToString();

            return currencyController.ExchangeToCAD(amount, code);

        }

        private void cmbAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayAccountDetails();
        }
    }
}