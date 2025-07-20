-- Query 1. Retrieve a list of MANAGERS. 
SELECT DISTINCT e.*
FROM EMP e
WHERE e.empno IN (SELECT DISTINCT mgr_id FROM EMP WHERE mgr_id IS NOT NULL);

-- Query 2. Find out the names and salaries of all employees earning more than 1000 per month. 
SELECT ename [Employee Name], sal [Salary]
FROM EMP
WHERE sal > 1000;

-- Query 3. Display the names and salaries of all employees except JAMES. 
SELECT ename, sal
FROM EMP
WHERE ename <> 'JAMES';

-- Query 4. Find out the details of employees whose names begin with ‘S’. 
SELECT *
FROM EMP
WHERE ename LIKE 'S%';

-- Query 5. Find out the names of all employees that have ‘A’ anywhere in their name. 
SELECT ename
FROM EMP
WHERE ename LIKE '%A%';

-- Query 6. Find out the names of all employees that have ‘L’ as their third character in their name. 
SELECT ename
FROM EMP
WHERE ename LIKE '__L%';

-- Query 7. Compute daily salary of JONES. 
SELECT ename, sal, (sal / 30.0) AS [Daily Salary]
FROM EMP
WHERE ename = 'JONES';

-- Query 8. Calculate the total monthly salary of all employees. 
SELECT SUM(sal) AS total_monthly_salary
FROM EMP;

-- Query 9. Print the average annual salary . 
SELECT AVG(sal * 12) AS average_annual_salary
FROM EMP;

-- Query 10. Select the name, job, salary, department number of all employees except SALESMAN from department number 30. 
SELECT ename, job, sal, deptno
FROM EMP
WHERE deptno = 30 AND job != 'SALESMAN';

-- Query 11. List unique departments of the EMP table. 
SELECT DISTINCT D.deptno, D.dname, D.loc
FROM EMP E
JOIN DEPT D ON E.deptno = D.deptno;
-- Query 12. List the name and salary of employees who earn more than 1500 and are in department 10 or 30. Label the columns Employee and Monthly Salary respectively.
SELECT ename AS Employee, sal AS "Monthly Salary"
FROM EMP
WHERE sal > 1500 AND deptno IN (10, 30);

-- Query 13. Display the name, job, and salary of all the employees whose job is MANAGER or ANALYST and their salary is not equal to 1000, 3000, or 5000. 
SELECT ename, job, sal
FROM EMP
WHERE job IN ('MANAGER', 'ANALYST') AND sal NOT IN (1000, 3000, 5000);

-- Query 14. Display the name, salary and commission for all employees whose commission amount is greater than their salary increased by 10%. 
SELECT ename, sal, comm
FROM EMP
WHERE comm > sal * 1.1;

-- Query 15. Display the name of all employees who have two Ls in their name and are in department 30 or their manager is 7782. 
SELECT ename
FROM EMP
WHERE LOWER(ename) LIKE '%l%l%' AND (deptno = 30 OR mgr_id = 7782);

-- Query 16. Display the names of employees with experience of over 30 years and under 40 yrs.Count the total number of employees. 
SELECT COUNT(*) AS total_employees
FROM EMP
WHERE DATEDIFF(YEAR, hiredate, GETDATE()) > 30
  AND DATEDIFF(YEAR, hiredate, GETDATE()) < 40;

-- Query 17. Retrieve the names of departments in ascending order and their employees in descending order. 
SELECT D.dname AS Department, E.ename AS Employee
FROM DEPT D
JOIN EMP E 
ON D.deptno = E.deptno
ORDER BY D.dname, E.ename DESC;

-- Query 18. Find out experience of MILLER. 
SELECT ename, DATEDIFF(YEAR, hiredate, GETDATE()) AS Experience
FROM EMP
WHERE ename = 'MILLER';

SELECT * FROM EMP