-- 1️ Users Table (Admin + Customers)
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(50) NOT NULL,
    Role VARCHAR(10) CHECK (Role IN ('Admin', 'Customer'))
)

-- Insert default Admin
INSERT INTO Users (Username, Password, Role)
VALUES ('admin', 'admin123', 'Admin')

-- Insert example customers
INSERT INTO Users (Username, Password, Role)
VALUES ('cust1', 'pass1', 'Customer'),
       ('cust2', 'pass2', 'Customer')

-----------------------------------------------------------

-- 2️ Train Details Table
CREATE TABLE TrainDetails (
    TrainNo INT PRIMARY KEY,
    TrainName VARCHAR(100) NOT NULL,
    FromStation VARCHAR(50) NOT NULL,
    ToStation VARCHAR(50) NOT NULL,
    Class1Max INT NOT NULL,
    Class2Max INT NOT NULL,
    Class3Max INT NOT NULL,
    Class1Cost FLOAT NOT NULL,
    Class2Cost FLOAT NOT NULL,
    Class3Cost FLOAT NOT NULL,
    IsActive BIT DEFAULT 1
)

-- Sample Train Data
INSERT INTO TrainDetails
(TrainNo, TrainName, FromStation, ToStation, Class1Max, Class2Max, Class3Max, Class1Cost, Class2Cost, Class3Cost)
VALUES
(101, 'Chennai Express', 'Chennai', 'Delhi', 50, 100, 200, 2500, 1800, 1200),
(102, 'Bangalore Mail', 'Bangalore', 'Mumbai', 40, 80, 150, 2200, 1600, 1100)

-----------------------------------------------------------

-- 3️⃣ Reservation Table
CREATE TABLE Reservation (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    TrainNo INT NOT NULL FOREIGN KEY REFERENCES TrainDetails(TrainNo),
    DateOfTravel DATE NOT NULL,
    Class VARCHAR(10) CHECK (Class IN ('1AC', '2AC', '3AC')),
    SeatNo INT NOT NULL,
    TotalCost FLOAT NOT NULL,
    DateOfBooking DATE NOT NULL DEFAULT GETDATE(),
    IsCancelled BIT DEFAULT 0
)

-----------------------------------------------------------

-- 4️ Cancellation Table
CREATE TABLE Cancellation (
    CancellationId INT PRIMARY KEY IDENTITY(1,1),
    BookingId INT NOT NULL FOREIGN KEY REFERENCES Reservation(BookingId),
    CancellationDate DATE NOT NULL DEFAULT GETDATE(),
    RefundAmount FLOAT NOT NULL
)

SELECT * FROM Users
SELECT * FROM TrainDetails
SELECT * FROM Reservation
SELECT * FROM Cancellation
