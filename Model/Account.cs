using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAssignment.Model
{
    public class Account
    {
        public int AccountID { get; private set; }
        public int ClientID { get; private set; }
        public double Balance { get; private set; }

        public Account(int accountID, int clientID, double initialBalance) 
        { 
            AccountID = accountID;
            ClientID = clientID;
            Balance = initialBalance;
        }

        public void ChangeBalance(double balance)
        { 
            Balance = balance;
        }
    }
}
