--Create Table student
CREATE TABLE Student (
sid INT PRIMARY KEY,
sname VARCHAR(20))
 
--Insert values into student table
INSERT INTO Student VALUES
(1,'Jack'),
(2,'Rithvik'),
(3,'Jaspreeth'),
(4,'Praveen'),
(5,'Bisa'),
(6,'Suraj')
 
--Create Table Marks
CREATE TABLE Marks (
Mid INT PRIMARY KEY,
Sid INT REFERENCES Student(sid),
Score INT NOT NULL)
 
--Insert values into Marks Table
INSERT INTO Marks VALUES
(1,1,23),
(2,6,95),
(3,4,98),
(4,2,17),
(5,3,53),
(6,5,13)
 
--Query 1. Write a T-SQL Program to find the factorial of a given number.
 
DECLARE @n INT = 5, @num INT
SET @num = @n
DECLARE @result INT = 1
 
WHILE(@n > 0)
  BEGIN 
    SET @result = @result * @n
    SET @n = @n -1
  END
 
PRINT 'Factorial of ' + CAST(@num AS VARCHAR(20)) + ' is ' + CAST(@result AS VARCHAR(20))
SELECT @num AS Number, @result AS 'Factorial'
 
 
--Query 2. Create a stored procedure to generate multiplication table that accepts a number and generates up to a given number.
 
CREATE OR ALTER PROC sp_MultyTable @num INT
AS
  BEGIN
    DECLARE @i INT = 1, @val INT
	  WHILE(@i <=10)
		BEGIN
		  SET @val = @num * @i
		  PRINT CAST(@num AS VARCHAR(10)) + ' * ' + CAST(@i AS VARCHAR(10)) + ' = ' + CAST(@val AS VARCHAR(10))
		  SET @i = @i + 1
		END
  END
 
--executing procedure
DECLARE @n INT = 5
PRINT 'The Multiplication Table is :'
EXEC sp_MultyTable @n
 
--Query 3. Create a function to calculate the status of the student. If student score >=50 then pass, else fail. Display the data
 
CREATE OR ALTER FUNCTION Calculate(@score INT)
RETURNS VARCHAR(100)
AS
  BEGIN
    RETURN (CASE 
	  WHEN @score >= 50 THEN 'Pass'
	  ELSE 'Fail'
	  END)
  END
 
--Executing function
 
SELECT s.sid, s.sname, m.score, dbo.Calculate(m.score) AS 'Result' 
FROM student s
JOIN marks m 
ON s.sid = m.mid


SELECT * FROM Student
SELECT * FROM Marks