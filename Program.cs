using PayXpert.dao;
using PayXpert.exception;
namespace PayXpert
{
    internal class Program
    {
        static void Main(string[] args)
        {

            EmployeeService employeeService = new EmployeeService();
            PayrollService payrollService = new PayrollService();
            TaxService taxService = new TaxService();
            FinancialRecordService financialRecordService = new FinancialRecordService();
            bool mainFlag = false;
            int choice;
            do
            {
                Console.WriteLine("\nEnter an appropriate choice from below:\n" +
                "1. Get an employee using ID\n" +
                "2. Get all employees\n" +
                "3. Add en employee\n" +
                "4. Update an existing employee\n" +
                "5. Calculate age of an employee\n" +
                "6. Remove an employee\n" +
                "7. Generate Payroll for an employee\n" +
                "8. Get Payroll using a specific ID\n" +
                "9. Get Payrolls for a specific employee\n" +
                "10. Get Payrolls for a specific period\n" +
                "11. Calculate Tax for an employee for a specific year\n" +
                "12. Get Taxes for a specific ID\n" +
                "13. Get Taxes for a specific employee\n" +
                "14. Get Taxes for a specific year\n" +
                "15. Add a Financial Record\n" +
                "16. Get a Financial Record by it's ID\n" +
                "17. Get Financial Records for a specific employee\n" +
                "18. Get Financial Records for a specific year\n" + 
                "0. Exit the menu");
                while (!int.TryParse(Console.ReadLine(), out choice) && (choice <= 0 || choice >=18))
                {
                    Console.WriteLine("Invalid choice. Try again!");
                }
                switch (choice)
                {
                    case 1:
                        Console.Write("\nEnter the ID of the employee:");
                        int id;
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.Write("\nWrong entry! This field only accepts integer inputs.");
                        }
                        employeeService.GetEmployeeById(id);
                        break;
                    case 2:
                        employeeService.GetAllEmployees();
                        break;
                    case 3:
                        bool flag = true;
                        do
                        {
                            mainFlag = false;
                            try
                            {
                                Console.Write("\nEnter details for the employee:");
                                Console.Write("Enter First Name: ");
                                string firstName = Console.ReadLine().ToString();
                                Console.Write("\nEnter Last Name");
                                string lastName = Console.ReadLine().ToString();
                                DateTime dateOfBirth = DateTime.MaxValue;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Date of Birth in \"YYYY-MM-DD\" format:");
                                    try
                                    {
                                        dateOfBirth = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                Console.Write("\nEnter gender of the employee: ");
                                string gender = Console.ReadLine();
                                Console.Write("\nEnter Email ID of the employee: ");
                                string email = Console.ReadLine();
                                Console.Write("\nEnter phone number of the employee");
                                string phoneNumber = Console.ReadLine();
                                Console.Write("\nEnter address of the employee: ");
                                string address = Console.ReadLine();
                                Console.Write("\nEnter designation of the employee: ");
                                string designation = Console.ReadLine();
                                DateTime joiningDate = DateTime.MaxValue;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Joining Date of the employee in \"YYYY-MM-DD\" format:");
                                    try
                                    {
                                        joiningDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                DateTime? terminationDate = null;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Termination Date of the employee in \"YYYY-MM-DD\" format, \"-\" if not available:");
                                    var temp = Console.ReadLine();
                                    if (temp != "-")
                                    {
                                        try
                                        {
                                            terminationDate = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                            flag = false;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                    }
                                    else
                                    {
                                        terminationDate = null;
                                        break;
                                    }
                                } while (flag);
                                employeeService.AddEmployee(firstName, lastName, dateOfBirth, gender, email, phoneNumber, address, designation, joiningDate, terminationDate);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 4:
                        int employeeID;
                        flag = true;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter details to update for the employee(If you do not wish to update a specific information, just type \"-\"):");
                                Console.Write("Enter the ID of the employee which needs to be updated: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    throw new InvalidInputException("Invalid ID! Please recheck and try again.");
                                }
                                Console.Write("\nEnter First Name: ");
                                string? temp = Console.ReadLine();
                                string? firstName = temp != "-" ? temp : null;
                                Console.Write("\nEnter Last Name");
                                temp = Console.ReadLine();
                                string? lastName = temp != "-" ? temp : null;
                                DateTime? dateOfBirth = null;
                                do
                                {
                                    Console.Write("\nEnter Date of Birth of the employee in \"YYYY-MM-DD\" format, \"-\" if no update is required:");
                                    temp = Console.ReadLine();
                                    if (temp != "-")
                                    {
                                        try
                                        {
                                            dateOfBirth = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                            flag = false;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                    }
                                    else { flag = false; }
                                } while (flag);
                                Console.Write("\nEnter gender of the employee: ");
                                temp = Console.ReadLine();
                                string? gender = temp != "-" ? temp : null;
                                Console.Write("\nEnter Email ID of the employee: ");
                                temp = Console.ReadLine();
                                string? email = temp != "-" ? temp : null;
                                Console.Write("\nEnter phone number of the employee");
                                temp = Console.ReadLine();
                                string? phoneNumber = temp != "-" ? temp : null;
                                Console.Write("\nEnter address of the employee: ");
                                temp = Console.ReadLine();
                                string? address = temp != "-" ? temp : null;
                                Console.Write("\nEnter designation of the employee: ");
                                temp = Console.ReadLine();
                                string? designation = temp != "-" ? temp : null;
                                DateTime? joiningDate = null;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Joining Date of the employee in \"YYYY-MM-DD\" format, \"-\" if no update is required:");
                                    temp = Console.ReadLine();
                                    if (temp != "-")
                                    {
                                        try
                                        {
                                            joiningDate = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                            flag = false;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                    } else { flag = false; }
                                } while (flag);
                                DateTime? terminationDate = null;
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Termination Date of the employee in \"YYYY-MM-DD\" format, \"-\" if not available:");
                                    temp = Console.ReadLine();
                                    if (temp != "-")
                                    {
                                        try
                                        {
                                            terminationDate = DateTime.ParseExact(temp, "yyyy-MM-dd", null);
                                            flag = false;
                                        }
                                        catch (ArgumentNullException anex)
                                        {
                                            Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                        }
                                        catch (FormatException fex)
                                        {
                                            Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                        }
                                    }
                                    else if (temp == "")
                                    {
                                        terminationDate = null;
                                    }
                                    else { flag = false; }
                                } while (flag);
                                employeeService.UpdateEmployee(employeeID, firstName, lastName, dateOfBirth, gender, email, phoneNumber, address, designation, joiningDate, terminationDate);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 5:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("Enter the ID of the employee whose age is to be calculated: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    throw new InvalidDataException("Invalid ID. Please try again.");
                                }
                                employeeService.CalculateAge(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 6:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter ID of employee to delete: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID)) throw new Exception("Invalid Input! Try again.");
                                employeeService.RemoveEmployee(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 7:
                        DateTime startDate = DateTime.MinValue;
                        DateTime endDate = DateTime.MaxValue;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter ID of employee whose payroll is to be generated: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID)) throw new Exception("Invalid Input! Try again.");
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Start Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                    try
                                    {
                                        startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Start Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                    try
                                    {
                                        endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                payrollService.GeneratePayroll(employeeID, startDate, endDate);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 8:
                        int payrollID;
                        mainFlag = true;
                        do
                        {
                            try
                            {
                                Console.Write("\nEnter ID of the specific payroll: ");
                                if (!int.TryParse(Console.ReadLine(), out payrollID)) throw new Exception("Invalid Input! Try again.");
                                payrollService.GetPayrollById(payrollID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 9:
                        mainFlag = true;
                        do
                        {
                            try
                            {
                                Console.Write("\nEnter ID of employee to get payrolls of: ");
                                if (!int.TryParse(Console.ReadLine(), out employeeID)) throw new Exception("Invalid Input! Try again.");
                                payrollService.GetPayrollsForEmployee(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 10:
                        startDate = DateTime.MinValue;
                        endDate = DateTime.MaxValue;
                        mainFlag = true;
                        do
                        {
                            try
                            {
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter Start Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                    try
                                    {
                                        startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                flag = true;
                                do
                                {
                                    Console.Write("\nEnter End Date to generate payroll from (in \"YYYY-MM-DD\" format): ");
                                    try
                                    {
                                        endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                                        flag = false;
                                    }
                                    catch (ArgumentNullException anex)
                                    {
                                        Console.Write("\nData entered was invalid or null! Please retry by entering correct data.");
                                    }
                                    catch (FormatException fex)
                                    {
                                        Console.Write("\nThe entered data was in wrong format. Please retry by entering data in correct format(YYYY-MM-DD)");
                                    }
                                } while (flag);
                                payrollService.GetPayrollsForPeriod(startDate, endDate);
                                mainFlag = false;
                            } catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 11:
                        int year;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee:");
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                Console.Write("\nEnter year to calculate tax: ");
                                while (!int.TryParse(Console.ReadLine(), out year))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                taxService.CalculateTax(employeeID, year);
                                mainFlag = false;
                            }catch(Exception ex) { mainFlag = true;}
                        }while (mainFlag);
                        break;
                    case 12:
                        int taxID;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID to get tax details of:");
                                while (!int.TryParse(Console.ReadLine(), out taxID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                taxService.GetTaxById(taxID);
                                mainFlag = false;
                            } catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 13:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee to get tax details of:");
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                taxService.GetTaxesForEmployee(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 14:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the year to get tax details of:");
                                while (!int.TryParse(Console.ReadLine(), out year))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                taxService.GetTaxesForYear(year);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 15:
                        int recordDate;
                        string description;
                        double amount;
                        string recordType;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee:");
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                Console.Write("\nEnter the year of the record:");
                                while (!int.TryParse(Console.ReadLine(), out recordDate))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                Console.Write("\nEnter the description for the record:");
                                description = Console.ReadLine();
                                Console.Write("\nEnter the amount of the financial record");
                                while (!double.TryParse(Console.ReadLine(), out amount))
                                {
                                    Console.Write("\nWrong entry! This field only accepts numeric inputs.");
                                }
                                Console.Write("\nEnter the type of the record: ");
                                recordType = Console.ReadLine();
                                financialRecordService.AddFinancialRecord(employeeID, recordDate, description, amount, recordType);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 16:
                        int recordID;
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID to get financial record details of:");
                                while (!int.TryParse(Console.ReadLine(), out recordID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                financialRecordService.GetFinancialRecordById(recordID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 17:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the ID of the employee to get financial record details of:");
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                financialRecordService.GetFinancialRecordsForEmployee(employeeID);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 18:
                        do
                        {
                            mainFlag = true;
                            try
                            {
                                Console.Write("\nEnter the year to get financial record details of:");
                                while (!int.TryParse(Console.ReadLine(), out year))
                                {
                                    Console.Write("\nWrong entry! This field only accepts integer inputs.");
                                }
                                financialRecordService.GetFinancialRecordsForDate(year);
                                mainFlag = false;
                            }
                            catch (Exception ex) { mainFlag = true; }
                        } while (mainFlag);
                        break;
                    case 0:
                        Console.Write("\nExiting...");
                        break;
                    default:
                        Console.Write("\nInvalid choice. Please enter a choice in integer format between 0 and 18.");
                        break;
                }
            } while (choice != 0);


            /*
            SqlConnection conn = util.DBConnUtil.ReturnConnectionString();

            SqlCommand cmd = conn.CreateCommand();

            List<string> list = new List<string>();
            cmd.CommandText = "select FirstName from Employee";
            cmd.Connection = conn;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
            conn.Close();
            
            Console.WriteLine("First names of all employees: ");
            foreach (string item in list)
            {
                Console.WriteLine(item);
            }

            EmployeeService employeeService = new EmployeeService();
            
            employeeService.AddEmployee("Richard", "Jackson", new DateTime(2001, 4, 2), "Male", "richardjk@gmail.com", "+919546857413", "P.O. Box 420, 3564 Lacinia Rd.", "Project Lead", new DateTime(2020, 7, 5), null);
            
            employeeService.AddEmployee("Ray", "Jones", new DateTime(1995, 7, 21), "Male", "rayjones@hotmail.com", "+9176481234", "P.O. Box 420, 3564 Lacinia Rd.", "Systems Engineer", new DateTime(2019, 2, 21), null);
            
            employeeService.GetAllEmployees();
            

            employeeService.GetEmployeeById(2002);
            
            employeeService.RemoveEmployee(2003);
            

            employeeService.UpdateEmployee(2002, "Richard", "Jones", new DateTime(1995, 7, 21), "Male", "richardjones@hotmail.com", "+917648123458", "Ap #176-4381 Sagittis Rd.", "Project Mangager", new DateTime(2019, 02, 21), null);

            PayrollService payrollService = new PayrollService();

            payrollService.GeneratePayroll(2, new DateTime(2020, 8, 4), new DateTime(2021, 8, 10));
            
            payrollService.GetPayrollById(150);
            
            payrollService.GetPayrollsForEmployee(1);
            
            payrollService.GetPayrollsForPeriod(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31));

            TaxService taxService = new TaxService();

            taxService.CalculateTax(1, 2022);

            taxService.GetTaxById(5);

            taxService.GetTaxesForEmployee(2);

            taxService.GetTaxesForYear(2022);
            

            FinancialRecordService frService = new FinancialRecordService();
            frService.AddFinancialRecord(2, 2024, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", 5647.8, "Expense");

            frService.GetFinancialRecordById(2);
            frService.GetFinancialRecordsForDate(2022);
            frService.GetFinancialRecordsForEmployee(5);
            */
        }
    }
}
