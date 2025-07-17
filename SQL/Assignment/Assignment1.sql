-- Creating Clients Table
CREATE TABLE Clients
(Client_ID NUMERIC(4) PRIMARY KEY,
Cname VARCHAR(40) NOT NULL,
Address VARCHAR(30),
Email VARCHAR(30) UNIQUE,
Phone NUMERIC(10),
Business VARCHAR(20) NOT NULL)

-- Creating Projects Table
CREATE TABLE Projects(
Project_ID NUMERIC(3) Primary Key,
Descr VARCHAR(30) Not Null, -- Description of project like ‘Accounting’ , ‘Inventory’, ‘Payroll’ etc.
Start_Date DATE, -- Start date of project
Planned_End_Date DATE, -- Planned End date of project
Actual_End_date DATE, -- Must be later than Planned_End_Date Actual End date of project (Use CHECK constraint)
Budget INT CONSTRAINT budget_check CHECK(Budget>0), --Must be positive Use CHECK constraint to ensure budget is > 0
Client_ID NUMERIC(4) FOREIGN KEY REFERENCES Clients(Client_ID)) -- Foreign Key Client ID from Clients table

-- Creating Departments Table
CREATE TABLE Departments(
Deptno NUMERIC(2) PRIMARY KEY,
Dname VARCHAR(15) NOT NULL,
Loc VARCHAR(20))

-- Creating Employees Table
CREATE TABLE Employees(
Empno NUMERIC(4) PRIMARY KEY,
Ename VARCHAR(20) NOT NULL,
Job VARCHAR(15),
Salary INT CONSTRAINT salcheck CHECK(Salary>0), --Must be positive Use CHECK constraint to ensure salary is > 0
Deptno NUMERIC(2) FOREIGN KEY REFERENCES Departments(Deptno))

-- Creating EmpProjectTasks Table
CREATE TABLE EmpProjectTasks(
Project_ID NUMERIC(3) FOREIGN KEY REFERENCES Projects(Project_ID), --Composite primary key and foreign keys referring Projects and Employees table
Empno NUMERIC(4) FOREIGN KEY REFERENCES Employees(Empno),
CONSTRAINT pk_emp_prj_tasks PRIMARY KEY(Project_ID, Empno),
Start_Date DATE, -- Start date when employee begins task on this project
End_Date DATE, -- End date when employee finishes task on this project
Task VARCHAR(25) Not Null, -- Task performed by employee like designing, coding, review, testing etc.
Status VARCHAR(15) Not Null) --Status of task like ‘in progress’, ‘complete’,’cancelled’

-- Insterting values into Clients Table
INSERT INTO Clients VALUES
(1001, 'ACME Utilities' ,'Noida' ,'contact@acmeutil.com', 9567880032, 'Manufacturing'),
(1002, 'Trackon Consultants', 'Mumbai', 'consult@trackon.com', 8734210090, 'Consultant'),
(1003, 'MoneySaver Distributors', 'Kolkata', 'save@moneysaver.com', 7799886655, 'Reseller'),
(1004, 'Lawful Corp', 'Chennai', 'justice@lawful.com', 9210342219, 'Professional')

SELECT * FROM Clients

-- Inserting values into Departments Table
INSERT INTO Departments VALUES
(10, 'Design', 'Pune'),
(20, 'Development', 'Pune'),
(30, 'Testing', 'Mumbai'),
(40, 'Document', 'Mumbai')

SELECT * FROM Departments

-- Inserting values into Employees Table
INSERT INTO Employees VALUES
(7001, 'Sandeep', 'Analyst', 25000, 10),
(7002, 'Rajesh', 'Designer', 30000, 10),
(7003, 'Madhav', 'Developer', 40000, 20),
(7004, 'Manoj', 'Developer', 40000, 20),
(7005, 'Abhay', 'Designer', 35000, 10),
(7006, 'Uma', 'Tester', 30000, 30),
(7007, 'Gita' ,'Tech. Writer', 30000, 40),
(7008, 'Priya', 'Tester', 35000, 30),
(7009, 'Nutan', 'Developer', 45000, 20),
(7010, 'Smita', 'Analyst', 20000, 10),
(7011, 'Anand', 'Project Mgr', 65000, 10)

SELECT * FROM Employees

-- Inserting values into Projects Table
INSERT INTO Projects VALUES(401, 'Inventory', '01-Apr-11', '01-Oct-11', '31-Oct-11', 150000, 1001)
INSERT INTO Projects (Project_ID, Descr, Start_Date, Planned_End_Date, Budget, Client_ID) VALUES
(402, 'Accounting', '01-Aug-11', '01-Jan-12', 500000, 1002),
(403, 'Payroll', '01-Oct-11', '31-Dec-11', 75000, 1003),
(404, 'Contact Mgmt', '01-Nov-11', '31-Dec-11', 50000, 1004)

SELECT * FROM Projects

-- Inserting values into EmpProjectTasks Table
INSERT INTO EmpProjectTasks VALUES
(401, 7001, '01-Apr-11', '20-Apr-11', 'System Analysis', 'Completed'),
(401, 7002, '21-Apr-11', '30-May-11', 'System Design', 'Completed'),
(401, 7003, '01-Jun-11', '15-Jul-11', 'Coding', 'Completed'),
(401, 7004, '18-Jul-11', '01-Sep-11', 'Coding', 'Completed'),
(401, 7006, '03-Sep-11', '15-Sep-11', 'Testing', 'Completed'),
(401, 7009, '18-Sep-11', '05-Oct-11', 'Code Change', 'Completed'),
(401, 7008, '06-Oct-11', '16-Oct-11', 'Testing', 'Completed'),
(401, 7007, '06-Oct-11', '22-Oct-11', 'Documentation', 'Completed'),
(401, 7011, '22-Oct-11', '31-Oct-11', 'Sign off', 'Completed'),
(402, 7010, '01-Aug-11', '20-Aug-11', 'System Analysis', 'Completed'),
(402, 7002, '22-Aug-11', '30-Sep-11', 'System Design', 'Completed'),
(402, 7004, '01-Oct-11', NULL       ,  'Coding', 'In Progress')

SELECT * FROM EmpProjectTasks