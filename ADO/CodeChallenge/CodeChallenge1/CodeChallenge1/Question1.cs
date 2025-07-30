using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CodeChallenge1
{
    class Question1
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlDataReader dr;
        // common function to get the connection
        static SqlConnection GetConnection() // func return sqlconnection
        {
            con = new SqlConnection(@"Data Source=ICS-LT-4FK96V3\SQLEXPRESS; " +
                "Initial Catalog=CodeChallenge;" +
                "user id=sa; password=Developer@123");
            con.Open(); // connection road is created, you have to open it
            return con;
        }
        static void Main(string[] args)
        {
            con = GetConnection();
            SqlCommand cmd = new SqlCommand("InsertEmployeeDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Input parameters
            cmd.Parameters.AddWithValue("@Name", "Syam");
            cmd.Parameters.AddWithValue("@Salary", 5000);
            cmd.Parameters.AddWithValue("@Gender", "Male");

            SqlParameter LastEmpId = new SqlParameter();
            LastEmpId.ParameterName = "LastEmpId";
            LastEmpId.DbType = DbType.Int32;
            LastEmpId.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(LastEmpId);

            cmd.ExecuteNonQuery();

            int empId = (int)LastEmpId.Value;

            // Retrieve the NetSalary after insert
            SqlCommand getNetSalaryCmd = new SqlCommand("SELECT NetSalary FROM Employee_Details WHERE EmpId = @EmpId", con);
            getNetSalaryCmd.Parameters.AddWithValue("@EmpId", empId);

            decimal netSalary = (decimal)getNetSalaryCmd.ExecuteScalar();

            Console.WriteLine($"Inserted EmpId: {empId}");
            Console.WriteLine($"Calculated NetSalary: {netSalary}");

            Console.Write("\nPress Enter to exit: ");
            Console.ReadLine();
        }
    }
}

