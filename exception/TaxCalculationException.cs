using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.exception
{
    internal class TaxCalculationException : Exception
    {
        public TaxCalculationException(string message) : base(message) { }
    }
}
