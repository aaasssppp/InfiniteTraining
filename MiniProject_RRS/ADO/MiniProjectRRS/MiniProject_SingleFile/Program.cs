using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MiniProjectRRS
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== RAILWAY RESERVATION SYSTEM =====");
                Console.WriteLine("1. Register (Customer)");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        LoginUser();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("===== CUSTOMER REGISTRATION =====");
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            string query = "INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, 'Customer')";
            SqlParameter[] parameters = {
                new SqlParameter("@u", username),
                new SqlParameter("@p", password)
            };

            int rows = DBHelper.ExecuteNonQuery(query, parameters);
            if (rows > 0)
                Console.WriteLine("Registration successful! Press Enter to continue...");
            else
                Console.WriteLine("Registration failed. Press Enter to continue...");
            Console.ReadLine();
        }

        static void LoginUser()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== LOGIN =====");
                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Username or password cannot be empty.");
                    Console.ReadLine();
                    return;
                }

                string query = "SELECT * FROM Users WHERE Username=@u AND Password=@p";
                SqlParameter[] parameters = {
                    new SqlParameter("@u", username),
                    new SqlParameter("@p", password)
                };

                DataTable dt = DBHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Session.UserId = Convert.ToInt32(row["UserId"]);
                    Session.Username = row["Username"].ToString();
                    Session.Role = row["Role"].ToString();

                    Console.WriteLine($"Welcome {Session.Username} ({Session.Role})!");
                    Console.ReadLine();

                    if (Session.Role == "Admin")
                        AdminMenu();
                    else
                        CustomerMenu();
                }
                else
                {
                    Console.WriteLine("Invalid credentials. Press Enter to try again...");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while logging in: {ex.Message}");
                Console.ReadLine();
            }
        }

        static void AdminMenu()
        {
            var adminOps = new AdminOperations();
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"===== ADMIN MENU ({Session.Username}) =====");
                Console.WriteLine("1. View All Trains");
                Console.WriteLine("2. Add Train");
                Console.WriteLine("3. Soft Delete Train");
                Console.WriteLine("4. Reactivate Train");
                Console.WriteLine("5. View Earnings Report");
                Console.WriteLine("6. Cancel All Bookings for a Train & Refund");
                Console.WriteLine("7. Cancel Individual Booking & Refund");
                Console.WriteLine("8. Logout");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AdminOperations.ViewAllTrains();
                        break;
                    case "2":
                        adminOps.AddTrain();
                        break;
                    case "3":
                        adminOps.SoftDeleteTrain();
                        break;
                    case "4":
                        adminOps.ReactivateTrain();
                        break;
                    case "5":
                        adminOps.ViewEarningsReport();
                        break;
                    case "6":
                        adminOps.CancelAllBookingsForTrain();
                        break;
                    case "7":
                        adminOps.CancelIndividualBooking();
                        break;
                    case "8":
                        Session.Clear();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void CustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== CUSTOMER MENU =====");
                Console.WriteLine("1. Book Ticket");
                Console.WriteLine("2. Cancel Ticket");
                Console.WriteLine("3. View My Tickets");
                Console.WriteLine("4. Logout");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        BookTicket();
                        break;
                    case "2":
                        CancelTicket();
                        break;
                    case "3":
                        ViewMyTickets();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void BookTicket()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== BOOK TICKET =====");

                string query = @"SELECT TrainNo, TrainName, FromStation, ToStation,
                Class1Available, Class1Cost, Class2Available, Class2Cost, Class3Available, Class3Cost,
                DepartureTime
                FROM TrainDetails
                WHERE IsActive = 1
                AND (Class1Available > 0 OR Class2Available > 0 OR Class3Available > 0)";


                DataTable trainDetails = DBHelper.ExecuteQuery(query);

                if (trainDetails.Rows.Count == 0)
                {
                    Console.WriteLine("No trains available.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("\nAvailable Trains:");
                foreach (DataRow t in trainDetails.Rows)
                {
                    Console.WriteLine($"TrainNo: {t["TrainNo"]} | {t["TrainName"]} | {t["FromStation"]} -> {t["ToStation"]} | Dep Time: {TimeSpan.Parse(t["DepartureTime"].ToString())}");
                    Console.WriteLine($"   1AC: Seats {t["Class1Available"]}, Price {t["Class1Cost"]}");
                    Console.WriteLine($"   2AC: Seats {t["Class2Available"]}, Price {t["Class2Cost"]}");
                    Console.WriteLine($"   3AC: Seats {t["Class3Available"]}, Price {t["Class3Cost"]}");
                }

                Console.Write("\nEnter TrainNo to book: ");
                if (!int.TryParse(Console.ReadLine(), out int trainNo))
                {
                    Console.WriteLine("Invalid TrainNo.");
                    Console.ReadLine();
                    return;
                }

                DataRow train = trainDetails.Select($"TrainNo = {trainNo}").FirstOrDefault();
                if (train == null)
                {
                    Console.WriteLine("Train not found.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter class (1AC/2AC/3AC): ");
                string cls = Console.ReadLine().ToUpper();
                string availCol = cls == "1AC" ? "Class1Available" : cls == "2AC" ? "Class2Available" : "Class3Available";
                string costCol = cls == "1AC" ? "Class1Cost" : cls == "2AC" ? "Class2Cost" : "Class3Cost";

                Console.Write("Enter number of seats: ");
                if (!int.TryParse(Console.ReadLine(), out int seats) || seats <= 0)
                {
                    Console.WriteLine("Invalid seat number.");
                    Console.ReadLine();
                    return;
                }

                if (seats > Convert.ToInt32(train[availCol]))
                {
                    Console.WriteLine("Not enough seats available.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter date of travel (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime travelDate))
                {
                    Console.WriteLine("Invalid date format.");
                    Console.ReadLine();
                    return;
                }

                // Validate date is not in the past
                DateTime today = DateTime.Today;
                if (travelDate.Date < today)
                {
                    Console.WriteLine("Cannot book for a past date.");
                    Console.ReadLine();
                    return;
                }

                // If booking for today, check 1-hour rule
                // First get departure time from TrainDetails
                string depQuery = "SELECT DepartureTime FROM TrainDetails WHERE TrainNo = @tno";
                DataTable depDt = DBHelper.ExecuteQuery(depQuery, new SqlParameter[] { new SqlParameter("@tno", trainNo) });
                if (depDt.Rows.Count > 0)
                {
                    TimeSpan depTime = TimeSpan.Parse(depDt.Rows[0]["DepartureTime"].ToString());
                    DateTime depDateTime = travelDate.Date + depTime;
                    if (travelDate.Date == today && DateTime.Now > depDateTime.AddHours(-1))
                    {
                        Console.WriteLine("Booking closed: Less than 1 hour before train departure.");
                        Console.ReadLine();
                        return;
                    }
                }


                decimal totalCost = seats * Convert.ToDecimal(train[costCol]);
                Console.WriteLine($"\nTotal Cost: {totalCost:C}");
                Console.Write("Confirm booking? (Y/N): ");
                if (Console.ReadLine().ToUpper() != "Y")
                {
                    Console.WriteLine("Booking cancelled by user.");
                    Console.ReadLine();
                    return;
                }

                // Loop for each seat
                for (int i = 0; i < seats; i++)
                {
                    string insertQuery = @"INSERT INTO Reservation (CustomerId, TrainNo, DateOfTravel, Class, SeatNo, TotalCost) 
                                           VALUES (@cid, @tno, @date, @class, @seat, @cost)";
                    DBHelper.ExecuteNonQuery(insertQuery, new SqlParameter[]
                    {
                        new SqlParameter("@cid", Session.UserId),
                        new SqlParameter("@tno", trainNo),
                        new SqlParameter("@date", travelDate),
                        new SqlParameter("@class", cls),
                        new SqlParameter("@seat", i + 1), // simplified seat number
                        new SqlParameter("@cost", Convert.ToDecimal(train[costCol]))
                    });
                }

                // Update availability
                string updateQuery = $"UPDATE TrainDetails SET {availCol} = {availCol} - @seats WHERE TrainNo = @tno";
                DBHelper.ExecuteNonQuery(updateQuery, new SqlParameter[]
                {
                    new SqlParameter("@seats", seats),
                    new SqlParameter("@tno", trainNo)
                });

                Console.WriteLine("Ticket(s) booked successfully!");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while booking: {ex.Message}");
                Console.ReadLine();
            }
        }

        static void ViewMyTickets()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== MY TICKETS =====");

                string query = @"SELECT r.BookingId, t.TrainNo, t.TrainName, t.FromStation, t.ToStation, 
                                 r.DateOfTravel, r.Class, r.SeatNo, r.TotalCost, r.IsCancelled
                                 FROM Reservation r
                                 JOIN TrainDetails t ON r.TrainNo = t.TrainNo
                                 WHERE r.CustomerId = @uid";
                DataTable tickets = DBHelper.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@uid", Session.UserId) });

                if (tickets.Rows.Count == 0)
                {
                    Console.WriteLine("No tickets found.");
                    Console.ReadLine();
                    return;
                }

                foreach (DataRow row in tickets.Rows)
                {
                    string status = (bool)row["IsCancelled"] ? "Cancelled" : "Active";
                    Console.WriteLine($"BookingId: {row["BookingId"]} | {row["TrainNo"]} - {row["TrainName"]} | {row["FromStation"]} -> {row["ToStation"]} | Date: {row["DateOfTravel"]:yyyy-MM-dd} | Class: {row["Class"]} | Seat: {row["SeatNo"]} | Cost: {row["TotalCost"]} | Status: {status}");
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while viewing tickets: {ex.Message}");
                Console.ReadLine();
            }
        }

        static void CancelTicket()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== CANCEL TICKET =====");

                string query = @"SELECT r.BookingId, t.TrainNo, t.TrainName, r.Class, r.SeatNo, r.TotalCost 
                                 FROM Reservation r
                                 JOIN TrainDetails t ON r.TrainNo = t.TrainNo
                                 WHERE r.CustomerId = @uid AND r.IsCancelled = 0";
                DataTable tickets = DBHelper.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@uid", Session.UserId) });

                if (tickets.Rows.Count == 0)
                {
                    Console.WriteLine("No active bookings found.");
                    Console.ReadLine();
                    return;
                }

                foreach (DataRow row in tickets.Rows)
                {
                    Console.WriteLine($"BookingId: {row["BookingId"]} | Train: {row["TrainNo"]} - {row["TrainName"]} | Class: {row["Class"]} | Seat: {row["SeatNo"]} | Cost: {row["TotalCost"]}");
                }

                Console.Write("\nEnter BookingId to cancel: ");
                if (!int.TryParse(Console.ReadLine(), out int bookingId))
                {
                    Console.WriteLine("Invalid BookingId.");
                    Console.ReadLine();
                    return;
                }

                DataRow booking = tickets.Select($"BookingId = {bookingId}").FirstOrDefault();
                if (booking == null)
                {
                    Console.WriteLine("Booking not found.");
                    Console.ReadLine();
                    return;
                }

                decimal refund = Convert.ToDecimal(booking["TotalCost"]) * 0.5m;

                // Mark cancelled
                string cancelQuery = "UPDATE Reservation SET IsCancelled = 1 WHERE BookingId = @bid";
                DBHelper.ExecuteNonQuery(cancelQuery, new SqlParameter[] { new SqlParameter("@bid", bookingId) });

                // Add cancellation record
                string insertCancel = "INSERT INTO Cancellation (BookingId, RefundAmount) VALUES (@bid, @refund)";
                DBHelper.ExecuteNonQuery(insertCancel, new SqlParameter[]
                {
                    new SqlParameter("@bid", bookingId),
                    new SqlParameter("@refund", refund)
                });

                // Restore seat availability
                string availCol = booking["Class"].ToString() == "1AC" ? "Class1Available" :
                                  booking["Class"].ToString() == "2AC" ? "Class2Available" : "Class3Available";

                string updateSeats = $@"UPDATE TrainDetails
                                        SET {availCol} = {availCol} + 1 
                                        WHERE TrainNo = (SELECT TrainNo FROM Reservation WHERE BookingId = @bid)";
                DBHelper.ExecuteNonQuery(updateSeats, new SqlParameter[] { new SqlParameter("@bid", bookingId) });

                // By default it gives in dollars

                // Set culture to India
                // using System.Globalization;
                // CultureInfo indianCulture = new CultureInfo("en-IN");

                Console.WriteLine($"Ticket cancelled. Refund: {refund:C}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while cancelling ticket: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}

