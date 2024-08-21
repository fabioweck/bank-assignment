using BankAssignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAssignment.Controller
{
    public class AccountController
    {
        //Create a list of accounts
        public List<IAccount> Accounts { get; private set; }

        public AccountController()
        {
            //Instantiate list of account
            Accounts = new List<IAccount>() 
            { 
                new Account(1001, 123, 1000),
                new Account(1002, 123, 1500),
                new Account(2001, 234, 900),
                new Account(2002, 234, 1600),
                new Account(3001, 345, 2000),
            };
        }

        //Method to return list of account IDs of a given client
        public IEnumerable<int> GetCheckingAccountIDs(int clientID)
        {
            //OfType<Account> is required to return only checking accounts
            return Accounts.OfType<Account>() 
                           .Where(a => a.ClientID == clientID)
                           .Select(a => a.AccountID);
        }

        //Method to return the balance of an account
        public double GetBalance(int id)
        {
            return Accounts.First(a => a.AccountID == id).Balance; 
        }

        //Method to make a deposit in a given account
        public void Deposit(int id, double amount)
        {
            //Get account object and pass the new balance to be updated
            IAccount account = Accounts.First(a => a.AccountID == id);
            double newBalance = account.Balance + amount;
            account.ChangeBalance(newBalance);
        }

        //Method to check if the amount to be withdraw is higher than the balance or not
        public bool CanWithdraw(int id, double amount)
        {
            IAccount account = Accounts.First(a => a.AccountID == id);
            return amount <= account.Balance;
        }

        //Method to withdraw from a given account
        public void Withdraw(int id, double amount)
        {
            //Get account object and pass the new balance to be updated
            IAccount account = Accounts.First(a => a.AccountID == id);
            double newBalance = account.Balance - amount;
            account.ChangeBalance(newBalance);
        }
    }
}
