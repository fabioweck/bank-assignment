using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAssignment.Model
{
    public class Client
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public List<Account> Accounts { get; private set; }

        public Client(int id, string name, string address, List<Account> accounts) 
        { 
            Id = id;
            Name = name;
            Address = address;
            Accounts = accounts;
        }

        public void UpdateAccounts(List<Account> accounts)
        {
            Accounts = accounts;
        }
    }
}
