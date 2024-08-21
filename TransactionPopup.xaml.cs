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
using System.Windows.Shapes;

namespace BankAssignment
{
    /// <summary>
    /// Interaction logic for TransactionPopup.xaml
    /// </summary>
    public partial class TransactionPopup : Window
    {
        //Delegate to return if the operation is confirmed
        public event Action<string> Option;

        public TransactionPopup(string operation, string originalAmount, string convertedAmount, string rate)
        {
            InitializeComponent();

            //Centers the popup on the screen
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;   
            
            //Defines the icon and texts to be displayed
            SetIcon(operation);
            SetTexts(operation, originalAmount, convertedAmount, rate);
            
        }

        //Method to determine which icon must be displayed based on operation (deposit or withdraw)
        private void SetIcon(string operation) 
        {
            //Locate resource (icon file) based on operation
            Uri uri = new Uri($"pack://application:,,,/Resources/Icons/{operation}.png", UriKind.RelativeOrAbsolute);

            BitmapImage bitmap = new BitmapImage(uri);
            imgIcon.Source = bitmap;
        }

        //Method to set all texts that are displayed on the popup
        private void SetTexts(string operation, string originalAmount, string convertedAmount, string rate)
        {
            lblOriginalAmount.Content = originalAmount;
            lblConvertedAmount.Content = convertedAmount;
            lblOperation.Content = operation;
            lblTransaction.Content = rate;
        }

        //Method to handle button "Cancel"
        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Option?.Invoke("Cancel");
            this.Close();
        }

        //Method to handle button "Confirm"
        private void BtnConfirm(object sender, RoutedEventArgs e)
        {
            Option?.Invoke("Confirm");
            this.Close();
        }
    }
}
