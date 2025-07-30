CREATE TABLE Employee_Details (
EmpId INT IDENTITY PRIMARY KEY,
Name VARCHAR(30),
Salary INT,
Gender VARCHAR(6),
NetSalary AS (Salary * 0.9) PERSISTED)

CREATE PROCEDURE InsertEmployeeDetails
@Name VARCHAR(30),
@Salary INT,
@Gender VARCHAR(6),
@LastEmpId INT OUTPUT
AS
BEGIN
  INSERT INTO Employee_Details (Name, Salary, Gender)
  VALUES (@Name, @Salary, @Gender);

  -- Return the generated Id
  SET @LastEmpId = @@IDENTITY;
END

INSERT INTO Employee_Details (Name, Salary, Gender)
VALUES
('June', 6000, 'Female')

DECLARE @EmpID INT
EXECUTE InsertEmployeeDetails 'June', 6000, 'Female', @EmpID OUTPUT

SELECT * FROM Employee_Details WHERE EmpId = @EmpID

--TRUNCATE TABLE Employee_Details

--DELETE FROM Employee_Details 
--WHERE Name='June'


CREATE PROCEDURE UpdateEmployeeSalary
@EmpId INT,
@UpdatedSalary INT OUTPUT
AS
BEGIN
  -- Updating
  UPDATE Employee_Details
  SET Salary = Salary + 100
  WHERE EmpId = @EmpId;

  -- Returning updated salary
  SELECT @UpdatedSalary = Salary
  FROM Employee_Details
  WHERE EmpId = @EmpId;
END

SELECT * FROM Employee_Details

DECLARE @UpdSalary INT
EXECUTE UpdateEmployeeSalary 1, @UpdSalary OUTPUT

SELECT * FROM Employee_Details WHERE Salary = @UpdSalary


SELECT * FROM Employee_Details
