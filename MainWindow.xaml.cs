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

        /* Since the program has only one display, MainWindow is used as "View" class
           to collect user side inputs and request logic to all controllers,
           following MVC pattern */

        //Controllers for clients, account, and currency models
        public ClientController clientController;
        public AccountController accountController;
        public CurrencyController currencyController;

        //To try other clients, use '234' or '345'
        public int clientID = 123;

        public string rateText = String.Empty;
        public string transactionDetailsText = String.Empty;

        public MainWindow()
        {
            //Instantiate client, account, and currency controllers
            clientController = new ClientController();
            accountController = new AccountController();
            currencyController = new CurrencyController();

            InitializeComponent();

            //Center main window on the screen when initialized
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DisplayClientName(clientID);
            PopulateCmbCurrency();
            PopulateCmbAccount(clientID);
        }

        //Method created to simulate the steps before reaching account page
        //assuming that a client was previously selected by ID
        private void DisplayClientName(int clientID)
        {
            lblClient.Content = $"Hello {clientController.GetClientName(clientID)}!";
        }

        //Get all codes to be displayed in the currency combo box
        private void PopulateCmbCurrency()
        { 
            cmbCurrency.ItemsSource = currencyController.GetCodes();
        }

        //Get all codes to be displayed in the account combo box
        private void PopulateCmbAccount(int clientID)
        {
            cmbAccount.ItemsSource = accountController.GetAccountsID(clientID);
        }

        //Get balance to display on the screen
        private void DisplayAccountDetails()
        {
            int id = (int)cmbAccount.SelectedItem;
            lblBalance.Content = $"${accountController.GetBalance(id)}";
            cmbCurrency.SelectedIndex = 0;
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
            double rate = currencyController.GetRate(code);
            rateText = $"$1 {code} = ${1/rate} CAD";
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

        private void NumericTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Use a regular expression to check if the input is numeric
            e.Handled = !IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string text)
        {
            // Regex to allow numeric values with optional decimal point
            return System.Text.RegularExpressions.Regex.IsMatch(text, "^[0-9]*(\\.[0-9]*)?$");
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