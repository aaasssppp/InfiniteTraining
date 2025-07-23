--Query 1. Write a query to display your birthday( day of week)
SELECT DATENAME(WEEKDAY, '2004-07-09') AS [Day of Week of my Birthday]

--Query 2. Write a query to display your age in days
SELECT DATEDIFF(DAY, '2004-07-09', GETDATE()) AS [Age in Days]

INSERT INTO EMP (empno, ename, job, mgr_id, hiredate, sal, comm, deptno)
VALUES
(7201, 'SYAM', 'MANAGER', 7203, '2018-07-15', 3000, NULL, 10),
(7202, 'PRASAD', 'ANALYST', 7203, '2015-07-05', 3500, NULL, 20),
(7203, 'LALITHA', 'DEVELOPER', 7788, '2010-07-20', 4000, NULL, 30),
(7204, 'JUNE', 'DEVELOPER', 7201, '2010-07-20', 2500, NULL, 30)

--Query 3. Write a query to display all employees information those who joined before 5 years
-- in the current month
SELECT * FROM EMP
WHERE hiredate < DATEADD(YEAR, -5, GETDATE()) AND MONTH(hiredate) = MONTH(GETDATE())

DELETE FROM EMP WHERE empno IN (7201, 7202, 7203, 7204)


--Query 4. perform the following operations in a single transaction

BEGIN TRANSACTION

--	a. First insert 3 rows 
INSERT INTO EMP (empno, ename, job, mgr_id, hiredate, sal, comm, deptno)
VALUES
(7201, 'SYAM', 'MANAGER', 7203, '2018-07-15', 3000, NULL, 10),
(7203, 'LALITHA', 'DEVELOPER', 7788, '2010-07-20', 4000, NULL, 30),
(7204, 'JUNE', 'DEVELOPER', 7201, '2010-07-20', 2500, 500, 30)

--  b. Update the second row sal with 15% increment  
UPDATE EMP SET sal = sal * 1.15
WHERE empno = 7203
SAVE TRANSACTION Updated

--  c. Delete the first row.
DELETE FROM EMP
WHERE empno = 7201

ROLLBACK TRANSACTION Updated -- Rollback the delete

COMMIT -- commit changes


--Query 5. Create a user defined function calculate Bonus for all employees of a  given dept using 	following conditions
--Creating function fn
CREATE FUNCTION dbo.fn_CalcBonus (@deptno INT, @sal DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @bonus DECIMAL(10,2)
--	a. For Deptno 10 employees 15% of sal as bonus.
    IF @deptno = 10
        SET @bonus = @sal * 0.15
--  b. For Deptno 20 employees  20% of sal as bonus
    ELSE IF @deptno = 20
        SET @bonus = @sal * 0.20
--  c. For Others employees 5%of sal as bonus
    ELSE
        SET @bonus = @sal * 0.05

    RETURN @bonus
END

--Executing function fn_CalcBonus
SELECT empno, ename, deptno, sal, dbo.fn_CalcBonus(deptno, sal) AS SalaryBonus
FROM EMP

--Query 6. Create a procedure to update the salary of employee by 500 whose dept name is Sales and current salary is below 1500 (use emp table)
-- Creating procedure UpdateSalaryForSales
CREATE PROCEDURE UpdateSalaryForSales
AS
BEGIN
    UPDATE EMP SET sal = sal + 500
    WHERE deptno = 
		(SELECT deptno FROM DEPT WHERE dname = 'Sales') AND sal < 1500
END

-- Executing the procedure UpdateSalaryForSales
EXEC UpdateSalaryForSales

SELECT * FROM EMP
