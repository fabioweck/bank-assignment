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
        public List<Currency> Currencies { get; private set; }

        public CurrencyController()
        {
            Currencies = new List<Currency>()
            {
                new Currency ("CAD", 1),
                new Currency ("USD", 0.5),
                new Currency ("MXN", 10),
                new Currency ("EURO", 0.25),
            };
        }

        public IEnumerable<string> GetCodes()
        {
            return Currencies.Select(c => c.Code);
        }

        public Double GetRate(string code)
        {
            return Currencies.First(c => c.Code == code).Rate;
        }

        public Double ExchangeToCAD(double amount, string code)
        {
            double rate = GetRate(code);
            if (rate == 1) return amount;
            return amount / rate;
        }
    }
}
