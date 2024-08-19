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

        public List<Account> account;

        public AccountController()
        {
            account = new List<Account>() 
            { 
                new Account() { accountID = 1001, clientName = "John Doe", balance = 1000 },
                new Account() { accountID = 1002, clientName = "John Doe", balance = 1000 }
            };
        }

        public double GetBalance()
        {
            return account[0].balance; 
        }

        public void Deposit(double amount)
        {
            account[0].balance += amount;
        }

        public bool CanWithdraw(double amount)
        {
            double balance = account[0].balance;
            return balance - amount >= 0;
        }

        public void Withdraw(double amount)
        {
            account[0].balance -= amount;
        }
    }
}
