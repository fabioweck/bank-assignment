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
        public List<IAccount> Accounts { get; private set; }

        public AccountController()
        {
            Accounts = new List<IAccount>() 
            { 
                new Account(1001, 123, 1000),
                new Account(1002, 123, 1500),
                new Account(2001, 234, 900),
                new Account(2002, 234, 1600),
                new Account(3001, 345, 2000),
            };
        }

        public IEnumerable<int> GetCheckingAccountIDs(int clientID)
        {
            return Accounts.OfType<Account>()
                           .Where(a => a.ClientID == clientID)
                           .Select(a => a.AccountID);
        }

        public double GetBalance(int id)
        {
            return Accounts.First(a => a.AccountID == id).Balance; 
        }

        public void Deposit(int id, double amount)
        {
            IAccount account = Accounts.First(a => a.AccountID == id);
            double newBalance = account.Balance + amount;
            account.ChangeBalance(newBalance);
        }

        public bool CanWithdraw(int id, double amount)
        {
            IAccount account = Accounts.First(a => a.AccountID == id);
            return amount <= account.Balance;
        }

        public void Withdraw(int id, double amount)
        {
            IAccount account = Accounts.First(a => a.AccountID == id);
            double newBalance = account.Balance - amount;
            account.ChangeBalance(newBalance);
        }
    }
}
