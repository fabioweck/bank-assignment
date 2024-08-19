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

        public TransactionPopup(string operation, string rate)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;     
            btnOk.Content = operation;
            txtTransaction.Text = rate;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Option?.Invoke("Ok");
            this.Close();
        }
    }
}
