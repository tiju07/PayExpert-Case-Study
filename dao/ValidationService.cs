using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace PayXpert.dao 
{

    internal class ValidationService
    {
        private static List<string> genders = new List<string>() { "Male", "Female", "Others" };
        /// <summary>
        /// Validation for the function AddEmployee in EmployeeService class
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="Gender"></param>
        /// <param name="Email"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="Address"></param>
        /// <param name="Designation"></param>
        /// <param name="JoiningDate"></param>
        /// <param name="TerminationDate"></param>
        /// <returns></returns>
        public static bool AddEmployeeValidation(string firstName, string lastName, DateTime DateOfBirth, string Gender, string Email, string PhoneNumber, string Address, string Designation, DateTime JoiningDate, DateTime? TerminationDate)
        {

            List<string> genders = new List<string>() { "Male", "Female", "Others" };

            if (!Regex.IsMatch(firstName, "([a-zA-Z])")) { Console.WriteLine("firstname"); return false;  }

            if (!Regex.IsMatch(lastName, "([a-zA-Z])")) { Console.WriteLine("lastname"); return false; }

            if (!genders.Contains(Gender)) { Console.WriteLine("gender"); return false; }

            if (!Regex.IsMatch(Email, "^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$")) { Console.WriteLine("email"); return false; }

            if (!Regex.IsMatch(PhoneNumber, "\\+([0-9]{12,})")) { Console.WriteLine("phonenumber"); return false; }

            if (Address.Length < 3) { Console.WriteLine("address"); return false; }
            if (Designation.Length < 3) { Console.WriteLine("designation"); return false; }
            //For the DateOnly variables, the validation will depend on how input is taken.

            return true;
        }

        /// <summary>
        /// Validation for the function UpdateEmployee in the EmployeeService class
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="Gender"></param>
        /// <param name="Email"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="Address"></param>
        /// <param name="Designation"></param>
        /// <param name="JoiningDate"></param>
        /// <param name="TerminationDate"></param>
        /// <returns></returns>
        public static bool UpdateEmployeeValidation(string? firstName, string? lastName, DateTime? DateOfBirth, string? Gender, string? Email, string? PhoneNumber, string? Address, string? Designation, DateTime? JoiningDate, DateTime? TerminationDate)
        {
            if (firstName != null && !Regex.IsMatch(firstName, "([a-zA-Z])")) { return false; }

            if (lastName != null && !Regex.IsMatch(lastName, "([a-zA-Z])")) { return false; }

            if (Gender != null && !genders.Contains(Gender)) { return false; }

            if (Email != null && !Regex.IsMatch(Email, "^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$")) { return false; }

            if (PhoneNumber != null && !Regex.IsMatch(PhoneNumber, "\\+([0-9]{12,})")) { return false; }

            if (Address != null && Address.Length < 3) { return false; }

            if (Designation != null && Designation.Length < 3) { return false; }
            //For the DateOnly variables, the validation will depend on how input is taken.

            return true;
        }

        /// <summary>
        /// Validation for the function AddFinancialRecord in the FinancialRecordService class
        /// </summary>
        /// <param name="recordDate"></param>
        /// <param name="description"></param>
        /// <param name="amount"></param>
        /// <param name="recordType"></param>
        /// <returns></returns>
        public static bool AddFinancialRecordValidation(int recordDate, string description, double amount, string recordType)
        {
            //All permissible record types
            List<string> recordTypes = ["expense", "income", "tax payment"];

            if (recordDate > (int)DateTime.Now.Year) return false;
            if (!Regex.IsMatch(description, "([a-zA-Z]+)([0-9]*)")) { return false; }
            if(amount < 0) return false;
            if (!recordTypes.Contains(recordType.ToLower())) return false;

            return true;
        }
    }
}