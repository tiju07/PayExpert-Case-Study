﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.exception
{
    internal class PayrollGenerationException : Exception
    {
        public PayrollGenerationException(string message) : base(message) { }
    }
}
