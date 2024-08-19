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
        public string AccountHolder { get; private set; }
        public double Balance { get; private set; }

        public Account(int id, string accountHolder, double initialBalance) 
        { 
            AccountID = id;
            AccountHolder = accountHolder;
            Balance = initialBalance;
        }

        public void ChangeBalance(double balance)
        { 
            Balance = balance;
        }
    }
}
