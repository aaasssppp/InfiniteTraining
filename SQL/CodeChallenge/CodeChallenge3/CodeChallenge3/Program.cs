using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient;

namespace CodeChallenge3
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
                "Initial Catalog=InfiniteDB;" +
                "user id=sa; password=Developer@123"); // give the credetials for connection. Be careful inside quotes coz the intecins cannot show any error
                                                       // Initial Catalag is similar to USE
                                                       // data source (or) database
                                                       // for windows authentication: Integrated Security = true (or) Trusted_connection = true
            con.Open(); // connection road is created, you have to open it
            return con;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(hi);
        }
    }

}
