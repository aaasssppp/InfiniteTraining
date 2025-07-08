using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment7
{
    // Question 1
    class SquareQuery
    {
        public IEnumerable<int> SquareReturn(List<int> list)
        {
            IEnumerable<int> result = from num in list
                         where num > 10
                         select num * num;
            return result;
        }
    }
    // Question 2
    class StringPatternQuery
    {
        public IEnumerable<string> StringReturn(List<string> list)
        {
            var result = from word in list
                         where (word.ToLower().StartsWith("a") && word.ToLower().EndsWith("m"))
                         select word;
            return result;
        }
    }
    // Question 3
    class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpCity { get; set; }
        public int EmpSalary { get; set; }

        public void DisplayAll(List<Employee> emplist)
        {
            Console.WriteLine("***DisplayAll***");
            foreach (var employee in emplist)
            {
                Console.WriteLine($"Employee Id: {employee.EmpId}, Employee Name: {employee.EmpName}, Employee City: {employee.EmpCity}, Employee Salary: {employee.EmpSalary}");
            }
        }
        public void DisplayEmployeeSalaryMore45000(List<Employee> emplist)
        {
            Console.WriteLine("***DisplayEmployeeSalaryMoreThan45k***");
            var data = from employee in emplist
                       where employee.EmpSalary > 45000
                       select employee;
            foreach (var employee in data)
            {
                Console.WriteLine($"Employee Id: {employee.EmpId}, Employee Name: {employee.EmpName}, Employee City: {employee.EmpCity}, Employee Salary: {employee.EmpSalary}");
            }
        }
        public void DisplayEmployeeBangalore(List<Employee> emplist)
        {
            Console.WriteLine("***DisplayEmployeeBangalore***");
            var data = from employee in emplist
                       where employee.EmpCity == "Bangalore"
                       select employee;
            foreach (var employee in data)
            {
                Console.WriteLine($"Employee Id: {employee.EmpId}, Employee Name: {employee.EmpName}, Employee City: {employee.EmpCity}, Employee Salary: {employee.EmpSalary}");
            }
        }
        public void DisplayEmployeeAscending(List<Employee> emplist)
        {
            Console.WriteLine("***DisplayEmployeeAscending***");
            var data = emplist.OrderBy(e => e.EmpName).ToList();
            foreach (var employee in data)
            {
                Console.WriteLine($"Employee Id: {employee.EmpId}, Employee Name: {employee.EmpName}, Employee City: {employee.EmpCity}, Employee Salary: {employee.EmpSalary}");
            }
        }
    }
    // Question 4
    class Library
    {
        public int TotalFare = 500;
        public string Name;
        public int Age;
        public void CalculateConcession(int age)
        {
            if (age <= 5)
                Console.WriteLine("Little Champs - Free Ticket");
            else if (age > 60)
                Console.WriteLine($"Senior Citizen, 30% discounted Fare: {TotalFare * 0.7}"); // 30% percentage discount
            else
                Console.WriteLine("Ticket Booked, Fare: " + TotalFare);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1
            Console.WriteLine("-----GreaterThan10SquaringQuery-----");
            List<int> List = new List<int>();
            Console.Write("Enter the length of the list: ");
            int LenghtList = Convert.ToInt32(Console.ReadLine());
            // Input
            for (int i = 0; i < LenghtList; i++)
            {
                Console.Write($"Enter the element of the list at the position {i + 1}: ");
                List.Add(Convert.ToInt32(Console.ReadLine()));
            }

            SquareQuery sq = new SquareQuery();
            var result = sq.SquareReturn(List);
            // Output
            foreach (var v in result)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();

            // Question 2
            Console.WriteLine("-----WordCheckStartswithA_AndEndsWithM_Query-----");
            List<string> ListString = new List<string>();
            Console.Write("Enter the length of the list: ");
            int LenghtListString = Convert.ToInt32(Console.ReadLine());
            // Input
            for (int i = 0; i < LenghtListString; i++)
            {
                Console.Write($"Enter the string at position {i + 1}: ");
                ListString.Add(Console.ReadLine());
            }
            StringPatternQuery spq = new StringPatternQuery();
            var resultString = spq.StringReturn(ListString);

            // Output
            foreach (var v in resultString)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();

            // Question 3
            Console.WriteLine("-----Employee-----");
            List<Employee> EmpList = new List<Employee>();
            Console.Write("Enter the number of Employee details you want to enter: ");
            int EmployeeCount = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < EmployeeCount; i++)
            {
                Employee emp = new Employee();
                Console.Write($"Enter the Employee Id of Employee {i + 1}: ");
                emp.EmpId = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Enter the Employee Name of Employee {i + 1}: ");
                emp.EmpName = Console.ReadLine();
                Console.Write($"Enter the Employee City of Employee {i + 1}: ");
                emp.EmpCity = Console.ReadLine();
                Console.Write($"Enter the Employee Salary of Employee {i + 1}: ");
                emp.EmpSalary = Convert.ToInt32(Console.ReadLine());

                EmpList.Add(emp);
            }
            Employee employee = new Employee();
            employee.DisplayAll(EmpList);
            employee.DisplayEmployeeSalaryMore45000(EmpList);
            employee.DisplayEmployeeBangalore(EmpList);
            employee.DisplayEmployeeAscending(EmpList);



            // Question 4
            Console.WriteLine("-----Library-----");

            Console.Write("Enter the number of booking you want to book: ");
            int BookingCount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < BookingCount; i++)
            {
                Console.WriteLine($"***BookingForPerson{i+1}***");
                Console.Write("Enter the Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter the Age: ");
                int age = Convert.ToInt32(Console.ReadLine());

                Library lib = new Library();
                lib.Name = name;
                lib.Age = age;
                lib.CalculateConcession(age);
            }

            Console.Write("Press Enter to exit: ");
            Console.ReadLine();
        }
    }
}
