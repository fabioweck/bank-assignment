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
        public List<Currency> currencies;

        public CurrencyController()
        {
            currencies = new List<Currency>()
            {
                new Currency { Code = "CAD", Rate = 1},
                new Currency { Code = "USD", Rate = 0.5},
                new Currency { Code = "MXN", Rate = 10},
                new Currency { Code = "EURO", Rate = 0.25},
            };
        }

        public IEnumerable<string> GetCodes()
        {
            return currencies.Select(c => c.Code);
        }

        public Double GetRate(string code)
        {
            return currencies.First(c => c.Code == code).Rate;
        }
    }
}
