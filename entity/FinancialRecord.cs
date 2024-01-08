﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.entity
{
    internal class FinancialRecord
    {
        private int recordID;
        private int employeeID;
        private SqlDateTime recordDate;
        private string description = null!;
        private double amount;
        private string recordType = null!;

        public int RecordID { get => recordID; private set { } }
        public int EmployeeID { get => employeeID; set { employeeID = value; } }
        public SqlDateTime RecordDate { get => recordDate; set { recordDate = value; } }
        public string Description { get => description; set { description = value; } }
        public double Amount { get => amount; set { amount = value; } }
        public string RecordType { get => recordType; set { recordType = value; } }

    }
}
