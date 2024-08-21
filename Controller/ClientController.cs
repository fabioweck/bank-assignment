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
        //Create a list of IPerson (clients)
        public List<IPerson> Clients { get; private set; }

        public ClientController() 
        {
            //Instantiate list of clients
            Clients = new List<IPerson>() 
            { 
                new Client(123, "John Smith", "123 Place, Red Deer, AB, T0Y U1O", "jsmith@email.com", "587-890-0987"),
                new Client(234, "Mary Doe", "234st SW, Red Deer, AB, T1Y U20", "maryD@gmail.com", "403-505-8765"),
                new Client(345, "Roger Smith", "345st NE, Red Deer, AB, T2Y U30", "roger.smith@email.com", "987-098-1234")
            };
        }

        //Method to return the account holder name of a given client ID
        public string GetClientName(int clientID)
        {
            return Clients.First(c => c.Id == clientID).Name;
        }
    }
}
