using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject
{
    public static class DBHelper
    {
        public static string ConnString = "Data Source=YOUR_SERVER;Initial Catalog=RailwayDB;Integrated Security=True";
    }
    class Program
    {
        // Connected ADO.NET Example – Login Check
        public bool AdminLogin(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.ConnString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username=@u AND Password=@p AND Role='Admin'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Connected ADO.NET Example – Booking a Ticket
        public void BookTicket(int customerId, int trainNo, string travelClass, DateTime travelDate)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.ConnString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // 1. Check availability
                    string checkQuery = $"SELECT Class1Max FROM TrainDetails WHERE TrainNo=@trainNo AND IsActive=1";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con, transaction);
                    checkCmd.Parameters.AddWithValue("@trainNo", trainNo);
                    int seats = (int)checkCmd.ExecuteScalar();

                    if (seats > 0)
                    {
                        // 2. Update seat count
                        string updateQuery = $"UPDATE TrainDetails SET Class1Max = Class1Max - 1 WHERE TrainNo=@trainNo";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, con, transaction);
                        updateCmd.Parameters.AddWithValue("@trainNo", trainNo);
                        updateCmd.ExecuteNonQuery();

                        // 3. Insert into Reservation table
                        string insertQuery = "INSERT INTO Reservation (CustomerId, TrainNo, DateOfTravel, Class, SeatNo, TotalCost) " +
                                             "VALUES (@cid, @tno, @dot, @cls, @seat, @cost)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, con, transaction);
                        insertCmd.Parameters.AddWithValue("@cid", customerId);
                        insertCmd.Parameters.AddWithValue("@tno", trainNo);
                        insertCmd.Parameters.AddWithValue("@dot", travelDate);
                        insertCmd.Parameters.AddWithValue("@cls", travelClass);
                        insertCmd.Parameters.AddWithValue("@seat", seats); // last seat number
                        insertCmd.Parameters.AddWithValue("@cost", 2500); // Example cost
                        insertCmd.ExecuteNonQuery();

                        transaction.Commit();
                        Console.WriteLine("✅ Ticket booked successfully!");
                    }
                    else
                    {
                        Console.WriteLine("❌ No seats available!");
                        transaction.Rollback();
                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        //  Disconnected ADO.NET Example – View All Trains
        public DataTable GetActiveTrains()
        {
            using (SqlConnection con = new SqlConnection(DBHelper.ConnString))
            {
                string query = "SELECT TrainNo, TrainName, FromStation, ToStation, Class1Max, Class2Max, Class3Max FROM TrainDetails WHERE IsActive=1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        //  Disconnected ADO.NET Example – Update Train Cost in Bulk
        public void UpdateTrainCosts(DataTable trains)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.ConnString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TrainDetails", con);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(trains);
            }
        }
        



        static void Main(string[] args)
        {   
        }
    }
}
