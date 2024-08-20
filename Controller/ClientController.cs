using BankAssignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAssignment.Controller
{
    public class ClientController
    {

        public List<Client> Clients { get; private set; }

        public ClientController() 
        {
            Clients = new List<Client>() 
            { 
                new Client(123, "John Smith", "123 Place, Red Deer, AB, T0Y U1O"),
                new Client(234, "Mary Doe", "234st SW, Red Deer, AB, T1Y U20"),
                new Client(345, "Roger Smith", "345st NE, Red Deer, AB, T2Y U30")
            };
        }

        public string GetClientName(int clientID)
        {
            return Clients.First(c => c.Id == clientID).Name;
        }
    }
}
