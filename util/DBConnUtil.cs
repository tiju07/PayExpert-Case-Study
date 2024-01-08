using System;
using System.Data.SqlClient;


namespace PayXpert.util
{
    public class DBConnUtil
    {
        public static SqlConnection ReturnConnectionString()
        {
            string connString = util.DBPropertyUtil.GetConnectionString();
            return new SqlConnection(connString);
        }
    }
}
