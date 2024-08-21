using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAssignment.Model
{
    //Interface to define common properties and methods
    //an account must have
    //(checking, savings, joint, and international accounts)

    public interface IAccount
    {
        public int AccountID { get; }
        public string AccountType { get; }
        public double Balance { get; }
        public void ChangeBalance(double balance);
    }

    //Checking account class
    public class Account: IAccount
    {
        public int AccountID { get; private set; }
        public int ClientID { get; private set; }
        public string AccountType { get; private set; } = "Checking";
        public double Balance { get; private set; }

        public Account(int accountID, int clientID, double initialBalance) 
        { 
            AccountID = accountID;
            ClientID = clientID;
            Balance = initialBalance;
        }

        //Method to update/change account balance
        public void ChangeBalance(double balance)
        {
            Balance = balance;
        }
    }
}
