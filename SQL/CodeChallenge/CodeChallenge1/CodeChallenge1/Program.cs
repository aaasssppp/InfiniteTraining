using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeLINQExample
{
    class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string City { get; set; }
    }
    class Program
    {
        public static void Display(Employee emp)
        {
            Console.WriteLine($"{emp.EmployeeID}, {emp.FirstName} {emp.LastName}, {emp.Title}, DOB: {emp.DOB.ToShortDateString()}, DOJ: {emp.DOJ.ToShortDateString()}, City: {emp.City}");
        }
        static void Main(string[] args)
        {
            List<Employee> empList = new List<Employee>
            {
                new Employee { EmployeeID = 1001, FirstName = "Malcolm", LastName = "Daruwalla", Title = "Manager", DOB = DateTime.ParseExact("16-11-1984", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("08-06-2011", "dd-MM-yyyy", null), City = "Mumbai" },
                new Employee { EmployeeID = 1002, FirstName = "Asdin", LastName = "Dhalla", Title = "AsstManager", DOB = DateTime.ParseExact("20-08-1994", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("07-07-2012", "dd-MM-yyyy", null), City = "Mumbai" },
                new Employee { EmployeeID = 1003, FirstName = "Madhavi", LastName = "Oza", Title = "Consultant", DOB = DateTime.ParseExact("14-11-1987", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("12-04-2015", "dd-MM-yyyy", null), City = "Pune" },
                new Employee { EmployeeID = 1004, FirstName = "Saba", LastName = "Shaikh", Title = "SE", DOB = DateTime.ParseExact("03-06-1990", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("02-02-2016", "dd-MM-yyyy", null), City = "Pune" },
                new Employee { EmployeeID = 1005, FirstName = "Nazia", LastName = "Shaikh", Title = "SE", DOB = DateTime.ParseExact("08-03-1991", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("02-02-2016", "dd-MM-yyyy", null), City = "Mumbai" },
                new Employee { EmployeeID = 1006, FirstName = "Amit", LastName = "Pathak", Title = "Consultant", DOB = DateTime.ParseExact("07-11-1989", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("08-08-2014", "dd-MM-yyyy", null), City = "Chennai" },
                new Employee { EmployeeID = 1007, FirstName = "Vijay", LastName = "Natrajan", Title = "Consultant", DOB = DateTime.ParseExact("02-12-1989", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("01-06-2015", "dd-MM-yyyy", null), City = "Mumbai" },
                new Employee { EmployeeID = 1008, FirstName = "Rahul", LastName = "Dubey", Title = "Associate", DOB = DateTime.ParseExact("11-11-1993", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("06-11-2014", "dd-MM-yyyy", null), City = "Chennai" },
                new Employee { EmployeeID = 1009, FirstName = "Suresh", LastName = "Mistry", Title = "Associate", DOB = DateTime.ParseExact("12-08-1992", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("03-12-2014", "dd-MM-yyyy", null), City = "Chennai" },
                new Employee { EmployeeID = 1010, FirstName = "Sumit", LastName = "Shah", Title = "Manager", DOB = DateTime.ParseExact("12-04-1991", "dd-MM-yyyy", null), DOJ = DateTime.ParseExact("02-01-2016", "dd-MM-yyyy", null), City = "Pune" }
            };


            Console.WriteLine("-----All Employees-----");
            var allEmp = from emp in empList
                         select emp;
            foreach (var emp in allEmp)
            {
                Display(emp);
            }

            Console.WriteLine("-----Employees not from Mumbai-----");
            var notMumbai = from emp in empList 
                            where emp.City != "Mumbai" 
                            select emp;
            foreach (var emp in notMumbai)
            {
                Display(emp);
            }

            Console.WriteLine("-----Employees with the Title 'AsstManager'-----");
            var asstManagers = from emp in empList 
                               where emp.Title == "AsstManager" 
                               select emp;
            foreach (var emp in asstManagers)
            {
                Display(emp);
            }

            Console.WriteLine("-----Employees whose last name starts with 'S'-----");
            var lastNameS = from emp in empList 
                            where emp.LastName.StartsWith("S") 
                            select emp;

            foreach (var emp in lastNameS)
            {
                Display(emp);
            }

            Console.ReadLine();
        }
    }
}