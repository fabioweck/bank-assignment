﻿using System;
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
    /// Interaction logic for AlertPopup.xaml
    /// </summary>
    public partial class AlertPopup : Window
    {
        public AlertPopup(string message)
        {
            InitializeComponent();

            //Centers the popup on the screen
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Set the message on the popup
            lblAmount.Content = message;
        }

        //Method to handle button "Close"
        private void BtnClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
