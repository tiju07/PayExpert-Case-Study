﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayXpert.exception;
using PayXpert.util;

namespace PayXpert.dao
{
    internal class TaxService : ITaxService
    {
        public void CalculateTax(int employeeId, int taxYear)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionString();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }

                DatabaseContext.GetDataFromDB($"SELECT * FROM Employee WHERE EmployeeID = {employeeId}", conn, "", false);
                if (taxYear > DateTime.Now.Year) { throw new InvalidInputException("Invalid year! Enter a year less than or equal to the current year."); }

                string q = $"SELECT TaxAmount FROM Tax WHERE EmployeeID={employeeId} AND TaxYear={taxYear}";
                SqlCommand cmd = new SqlCommand(q, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (!dr.HasRows) { throw new TaxCalculationException("Could not find any details for the given data!"); }
                else
                {
                    while (dr.Read())
                    {
                        Console.WriteLine($"TaxAmount for employee with ID {employeeId} for year {taxYear} is ₹{dr.GetValue(0)}");
                    }
                    dr.Close();
                }
            }
            catch (TaxCalculationException tcex) { Console.WriteLine(tcex.Message); throw new Exception(tcex.Message); }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); throw new Exception(dbcex.Message); }
            catch (InvalidInputException iiex) { Console.WriteLine(iiex.Message); throw new Exception(iiex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetTaxById(int taxId)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionString();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT TaxAmount FROM Tax WHERE TaxID={taxId}";
                SqlCommand cmd = new SqlCommand(q, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"TaxAmount for Tax ID \"{taxId}\" is {string.Format(new CultureInfo("en-US"), "{0:C}", dr.GetValue(0))}");
                }
                dr.Close();
            }
            catch (TaxCalculationException tcex) { Console.WriteLine(tcex.Message); throw new Exception(tcex.Message); }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); throw new Exception(dbcex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetTaxesForEmployee(int employeeId)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionString();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT TaxAmount FROM Tax WHERE EmployeeID={employeeId}";
                DatabaseContext.GetDataFromDB(q, conn, $"Tax Information for employee with ID: {employeeId}", true);
            }
            catch (TaxCalculationException tcex) { Console.WriteLine(tcex.Message); throw new Exception(tcex.Message); }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); throw new Exception(dbcex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }

        public void GetTaxesForYear(int taxYear)
        {
            SqlConnection conn = null!;
            try
            {
                conn = DBConnUtil.ReturnConnectionString();
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) { throw new DatabaseConnectionException("Could not connect to the database!"); }
                string q = $"SELECT * FROM Tax WHERE TaxYear={taxYear}";
                DatabaseContext.GetDataFromDB(q, conn, $"Tax Information for the year: {taxYear}", true);
            }
            catch (TaxCalculationException tcex) { Console.WriteLine(tcex.Message); throw new Exception(tcex.Message); }
            catch (DatabaseConnectionException dbcex) { Console.WriteLine(dbcex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw new Exception(ex.Message); }
            finally { conn.Close(); }
        }
    }
}