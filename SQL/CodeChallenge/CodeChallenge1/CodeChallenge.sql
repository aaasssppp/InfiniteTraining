-- Creating Table CC1_Books
CREATE TABLE CC1_Books
(id INT PRIMARY KEY,
title VARCHAR(30),
author VARCHAR(30),
isbn VARCHAR(16) UNIQUE,
published_date DATE)


SELECT * FROM CC1_Books

INSERT INTO CC1_Books 
VALUES(1, 'My First SQL book', 'Mary Parker', '981483029127', '2012-02-22'),
(2, 'My Second SQL book', 'John Mayer', '857300923713', '1972-07-03'),
(3, 'My Third SQL book', 'Cary Flint', '523120967812', '2015-10-18')

-- Query 1: Write a query to fetch the details of the books written by author whose name ends with er.
SELECT * FROM CC1_Books
WHERE author LIKE '%er'


-- Creating Table CC1_Reviews
CREATE TABLE CC1_Reviews
(id INT PRIMARY KEY,
book_id INT,
reviewer_name VARCHAR(30),
content VARCHAR(30),
rating INT,
published_date DATE)

INSERT INTO CC1_Reviews
VALUES(1,1,'John Smith','My first review',4,'2017-12-10'),
(2,2,'John Smith','My second review',5,'2017-10-13'),
(3,2,'Alice Walker','Another review',1,'2017-10-22')


SELECT * FROM CC1_Reviews



-- Query 2: Display the Title ,Author and ReviewerName for all the books from the above table
SELECT Books.title, Books.Author, Reviews.reviewer_name FROM CC1_Books AS Books, CC1_Reviews AS Reviews
WHERE Books.id = Reviews.book_id


-- Query 3: Display the reviewer name who reviewed more than one book.
SELECT Reviews.reviewer_name FROM CC1_Reviews AS Reviews
GROUP BY reviewer_name
HAVING COUNT(reviewer_name) > 1
	
-- Create Customer Table
CREATE TABLE CC1_Customer
(id INT PRIMARY KEY,
name VARCHAR(30),
age INT,
address VARCHAR(30),
salary FLOAT)

INSERT INTO CC1_Customer
VALUES (1, 'Ramesh', 32, 'Ahmedabad', 2000.00),
(2, 'Khilan', 25, 'Delhi', 1500.00),
(3, 'Kaushik', 23, 'Kota', 2000.00),
(4, 'Chaitali', 25, 'Mumbai', 6500.00),
(5, 'Hardik', 27, 'Bhopal', 8500.00),
(6, 'Komal', 22, 'MP', 4500.00),
(7, 'Muffy', 24, 'Indore', 10000.00)

SELECT * FROM CC1_Customer

-- Query 4: Display the Name for the customer from above customer table who live in same address which has character o anywhere in address
SELECT name FROM CC1_Customer
WHERE address LIKE '%o%'

-- Create table Orders 
CREATE TABLE CC1_Orders
(oid INT PRIMARY KEY,
date DATE,
customer_id INT,
Amount INT)

INSERT INTO CC1_Orders
VALUES( 102, '2009-10-08', 3,3000),
( 100, '2009-10-08', 3,1500),
( 101, '2009-11-20', 2,1560),
( 103, '2009-05-20', 4,2060)

SELECT * FROM CC1_Orders

-- Query 5: Write a query to display the Date,Total no of customer placed order on same Date
SELECT Date, COUNT(customer_id) AS 'Total Customers' FROM CC1_Orders
GROUP BY Date -- Grouping by date
	SELECT COUNT(customer_id) AS 'Total Customers' FROM CC1_Orders
	GROUP BY customer_id -- Grouping by customers

-- Create Employee Table
CREATE TABLE CC1_Employee
(id INT PRIMARY KEY,
name VARCHAR(30),
age INT,
address VARCHAR(30),
salary FLOAT)

INSERT INTO CC1_Employee
VALUES (1, 'Ramesh', 32, 'Ahmedabad', 2000.00),
(2, 'Khilan', 25, 'Delhi', 1500.00),
(3, 'Kaushik', 23, 'Kota', 2000.00),
(4, 'Chaitali', 25, 'Mumbai', 6500.00),
(5, 'Hardik', 27, 'Bhopal', 8500.00),
(6, 'Komal', 22, 'MP', NULL),
(7, 'Muffy', 24, 'Indore', NULL)

SELECT * FROM CC1_Employee

-- Query 6: Display the Names of the Employee in lower case, whose salary is null
SELECT LOWER(Name) AS 'Employee whose salary is Unknown' FROM CC1_Employee
WHERE salary IS NULL

-- Create Student table
CREATE TABLE StudentDetails
(RegisterNo INT,
Name VARCHAR(30),
Age INT,
Qualification VARCHAR(30),
MobileNo NUMERIC,
Mail_id VARCHAR(60),
Location VARCHAR(30),
Gender VARCHAR(1))
 
INSERT INTO StudentDetails 
VALUES(2,'Sai',22,'B.E',9952836777,'Sai@gmail.com','Chennai','M'),
(3,'Kumar',20,'B.SC',7890125648,'Kumar@gmail.com','Madurai','M'),
(4,'Selvi',22,'B.Tech',890467342,'selvi@gmail.com','Salem','F'),
(5,'Nisha',25,'M.E',7834672310,'Nisha@gmail.com','Theni','F'),
(6,'SaiSaran',21,'B.A',7890345678,'saran@gmail.com','Madurai','F'),
(7,'Tom',23,'BCA',8901234675,'Tom@gmail.com','Pune','M')
 
-- Query 7: Write a sql server query to display the Gender,Total no of male and female from the above relation
SELECT Gender,COUNT(Gender) [Total Members] FROM StudentDetails 
GROUP BY Gender