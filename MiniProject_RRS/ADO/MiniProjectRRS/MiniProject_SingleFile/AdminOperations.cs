using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MiniProjectRRS
{

    public class AdminOperations
    {
        public static void ViewAllTrains()
        {
            Console.Clear();
            Console.WriteLine("===== TRAIN LIST =====");
            Console.WriteLine($"{"TrainNo",-7} | {"TrainName",-20} | {"From",-12} | {"To",-12} | {"1AC Avl",-8} | {"2AC Avl",-8} | {"3AC Avl",-8} | {"Dep Time",-8} | Active");
            Console.WriteLine(new string('-', 100));


            string query = "SELECT TrainNo, TrainName, FromStation, ToStation, Class1Available, Class2Available, Class3Available, DepartureTime, IsActive FROM TrainDetails ORDER BY TrainNo";
            DataTable dt = DBHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No trains found.");
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(
                        $"{row["TrainNo"],-7} | " +
                        $"{row["TrainName"],-20} | " +
                        $"{row["FromStation"],-12} | " +
                        $"{row["ToStation"],-12} | " +
                        $"{row["Class1Available"],-8} | " +
                        $"{row["Class2Available"],-8} | " +
                        $"{row["Class3Available"],-8} | " +
                        $"{TimeSpan.Parse(row["DepartureTime"].ToString()),-8} | " +
                        $"{((bool)row["IsActive"] ? "Yes" : "No")}"
                    );
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
            Console.Clear();
        }

        // 1) Add Train
        public void AddTrain()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== ADD NEW TRAIN =====");
                Console.Write("Train Name: ");
                string name = Console.ReadLine()?.Trim();
                Console.Write("From Station: ");
                string from = Console.ReadLine()?.Trim();
                Console.Write("To Station: ");
                string to = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
                {
                    Console.WriteLine("Name/from/to cannot be empty. Press Enter...");
                    Console.ReadLine();
                    return;
                }
                //prompt for departure time
                TimeSpan departureTime;
                while (true)
                {
                    Console.Write("Departure Time (HH:mm, 24-hour format): ");
                    string depTimeInput = Console.ReadLine()?.Trim();
                    if (TimeSpan.TryParse(depTimeInput, out departureTime))
                        break;
                    Console.WriteLine("Invalid time format. Please enter time as HH:mm (e.g., 15:30).");
                }

                int c1 = PromptInt("Class1 (1AC) Capacity: ");
                decimal cost1 = PromptDecimal("Class1 (1AC) Cost: ");
                int c2 = PromptInt("Class2 (2AC) Capacity: ");
                decimal cost2 = PromptDecimal("Class2 (2AC) Cost: ");
                int c3 = PromptInt("Class3 (3AC) Capacity: ");
                decimal cost3 = PromptDecimal("Class3 (3AC) Cost: ");

                string query = @"INSERT INTO TrainDetails
(TrainName, FromStation, ToStation,
 Class1Capacity, Class1Available, Class1Cost,
 Class2Capacity, Class2Available, Class2Cost,
 Class3Capacity, Class3Available, Class3Cost)
VALUES
(@name, @from, @to,
 @c1, @c1, @cost1,
 @c2, @c2, @cost2,
 @c3, @c3, @cost3)";

                SqlParameter[] parameters = {
                    DBHelper.Param("@name", name),
                    DBHelper.Param("@from", from),
                    DBHelper.Param("@to", to),
                    DBHelper.Param("@c1", c1),
                    DBHelper.Param("@cost1", cost1),
                    DBHelper.Param("@c2", c2),
                    DBHelper.Param("@cost2", cost2),
                    DBHelper.Param("@c3", c3),
                    DBHelper.Param("@cost3", cost3)
                };

                int rows = DBHelper.ExecuteNonQuery(query, parameters);
                if (rows > 0)
                    Console.WriteLine("Train added successfully. Press Enter...");
                else
                    Console.WriteLine("Failed to add train. Press Enter...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding train: {ex.Message}");
            }
            Console.ReadLine();
        }

        // 2) Soft Delete Train (IsActive = 0)
        public void SoftDeleteTrain()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== SOFT DELETE TRAIN =====");
                DisplayTrains(showInactive: false);

                int trainNo = PromptInt("Enter TrainNo to soft delete (or 0 to cancel): ");
                if (trainNo == 0) return;

                var dt = DBHelper.ExecuteQuery("SELECT * FROM TrainDetails WHERE TrainNo=@tno", new[] { DBHelper.Param("@tno", trainNo) });
                if (dt == null || dt.Rows.Count == 0)
                {
                    Console.WriteLine("Train not found. Press Enter...");
                    Console.ReadLine();
                    return;
                }

                string query = "UPDATE TrainDetails SET IsActive = 0 WHERE TrainNo=@tno";
                int rows = DBHelper.ExecuteNonQuery(query, new[] { DBHelper.Param("@tno", trainNo) });
                if (rows > 0)
                    Console.WriteLine("Train soft-deleted. Press Enter...");
                else
                    Console.WriteLine("Failed to soft-delete train. Press Enter...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while soft-deleting: {ex.Message}");
            }
            Console.ReadLine();
        }

        public void ReactivateTrain()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== REACTIVATE SOFT-DELETED TRAIN =====");

                // Display only soft-deleted trains (IsActive = 0)
                string query = "SELECT TrainNo, TrainName, FromStation, ToStation FROM TrainDetails WHERE IsActive = 0";
                DataTable dt = DBHelper.ExecuteQuery(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    Console.WriteLine("No soft-deleted trains found. Press Enter...");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("TrainNo | TrainName           | From -> To");
                Console.WriteLine("--------------------------------------------");
                foreach (DataRow r in dt.Rows)
                {
                    Console.WriteLine($"{r["TrainNo"],-7} | {r["TrainName"],-18} | {r["FromStation"]} -> {r["ToStation"]}");
                }

                int trainNo = PromptInt("Enter TrainNo to reactivate (or 0 to cancel): ");
                if (trainNo == 0) return;

                // Check if train exists and is soft-deleted
                DataTable trainCheck = DBHelper.ExecuteQuery("SELECT * FROM TrainDetails WHERE TrainNo=@tno AND IsActive=0",
                                                            new[] { DBHelper.Param("@tno", trainNo) });
                if (trainCheck == null || trainCheck.Rows.Count == 0)
                {
                    Console.WriteLine("Train not found or already active. Press Enter...");
                    Console.ReadLine();
                    return;
                }

                // Update IsActive to 1 (reactivate)
                string updateQuery = "UPDATE TrainDetails SET IsActive = 1 WHERE TrainNo=@tno";
                int rows = DBHelper.ExecuteNonQuery(updateQuery, new[] { DBHelper.Param("@tno", trainNo) });

                if (rows > 0)
                    Console.WriteLine("Train reactivated successfully. Press Enter...");
                else
                    Console.WriteLine("Failed to reactivate train. Press Enter...");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reactivating train: {ex.Message}");
            }
            Console.ReadLine();
        }


        // 3) View Earnings Report
        public void ViewEarningsReport()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== EARNINGS REPORT =====");
                string q = @"
SELECT 
    td.TrainNo,
    td.TrainName,
    ISNULL(SUM(CASE WHEN r.IsCancelled = 0 THEN r.TotalCost ELSE 0 END), 0) AS TotalSales,
    ISNULL(SUM(c.RefundAmount), 0) AS TotalRefunds,
    ISNULL(SUM(CASE WHEN r.IsCancelled = 0 THEN r.TotalCost ELSE 0 END), 0) - ISNULL(SUM(c.RefundAmount), 0) AS NetEarnings
FROM TrainDetails td
LEFT JOIN Reservation r ON td.TrainNo = r.TrainNo
LEFT JOIN Cancellation c ON r.BookingId = c.BookingId
GROUP BY td.TrainNo, td.TrainName
ORDER BY td.TrainNo;
";
                DataTable dt = DBHelper.ExecuteQuery(q);
                if (dt == null)
                {
                    Console.WriteLine("Could not fetch earnings. Press Enter...");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("TrainNo | TrainName           | Sales     | Refunds   | NetEarnings");
                Console.WriteLine("-------------------------------------------------------------------");
                foreach (DataRow r in dt.Rows)
                {
                    Console.WriteLine($"{r["TrainNo"],-7} | {r["TrainName"],-18} | {Convert.ToDecimal(r["TotalSales"]),9:0.00} | {Convert.ToDecimal(r["TotalRefunds"]),9:0.00} | {Convert.ToDecimal(r["NetEarnings"]),11:0.00}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching report: {ex.Message}");
            }
            Console.WriteLine("\nPress Enter...");
            Console.ReadLine();
        }

        // 4) Cancel all bookings for a train & refund
        public void CancelAllBookingsForTrain()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== CANCEL ALL BOOKINGS FOR A TRAIN =====");
                DisplayTrains(showInactive: true);

                int trainNo = PromptInt("Enter TrainNo to cancel all bookings for (or 0 to cancel): ");
                if (trainNo == 0) return;

                bool ok = DBHelper.ExecuteTransaction((conn, tran) =>
                {
                    using (SqlCommand selectCmd = new SqlCommand(
                        "SELECT BookingId, Class, TotalCost FROM Reservation WHERE TrainNo=@tno AND IsCancelled=0", conn, tran))
                    {
                        selectCmd.Parameters.Add(DBHelper.Param("@tno", trainNo));
                        using (SqlDataReader rdr = selectCmd.ExecuteReader())
                        {
                            var bookings = new List<(int bid, string cls, decimal cost)>();
                            while (rdr.Read())
                            {
                                bookings.Add((Convert.ToInt32(rdr["BookingId"]), rdr["Class"].ToString(), Convert.ToDecimal(rdr["TotalCost"])));
                            }
                            rdr.Close();

                            if (bookings.Count == 0)
                            {
                                Console.WriteLine("No active bookings to cancel.");
                                return;
                            }

                            foreach (var b in bookings)
                            {
                                using (SqlCommand updRes = new SqlCommand("UPDATE Reservation SET IsCancelled=1 WHERE BookingId=@bid", conn, tran))
                                {
                                    updRes.Parameters.Add(DBHelper.Param("@bid", b.bid));
                                    updRes.ExecuteNonQuery();
                                }

                                decimal refund = Math.Round(b.cost * 0.5M, 2);
                                using (SqlCommand insCancel = new SqlCommand("INSERT INTO Cancellation (BookingId, RefundAmount) VALUES (@bid, @refund)", conn, tran))
                                {
                                    insCancel.Parameters.Add(DBHelper.Param("@bid", b.bid));
                                    insCancel.Parameters.Add(DBHelper.Param("@refund", refund));
                                    insCancel.ExecuteNonQuery();
                                }

                                string col = (b.cls == "1AC") ? "Class1Available" :
                                             (b.cls == "2AC") ? "Class2Available" : "Class3Available";
                                using (SqlCommand updTrain = new SqlCommand($"UPDATE TrainDetails SET {col} = {col} + 1 WHERE TrainNo=@tno", conn, tran))
                                {
                                    updTrain.Parameters.Add(DBHelper.Param("@tno", trainNo));
                                    updTrain.ExecuteNonQuery();
                                }
                            }

                            Console.WriteLine($"{bookings.Count} bookings cancelled and refunded (50%).");
                        }
                    }
                });

                if (!ok)
                {
                    Console.WriteLine("Transaction failed. No changes were committed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cancelling bookings for train: {ex.Message}");
            }
            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }

        // 5) Cancel individual booking & refund
        public void CancelIndividualBooking()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== CANCEL INDIVIDUAL BOOKING =====");
                int bookingId = PromptInt("Enter BookingId to cancel (or 0 to cancel): ");
                if (bookingId == 0) return;

                bool ok = DBHelper.ExecuteTransaction((conn, tran) =>
                {
                    using (SqlCommand getCmd = new SqlCommand("SELECT TrainNo, Class, TotalCost, IsCancelled FROM Reservation WHERE BookingId=@bid", conn, tran))
                    {
                        getCmd.Parameters.Add(DBHelper.Param("@bid", bookingId));
                        using (SqlDataReader dr = getCmd.ExecuteReader())
                        {
                            if (!dr.Read())
                                throw new Exception("Booking not found.");

                            if (Convert.ToBoolean(dr["IsCancelled"]))
                                throw new Exception("Booking already cancelled.");

                            int trainNo = Convert.ToInt32(dr["TrainNo"]);
                            string cls = dr["Class"].ToString();
                            decimal cost = Convert.ToDecimal(dr["TotalCost"]);
                            dr.Close();

                            using (SqlCommand updRes = new SqlCommand("UPDATE Reservation SET IsCancelled=1 WHERE BookingId=@bid", conn, tran))
                            {
                                updRes.Parameters.Add(DBHelper.Param("@bid", bookingId));
                                updRes.ExecuteNonQuery();
                            }

                            decimal refund = Math.Round(cost * 0.5M, 2);
                            using (SqlCommand insCancel = new SqlCommand("INSERT INTO Cancellation (BookingId, RefundAmount) VALUES (@bid, @refund)", conn, tran))
                            {
                                insCancel.Parameters.Add(DBHelper.Param("@bid", bookingId));
                                insCancel.Parameters.Add(DBHelper.Param("@refund", refund));
                                insCancel.ExecuteNonQuery();
                            }

                            string col = (cls == "1AC") ? "Class1Available" :
                                         (cls == "2AC") ? "Class2Available" : "Class3Available";
                            using (SqlCommand updTrain = new SqlCommand($"UPDATE TrainDetails SET {col} = {col} + 1 WHERE TrainNo=@tno", conn, tran))
                            {
                                updTrain.Parameters.Add(DBHelper.Param("@tno", trainNo));
                                updTrain.ExecuteNonQuery();
                            }

                            Console.WriteLine($"Booking {bookingId} cancelled. Refund: {refund}");
                        }
                    }
                });

                if (!ok)
                {
                    Console.WriteLine("Transaction failed. No changes were committed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cancelling booking: {ex.Message}");
            }
            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }

        // ---------------- Helper utilities ----------------

        private static int PromptInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();
                if (int.TryParse(s, out int v) && v >= 0) return v;
                Console.WriteLine("Please enter a valid non-negative integer.");
            }
        }

        private static decimal PromptDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();
                if (decimal.TryParse(s, out decimal v) && v >= 0) return v;
                Console.WriteLine("Please enter a valid non-negative number.");
            }
        }

        private void DisplayTrains(bool showInactive)
        {
            string q = showInactive ?
                "SELECT TrainNo, TrainName, FromStation, ToStation, IsActive FROM TrainDetails" :
                "SELECT TrainNo, TrainName, FromStation, ToStation FROM TrainDetails WHERE IsActive=1";

            DataTable dt = DBHelper.ExecuteQuery(q);
            if (dt == null)
            {
                Console.WriteLine("Could not load trains.");
                return;
            }

            Console.WriteLine("TrainNo | TrainName           | From -> To         | Active");
            Console.WriteLine("------------------------------------------------------------");
            foreach (DataRow r in dt.Rows)
            {
                Console.WriteLine($"{r["TrainNo"],-7} | {r["TrainName"],-18} | {r["FromStation"]} -> {r["ToStation"],-10} | {(showInactive ? (Convert.ToBoolean(r["IsActive"]) ? "Yes" : "No") : "Yes")}");
            }
        }
    }
}

