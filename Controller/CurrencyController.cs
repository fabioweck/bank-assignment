using BankAssignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAssignment.Controller
{
    public class CurrencyController
    {
        //Create a list of currencies
        public List<Currency> Currencies { get; private set; }

        public CurrencyController()
        {
            //Instantie list of currencies
            Currencies = new List<Currency>()
            {
                new Currency ("CAD", 1),
                new Currency ("USD", 0.5),
                new Currency ("MXN", 10),
                new Currency ("EURO", 0.25),
            };
        }

        //Method to return a list of currency codes
        public IEnumerable<string> GetCodes()
        {
            return Currencies.Select(c => c.Code);
        }

        //Method to get the rate of a given code
        public Double GetRate(string code)
        {
            return Currencies.First(c => c.Code == code).Rate;
        }

        //Method to convert a given amount to CAD values
        public Double ExchangeToCAD(double amount, string code)
        {
            //Get rate, and if it's not '1', then return amount converted
            double rate = GetRate(code);
            if (rate == 1) return amount;
            return amount / rate;
        }
    }
}
