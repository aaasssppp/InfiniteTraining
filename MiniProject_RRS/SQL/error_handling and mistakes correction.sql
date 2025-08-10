-- STEP 1: Create database
CREATE DATABASE MiniProjectRRS;
GO

USE MiniProjectRRS;
GO

-- 1) Users table (admin + sample customers)
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,        -- store plain for now; hash later if needed
    Role VARCHAR(10) NOT NULL CHECK (Role IN ('Admin','Customer'))
);
GO

INSERT INTO Users (Username, Password, Role)
VALUES ('admin','admin123','Admin'),
       ('cust1','pass1','Customer'),
       ('cust2','pass2','Customer');
GO

-- 2) TrainDetails table
-- Note: we store capacity and available seats separately so seat numbers can be computed reliably
CREATE TABLE TrainDetails (
    TrainNo INT IDENTITY(100,1) PRIMARY KEY,
    TrainName VARCHAR(100) NOT NULL,
    FromStation VARCHAR(50) NOT NULL,
    ToStation VARCHAR(50) NOT NULL,

    Class1Capacity INT NOT NULL,      -- e.g., 1AC capacity
    Class1Available INT NOT NULL,     -- current available seats
    Class1Cost DECIMAL(10,2) NOT NULL,

    Class2Capacity INT NOT NULL,
    Class2Available INT NOT NULL,
    Class2Cost DECIMAL(10,2) NOT NULL,

    Class3Capacity INT NOT NULL,
    Class3Available INT NOT NULL,
    Class3Cost DECIMAL(10,2) NOT NULL,

    IsActive BIT NOT NULL DEFAULT 1   -- soft delete flag
);
GO

INSERT INTO TrainDetails
(TrainName, FromStation, ToStation,
 Class1Capacity, Class1Available, Class1Cost,
 Class2Capacity, Class2Available, Class2Cost,
 Class3Capacity, Class3Available, Class3Cost)
VALUES
('Chennai Express','Chennai','Delhi', 50, 50, 2500.00, 100, 100, 1800.00, 200, 200, 1200.00),
('Bangalore Mail','Bangalore','Mumbai', 40, 40, 2200.00, 80, 80, 1600.00, 150, 150, 1100.00);
GO

-- 3) Reservation table
CREATE TABLE Reservation (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    TrainNo INT NOT NULL FOREIGN KEY REFERENCES TrainDetails(TrainNo),
    DateOfTravel DATE NOT NULL,
    Class VARCHAR(10) NOT NULL CHECK (Class IN ('1AC','2AC','3AC')),
    SeatNo INT NOT NULL,
    TotalCost DECIMAL(10,2) NOT NULL,
    DateOfBooking DATETIME NOT NULL DEFAULT GETDATE(),
    IsCancelled BIT NOT NULL DEFAULT 0
);
GO

-- 4) Cancellation table
CREATE TABLE Cancellation (
    CancellationId INT PRIMARY KEY IDENTITY(1,1),
    BookingId INT NOT NULL FOREIGN KEY REFERENCES Reservation(BookingId),
    CancellationDate DATETIME NOT NULL DEFAULT GETDATE(),
    RefundAmount DECIMAL(10,2) NOT NULL
);
GO

-- SAMPLE reservation so earnings report isn't empty
-- cust1 is UserId = 2 (because identity inserted admin=1,cust1=2,cust2=3)
-- TrainNo identity started at 100, so first train = 100
INSERT INTO Reservation (CustomerId, TrainNo, DateOfTravel, Class, SeatNo, TotalCost)
VALUES (2, 100, DATEADD(day,7,GETDATE()), '1AC', 1, 2500.00)
GO

-- Update availability after sample booking (decrement available)
UPDATE TrainDetails
SET Class1Available = Class1Available - 1
WHERE TrainNo = 100
GO

-- Optional: quick sanity-check queries you can run
-- 1) list trains
SELECT TrainNo, TrainName, FromStation, ToStation,
       Class1Available, Class1Cost, Class2Available, Class2Cost, Class3Available, Class3Cost, IsActive
FROM TrainDetails

-- 2) reservations
SELECT r.BookingId, r.CustomerId, u.Username, r.TrainNo, r.Class, r.SeatNo, r.TotalCost, r.IsCancelled, r.DateOfTravel, r.DateOfBooking
FROM Reservation r
JOIN Users u ON r.CustomerId = u.UserId

-- 3) earnings summary (left join so trains with zero bookings still show)
SELECT td.TrainNo, td.TrainName,
       ISNULL(SUM(r.TotalCost), 0) AS TotalEarnings
FROM TrainDetails td
LEFT JOIN Reservation r ON td.TrainNo = r.TrainNo AND r.IsCancelled = 0
GROUP BY td.TrainNo, td.TrainName
ORDER BY td.TrainNo

SELECT * FROM Users
