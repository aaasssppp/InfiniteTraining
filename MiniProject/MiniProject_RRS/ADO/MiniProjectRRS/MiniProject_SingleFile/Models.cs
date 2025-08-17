using System;

namespace MiniProjectRRS
{
    // Users table
    public class User
    {
        public int UserId { get; set; }         // PK
        public string Username { get; set; }
        public string Password { get; set; }    // In DB it's currently plain text
        public string Role { get; set; }        // 'Admin' or 'Customer'
    }

    // TrainDetails table
    public class TrainDetail
    {
        public int TrainNo { get; set; }        // PK
        public string TrainName { get; set; }
        public string FromStation { get; set; }
        public string ToStation { get; set; }

        public int Class1Capacity { get; set; }
        public int Class1Available { get; set; }
        public decimal Class1Cost { get; set; }

        public int Class2Capacity { get; set; }
        public int Class2Available { get; set; }
        public decimal Class2Cost { get; set; }

        public int Class3Capacity { get; set; }
        public int Class3Available { get; set; }
        public decimal Class3Cost { get; set; }

        public bool IsActive { get; set; }      // Soft delete flag
    }

    // Reservation table
    public class Reservation
    {
        public int BookingId { get; set; }      // PK
        public int CustomerId { get; set; }     // FK -> Users.UserId
        public int TrainNo { get; set; }        // FK -> TrainDetails.TrainNo
        public DateTime DateOfTravel { get; set; }
        public string Class { get; set; }       // '1AC', '2AC', '3AC'
        public int SeatNo { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime DateOfBooking { get; set; }
        public bool IsCancelled { get; set; }
    }

    // Cancellation table
    public class Cancellation
    {
        public int CancellationId { get; set; } // PK
        public int BookingId { get; set; }      // FK -> Reservation.BookingId
        public DateTime CancellationDate { get; set; }
        public decimal RefundAmount { get; set; }
    }
}

