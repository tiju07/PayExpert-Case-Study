using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayXpert.exception;
using PayXpert.util;

namespace PayXpert.dao
{
    internal class PayrollService : IPayrollService
    {
        public void GeneratePayroll(int employeeID, DateTime startDate, DateTime endDate)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionString();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = "SELECT BasicSalary, OvertimePay, Deductions FROM Payroll WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                SqlDataReader dr = cmd.ExecuteReader();
                Console.WriteLine($"Payroll for employee with ID: {employeeID}");

                int months = ((TimeSpan) (endDate - startDate)).Days / 30;

                while (dr.Read())
                {
                    decimal totPay = ((decimal)dr.GetValue(0) * months) + (decimal)dr.GetValue(1) - (decimal)dr.GetValue(2);
                    Console.WriteLine($"Details: \nBasic Salary: {dr.GetValue(0)} \t Overtime Pay: {dr.GetValue(1)} \t Deductions: {dr.GetValue(2)}");
                    Console.WriteLine($"Total Pay: {totPay}");
                }
                dr.Close();
            }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (PayrollGenerationException pgex) {  Console.WriteLine(pgex.Message); throw new Exception(pgex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetPayrollById(int payrollID)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionString();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM Payroll WHERE PayrollID = {payrollID}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the payroll details for ID: {payrollID}", true);
            }catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); throw new Exception(pgex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetPayrollsForEmployee(int employeeID)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionString();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM Payroll WHERE EmployeeID = {employeeID}";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the payrolls for the employee with ID: {employeeID}", true);
            } 
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); throw new Exception(pgex.Message); }
            catch (EmployeeNotFoundException enfex) { Console.WriteLine(enfex.Message); throw new Exception(enfex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionString();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string sd = startDate.Year.ToString() + '-' + startDate.Month.ToString() + '-' + startDate.Day.ToString();
                string ed = endDate.Year.ToString() + '-' + endDate.Month.ToString() + '-' + endDate.Day.ToString();

                string q = $"SELECT * FROM Payroll where PayPeriodStartDate >= \'{sd}\' and PayPeriodEndDate < \'{ed}\'";
                DatabaseContext.GetDataFromDB(q, conn, $"Following are the payrolls between {sd} and {ed}", true);
            }catch(DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (PayrollGenerationException pgex) { Console.WriteLine(pgex.Message); throw new Exception(pgex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }
    }
}
