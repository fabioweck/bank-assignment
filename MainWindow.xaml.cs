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

        /* 
           Since the program has only one display, MainWindow is used as "View" class
           to collect user side inputs and request logic to all controllers,
           following MVC pattern 
        */

        //Controllers for clients, account, and currency models
        public ClientController clientController;
        public AccountController accountController;
        public CurrencyController currencyController;

        //To try other clients and accounts, use '234' or '345'
        public int clientID = 123;

        //Variables to store strings formated and be passed to popups
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

        //Get all accounts of a client to be displayed in the account combo box
        private void PopulateCmbAccount(int clientID)
        {
            cmbAccount.ItemsSource = accountController.GetCheckingAccountIDs(clientID);
        }

        //Get client's account balance to display on the screen
        private void DisplayAccountDetails()
        {
            //Get account ID and pass to GetBalance() to return the balance
            int accountID = (int)cmbAccount.SelectedItem;
            lblBalance.Content = $"${accountController.GetBalance(accountID):F2} (CAD)";
            cmbCurrency.SelectedIndex = 0;
        }

        //Get the amount converted and account id to pass to operation (deposit and withdraw)
        private (int code, double amount) TransactionDetails()
        {
            double amountConverted = ConvertInput();
            int code = (int)cmbAccount.SelectedItem;
            return (code, amountConverted);
        }

        //Method to format rateText variable based on currency seletion
        private void CmbCurrencySelection(object sender, SelectionChangedEventArgs e)
        {
            string code = cmbCurrency.SelectedItem.ToString();
            double rate = currencyController.GetRate(code);
            if(code != "CAD") rateText = $"($1 {code} = ${1/rate} CAD)";
        }

        //Method to open transaction popup and return confirmation
        private string ConfirmTransaction(string operation, string convertedAmount)
        { 
            TransactionPopup transactionScreen = new TransactionPopup(operation, originalAmountText, convertedAmount, rateText);
            string optionSelected = String.Empty;

            //Delegate method to an event and collect the option selected
            transactionScreen.Option += (o) => { optionSelected = o; }; 

            transactionScreen.ShowDialog();
            return optionSelected;
        }

        //Method to clear texts
        private void ClearTexts()
        {
            originalAmountText = String.Empty;
            rateText = String.Empty;
            amountConvertedText = String.Empty;
            amountInput.Text = "0";
        }

        //Method to handle button "Deposit"
        private void BtnDeposit(object sender, RoutedEventArgs e)
        {
            //Check if input is valid, if not, then return
            if (!ValidateInput()) return;

            //Collect transaction details to pass to ConfirmTransaction method
            var transaction = TransactionDetails();
            string operation = btnDeposit.Content.ToString();

            //Collect the option selected
            string confirmTransaction = ConfirmTransaction(operation, amountConvertedText);

            if (confirmTransaction == "Confirm") 
            {
                //Proceed with deposit
                ProceedOperation(operation, transaction.code, transaction.amount);
            }
        }

        //Method to handle button "Withdraw"
        private void BtnWithdraw(object sender, RoutedEventArgs e)
        {

            //Check if input is valid, if not, then return
            if (!ValidateInput()) return;

            //Collect transaction details to pass to ConfirmTransaction method
            var transaction = TransactionDetails();
            string operation = btnWithdraw.Content.ToString();

            //Check if it's possible to withdraw the amount
            if (accountController.CanWithdraw(transaction.code, transaction.amount))
            {
                //Collect the option selected
                string confirmTransaction = ConfirmTransaction(operation, amountConvertedText);

                if (confirmTransaction == "Confirm")
                {
                    //Proceed with withdraw
                    ProceedOperation(operation, transaction.code, transaction.amount);
                }
            }
            //If not, then display alert to the client
            else
            {
                DisplayAlert(transaction.amount);
                ClearTexts();
            }
        }
        
        private void ProceedOperation(string operation, int code, double amount)
        {
            //Check operation and call account controller to deposit or withdraw
            switch(operation)
            {
                case "Deposit":
                    accountController.Deposit(code, amount);
                    break;
                case "Withdraw":
                    accountController.Withdraw(code, amount);
                    break;
            }

            //Clear all texts
            ClearTexts();

            //Update info on the screen
            DisplayAccountDetails();
        }
        
        //Method to display an alert to the client
        //when tries to withdraw an amount higher than the balance
        private void DisplayAlert(double amount)
        {
            //Format amount converted to be displayed on the popup
            amountConvertedText = $"${amount} CAD";

            string code = cmbCurrency.SelectedItem.ToString();
            string message;

            //If code is not CAD, then format message
            if (code != "CAD") message = $"{originalAmountText} = {amountConvertedText}";
            else message = amountConvertedText;

            //Instantiate and open popup
            AlertPopup popup = new AlertPopup(message);
            popup.ShowDialog();
        }

        //Method to limit textbox to only numbers
        private void NumericTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Use a regular expression to check if the input is numeric
            e.Handled = !IsTextNumeric(e.Text);
        }

        //Method to check if character is allowed or not
        private static bool IsTextNumeric(string text)
        {
            // Regex to allow numeric values with optional decimal point
            return System.Text.RegularExpressions.Regex.IsMatch(text, "^[0-9]*(\\.[0-9]*)?$");
        }

        //Method to check if the amount input is not empty or zero
        private bool ValidateInput()
        {
            if (amountInput.Text == null ||
                amountInput.Text == String.Empty ||
                Convert.ToDouble(amountInput.Text) == 0) return false;
            return true;
        }

        //Method to collect amount input and return it converted to CAD
        private Double ConvertInput()
        {

            double amount;
            
            //Try to convert amount to double
            try
            {
                amount = Convert.ToDouble(amountInput.Text);
            }
            catch
            {
                return 0;
            }

            //Check code and format text to be displayed
            string code = cmbCurrency.SelectedItem.ToString();
            originalAmountText = $"${amountInput.Text} {code}";

            //Get amount converted to CAD
            double amountConverted = currencyController.ExchangeToCAD(amount, code);
            
            //If code is not CAD, then format amount converted
            if (code != "CAD") amountConvertedText = $"Equals ${amountConverted} CAD";
            else amountConvertedText = String.Empty;

            return amountConverted;

        }

        //Method to refresh info to be displayed on the screen when switching accounts
        private void CmbAccountSelection(object sender, SelectionChangedEventArgs e)
        {
            DisplayAccountDetails();
        }
    }
}