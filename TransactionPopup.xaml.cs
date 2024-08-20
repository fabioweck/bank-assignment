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

        public event Action<string> Option;

        public TransactionPopup(string operation, string originalAmount, string convertedAmount, string rate)
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;   
            
            SetIcon(operation);

            lblOriginalAmount.Content = originalAmount;
            lblConvertedAmount.Content = convertedAmount;
            lblOperation.Content = operation;
            lblTransaction.Content = rate;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Option?.Invoke("Confirm");
            this.Close();
        }

        private void SetIcon(string operation) 
        {
            Uri uri = new Uri($"pack://application:,,,/Resources/Icons/{operation}.png", UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(uri);
            imgIcon.Source = bitmap;
        }
    }
}
