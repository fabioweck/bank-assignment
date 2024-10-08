﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAssignment.Model
{
    public class Currency
    {
        public string Code { get; set; }
        public double Rate { get; private set; }

        public Currency(string code, double rate) 
        { 
            Code = code;
            Rate = rate;
        }

        //Method to update rate (not required)
        public void UpdateRate(double rate)
        {
            Rate = rate;
        }
    }
}
