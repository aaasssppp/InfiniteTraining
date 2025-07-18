-- Creating EMP table
CREATE TABLE EMP(
empno NUMERIC(4), 
ename VARCHAR(30), 
job VARCHAR(30), 
mgr_id NUMERIC(4), 
hiredate DATE, 
sal INT, 
comm INT, 
deptno NUMERIC(2)) 

-- Creating DEPT table
CREATE TABLE DEPT(
deptno NUMERIC(2), 
dname VARCHAR(30), 
loc VARCHAR(30)) 

-- Insert values into EMP table
INSERT INTO EMP VALUES
(7369, 'SMITH', 'CLERK', 7902,'17-DEC-80', 800, NULL, 20),
(7499, 'ALLEN', 'SALESMAN', 7698, '20-FEB-81', 1600, 300, 30),
(7521,'WARD', 'SALESMAN', 7698,	'22-FEB-81', 1250, 500, 30),
(7566, 'JONES', 'MANAGER', 7839, '02-APR-81', 2975, NULL, 20),
(7654, 'MARTIN', 'SALESMAN', 7698, '28-SEP-81', 1250, 1400, 30),
(7698, 'BLAKE', 'MANAGER', 7839, '01-MAY-81', 2850, NULL, 30),
(7782, 'CLARK', 'MANAGER', 7839, '09-JUN-81', 2450, NULL, 10),
(7788, 'SCOTT', 'ANALYST', 7566, '19-APR-87', 3000, NULL, 20),
(7839, 'KING', 'PRESIDENT', NULL, '17-NOV-81', 5000, NULL, 10),
(7844, 'TURNER', 'SALESMAN', 7698, '08-SEP-81', 1500, 0, 30),
(7876, 'ADAMS', 'CLERK', 7788, '23-MAY-87', 1100, NULL, 20),
(7900, 'JAMES', 'CLERK', 7698, '03-DEC-81', 950, NULL, 30),
(7902, 'FORD', 'ANALYST', 7566, '03-DEC-81', 3000, NULL, 20),
(7934, 'MILLER', 'CLERK', 7782, '23-JAN-82', 1300, NULL, 10)

-- Insert values into DEPT table
INSERT INTO DEPT VALUES
(10, 'ACCOUNTING', 'NEW YORK'), 
(20, 'RESEARCH', 'DALLAS'),
(30, 'SALES', 'CHICAGO'), 
(40, 'OPERATIONS', 'BOSTON')

-- Query 1. List all employees whose name begins with 'A'. 
SELECT * FROM EMP
WHERE ename LIKE 'A%'

-- Query 2. Select all those employees who don't have a manager. 
SELECT * FROM EMP
WHERE mgr_id IS NULL

-- Query 3. List employee name, number and salary for those employees who earn in the range 1200 to 1400. 
SELECT empno [Employee Number],ename [Employee Name],sal [Salary] FROM EMP
WHERE sal BETWEEN 1200 AND 1400

-- Query 4. Give all the employees in the RESEARCH department a 10% pay rise. Verify that this has been done by listing all their details before and after the rise.
-- Before 10% pay rise
SELECT ename [Employee Name], sal [Salary], dname [Department] FROM EMP
JOIN DEPT
ON EMP.deptno = DEPT.deptno
WHERE DEPT.dname = 'Research'
-- After 10% pay rise
UPDATE EMP
SET sal = sal * 1.1
WHERE deptno = (
    SELECT deptno FROM DEPT WHERE dname = 'RESEARCH'
)
--UPDATE EMP SET sal = sal*1.1
--WHERE EXISTS(
--	SELECT 'x' FROM DEPT
--	WHERE EMP.deptno = DEPT.deptno AND DEPT.dname = 'Reasearch')
-- Viewing after 10% rise
SELECT ename [Employee Name], sal [Salary], dname [Department] FROM EMP
JOIN DEPT
ON EMP.deptno = DEPT.deptno
WHERE DEPT.dname = 'Research'

-- Query 5. Find the number of CLERKS employed. Give it a descriptive heading. 
SELECT COUNT(*) AS [Number of Clerks] FROM EMP
WHERE job = 'Clerk'

-- Query 6. Find the average salary for each job type and the number of people employed in each job. 
SELECT dname, AVG(sal)[Average salary], COUNT(*)[Number of people] 
FROM EMP
JOIN DEPT
ON EMP.deptno = DEPT.deptno
GROUP BY DEPT.dname

-- Query 7. List the employees with the lowest and highest salary. 
SELECT * 
FROM EMP 
WHERE sal = (SELECT MIN(sal) FROM EMP) OR sal = (SELECT MAX(sal) FROM EMP)

-- Query 8. List full details of departments that don't have any employees. 
SELECT D.*
FROM DEPT D
LEFT JOIN EMP E ON D.deptno = E.deptno
WHERE E.empno IS NULL;

-- Query 9. Get the names and salaries of all the analysts earning more than 1200 who are based in department 20. Sort the answer by ascending order of name. 
SELECT ename AS [Employee Name], sal AS [Salary]
FROM EMP
WHERE job = 'Analyst' AND sal > 1200 AND deptno = 20
ORDER BY ename;

-- Query 10. For each department, list its name and number together with the total salary paid to employees in that department. 
SELECT D.deptno AS [Department Number], D.dname AS [Department Name], SUM(E.sal) AS [Total Salary]
FROM DEPT D
JOIN EMP E ON D.deptno = E.deptno
GROUP BY D.deptno, D.dname;

-- Query 11. Find out salary of both MILLER and SMITH.
SELECT ename AS [Employee Name], sal AS [Salary] FROM EMP
WHERE ename IN ('MILLER', 'SMITH');

-- Query 12. Find out the names of the employees whose name begin with ‘A’ or ‘M’.
SELECT ename AS [Employee Name] FROM EMP
WHERE ename LIKE 'A%' OR ename LIKE 'M%';

-- Query 13. Compute yearly salary of SMITH. 
SELECT ename AS [Employee Name], sal * 12 AS [Yearly Salary] FROM EMP
WHERE ename = 'SMITH';

-- Query 14. List the name and salary for all employees whose salary is not in the range of 1500 and 2850. 
SELECT ename AS [Employee Name], sal AS [Salary] FROM EMP
WHERE sal NOT BETWEEN 1500 AND 2850;

-- Query 15. Find all managers who have more than 2 employees reporting to them
SELECT e.empno AS [Manager ID], e.ename AS [Manager Name], COUNT(*) AS [Number of Reportees] FROM EMP m
JOIN EMP e ON m.mgr_id = e.empno
GROUP BY e.empno, e.ename
HAVING COUNT(*) > 2;


SELECT * FROM EMP
SELECT * FROM DEPT
