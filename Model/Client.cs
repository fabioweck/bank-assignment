using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAssignment.Model
{
    //Interface to define common properties of individuals
    //from a bank environment (clients, managers, office staff,...)

    public interface IPerson
    {
        public int Id { get; }
        public string Name { get; }
        public string Address { get; }
        public string Email { get; }
        public string Phone { get; }
    }

    public class Client: IPerson
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public Client(
            int id, 
            string name, 
            string address,
            string email,
            string phone) 
        { 
            Id = id;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
        }

        public void UpdateInfo(
            int id,
            string name,
            string address,
            string email,
            string phone)
        {
            Id = id;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
        }
    }
}
