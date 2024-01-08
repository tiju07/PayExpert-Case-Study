using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.exception
{
    internal class FinancialRecordException : Exception
    {
        public FinancialRecordException(string message) : base(message) { }
    }
}
