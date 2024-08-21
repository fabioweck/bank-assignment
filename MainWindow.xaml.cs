using System.ComponentModel;
using System.Data.Common;
using System.Transactions;
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

        public string originalAmountText = String.Empty;
        public string amountConvertedText = String.Empty;
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
            lblClient.Content = $"Welcome {clientController.GetClientName(clientID)}!";
        }

        //Get all codes to be displayed in the currency combo box
        private void PopulateCmbCurrency()
        { 
            cmbCurrency.ItemsSource = currencyController.GetCodes();
        }

        //Get all codes to be displayed in the account combo box
        private void PopulateCmbAccount(int clientID)
        {
            cmbAccount.ItemsSource = accountController.GetCheckingAccountIDs(clientID);
        }

        //Get balance to display on the screen
        private void DisplayAccountDetails()
        {
            int id = (int)cmbAccount.SelectedItem;
            lblBalance.Content = $"${accountController.GetBalance(id):F2} (CAD)";
            cmbCurrency.SelectedIndex = 0;
        }

        //Get the amount converted and account id to pass to transaction
        private (int code, double amount) TransactionDetails()
        {
            double amountConverted = ConvertInput();
            int code = (int)cmbAccount.SelectedItem;
            return (code, amountConverted);
        }

        private void cmbCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string code = cmbCurrency.SelectedItem.ToString();
            double rate = currencyController.GetRate(code);
            if(code != "CAD") rateText = $"($1 {code} = ${1/rate} CAD)";
        }

        private string ConfirmTransaction(string operation, string convertedAmount)
        { 
            TransactionPopup transactionScreen = new TransactionPopup(operation, originalAmountText, convertedAmount, rateText);
            string optionSelected = String.Empty;
            transactionScreen.Option += (o) => { optionSelected = o; };
            transactionScreen.ShowDialog();
            return optionSelected;
        }

        private void ClearTexts()
        {
            originalAmountText = String.Empty;
            rateText = String.Empty;
            amountConvertedText = String.Empty;
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            var transaction = TransactionDetails();
            string operation = btnDeposit.Content.ToString();

            string confirmTransaction = ConfirmTransaction(operation, amountConvertedText);

            if (confirmTransaction == "Confirm") 
            {
                accountController.Deposit(transaction.code, transaction.amount);
                ClearTexts();
                DisplayAccountDetails();
            }
        } 

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateInput()) return;

            var transaction = TransactionDetails();
            string operation = btnWithdraw.Content.ToString();

            if (accountController.CanWithdraw(transaction.code, transaction.amount))
            {
                string confirmTransaction = ConfirmTransaction(operation, amountConvertedText);

                if (confirmTransaction == "Confirm")
                {
                    accountController.Withdraw(transaction.code, transaction.amount);
                    ClearTexts();
                    DisplayAccountDetails();
                }
            }
            else
            {
                amountConvertedText = $"${transaction.amount} CAD";
                string code = cmbCurrency.SelectedItem.ToString();
                string message;
                if (code != "CAD") message = $"{originalAmountText} = {amountConvertedText}";
                else message = amountConvertedText;
                AlertPopup popup = new AlertPopup(message);
                popup.ShowDialog();
                ClearTexts();
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

        private bool ValidateInput()
        {
            if (amountInput.Text == null ||
                amountInput.Text == String.Empty ||
                Convert.ToDouble(amountInput.Text) == 0) return false;
            return true;
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
            originalAmountText = $"${amountInput.Text} {code}";
            double amountConverted = currencyController.ExchangeToCAD(amount, code);
            if (code != "CAD")
            {
                amountConvertedText = $"Equals ${amountConverted} CAD";
            }
            else 
            { 
                amountConvertedText = String.Empty; 
            }

            return amountConverted;

        }

        private void cmbAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayAccountDetails();
        }
    }
}