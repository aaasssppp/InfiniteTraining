using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MiniProjectRRS
{
    public class AdminOperations
    {
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

        // 3) View Earnings Report
        public void ViewEarningsReport()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== EARNINGS REPORT =====");
                string q = @"
SELECT td.TrainNo, td.TrainName,
       ISNULL(SUM(r.TotalCost),0) AS TotalSales,
       ISNULL(SUM(c.RefundAmount),0) AS TotalRefunds,
       ISNULL(SUM(r.TotalCost),0) - ISNULL(SUM(c.RefundAmount),0) AS NetEarnings
FROM TrainDetails td
LEFT JOIN Reservation r ON td.TrainNo = r.TrainNo AND r.IsCancelled = 0
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
                    Console.WriteLine($"{r["TrainNo"],-7} | {r["TrainName"],-18} | {Convert.ToDecimal(r["TotalSales"]):0.00,9} | {Convert.ToDecimal(r["TotalRefunds"]):0.00,9} | {Convert.ToDecimal(r["NetEarnings"]):0.00,11}");
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

