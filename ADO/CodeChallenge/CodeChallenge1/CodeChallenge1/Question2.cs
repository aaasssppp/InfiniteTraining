using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CodeChallenge1
{
    class Program
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
        static void Main()
        {
            con = GetConnection();

            Console.Write("Enter the Employee Id to be updated: ");
            int empId = Convert.ToInt32(Console.ReadLine());

            // Call the procedure to update salary
            SqlCommand updateCmd = new SqlCommand("UpdateEmployeeSalary", con);
            updateCmd.CommandType = CommandType.StoredProcedure;

            updateCmd.Parameters.AddWithValue("@EmpId", empId);

            SqlParameter updatedSalary = new SqlParameter();
            updatedSalary.ParameterName = "@UpdatedSalary";
            updatedSalary.DbType = DbType.Decimal;
            updatedSalary.Direction = ParameterDirection.Output;
            updateCmd.Parameters.Add(updatedSalary);

            updateCmd.ExecuteNonQuery();

            decimal updSal = (decimal)updatedSalary.Value;
            Console.WriteLine($"Updated Salary for EmpId {empId}: {updSal}");

            // employee details
            SqlCommand getEmpCmd = new SqlCommand("SELECT EmpId, Name, Salary, Gender, NetSalary FROM Employee_Details WHERE EmpId = @EmpId", con);
            getEmpCmd.Parameters.AddWithValue("@EmpId", empId);

            SqlDataReader reader = getEmpCmd.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine("\n--- Employee Details ---");
                Console.WriteLine($"EmpId : {reader["EmpId"]}");
                Console.WriteLine($"Name : {reader["Name"]}");
                Console.WriteLine($"Gender : {reader["Gender"]}");
                Console.WriteLine($"Salary : {reader["Salary"]}");
                Console.WriteLine($"NetSalary : {reader["NetSalary"]}");
            }
            else
            {
                Console.WriteLine($"No employee found with EmpId {empId}");
            }

            Console.Write("\nPress Enter to exit: ");
            Console.ReadLine();
        }
    }
}


