--Query 1

CREATE OR ALTER PROC sp_Payslip(@empno INT)
AS 
BEGIN
DECLARE @ename VARCHAR(30)
DECLARE @sal FLOAT
DECLARE @hra FLOAT
DECLARE @da FLOAT
DECLARE @pf FLOAT
DECLARE @it FLOAT
DECLARE @ded FLOAT
DECLARE @gross FLOAT
DECLARE @net FLOAT
SELECT @ename=ename,@sal=sal FROM EMP WHERE empno=@empno
IF @ename IS NULL
BEGIN
PRINT 'Employee data not found.'
RETURN
END
SET @hra=@sal*0.10
SET @da=@sal*0.20
SET @pf=@sal*0.08
SET @it=@sal*0.05
SET @ded=@pf+@it
SET @gross=@sal+@hra+@da
SET @net=@gross-@ded
PRINT 'Employee Number: ' + CAST(@empno AS VARCHAR(10))
PRINT 'Employee Name: ' + @ename
PRINT 'Basic Salary: ' + CAST(@sal AS VARCHAR(10))
PRINT 'HRA (10%): ' + CAST(@Hra AS VARCHAR(10))
PRINT 'DA  (20%): ' + CAST(@da AS VARCHAR(10))
PRINT 'PF  (8%): ' + CAST(@pf AS VARCHAR(10))
PRINT 'IT  (5%): ' + CAST(@it AS VARCHAR(10))
PRINT 'Deductions: ' + CAST(@ded AS VARCHAR(10))
PRINT 'Gross Salary: ' + CAST(@gross AS VARCHAR(10))
PRINT 'Net Salary: ' + CAST(@net AS VARCHAR(10))
END
SELECT * FROM EMP
EXEC sp_payslip 7369
EXEC sp_payslip 7490

 
--Query 2
 
CREATE TABLE Holiday
(Holiday_Date DATE,
Holiday_Name VARCHAR(20))
INSERT INTO holiday VALUES
('2025-01-26', 'Republic Day'),
('2025-08-15', 'Independence Day'),
('2025-10-20', 'Diwali'),
('2025-12-25', 'Christmas')
 
SELECT * FROM Holiday
 
CREATE OR ALTER TRIGGER trg_On_Holiday
ON emp FOR INSERT,DELETE,UPDATE 
AS
BEGIN
DECLARE @hname VARCHAR(20)
SELECT @hname=holiday_name FROM holiday WHERE Holiday_date=CAST(GETDATE() AS DATE)
IF @hname IS NOT NULL
BEGIN
RAISERROR('Due to %s,you cannot manipulate data',16,1,@hname)
ROLLBACK
END
END
SELECT CAST(GETDATE() AS DATE) AS today;
UPDATE emp SET sal = sal + 1000 WHERE empno = 7369;
INSERT INTO holiday VALUES (CAST(GETDATE() AS DATE), 'holiday');
UPDATE emp SET sal = sal + 500 WHERE empno = 7369;