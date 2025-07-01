using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    class EmployeeManagement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }

        List<EmployeeManagement> empmanlist = new List<EmployeeManagement>();
        internal void AddNewEmployee()
        {
            EmployeeManagement em = new EmployeeManagement();
            Console.WriteLine("Enter the Employee Id: ");
            em.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Employee Name: ");
            em.Name = Console.ReadLine();
            Console.WriteLine("Enter the Employee Department: ");
            em.Department = Console.ReadLine();
            Console.WriteLine("Enter the Employee Salary: ");
            em.Salary = Convert.ToInt32(Console.ReadLine());

            empmanlist.Add(em);
            Console.WriteLine($"Employee with Id: {em.Id} added!");
        }
        internal void ViewAllEmployees()
        {
            Console.WriteLine("----- ViewAllEmployees -----");
            foreach (EmployeeManagement employee in empmanlist)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Department: {employee.Department}, Salary: {employee.Salary}");
            }
        }
        internal void SearchEmployeeByID()
        {
            Console.WriteLine($"----- SearchEmployeeByID -----");
            Console.WriteLine("Enter the Employee ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            foreach (EmployeeManagement employee in empmanlist)
            {
                if (employee.Id == id)
                    Console.WriteLine($"The employee with ID: {id}, Name: {employee.Name}, Department: {employee.Department}, Salary: {employee.Salary}");
            }
        }
        internal void UpdateEmployeeDetails()
        {
            Console.WriteLine($"----- UpdateEmployeeDetails ----");

            Console.WriteLine("Enter the Employee Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            foreach (EmployeeManagement employee in empmanlist)
            {
                if (employee.Id == id)
                {
                    Console.WriteLine("Enter the Employee Name: ");
                    employee.Name = Console.ReadLine();

                    Console.WriteLine("Enter the Employee Department: ");
                    employee.Department = Console.ReadLine();

                    Console.WriteLine("Enter the Employee Salary: ");
                    employee.Salary = Convert.ToInt32(Console.ReadLine());
                }
                else
                    Console.WriteLine($"There is no such records found! with Employee Id: {id}. Create new record if you want");
            }
            Console.WriteLine($"*** Updateded Employee with Id: {id}***");
        }
        internal void DeleteEmployee()
        {
            Console.WriteLine("----- Delete Employee -----");
            Console.WriteLine("Enter the Employee ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            for (int empind = 0; empind < empmanlist.Count; empind++)
            {
                if (empmanlist[empind].Id == id)
                    empmanlist.Remove(empmanlist[empind]);
            }
            Console.WriteLine($"*** Removed Employee with Id: {id} ***");
        }
    }
    class Scenrio_GenericCollectionList
    {
        static void Main(string[] args)
        {
            EmployeeManagement empman = new EmployeeManagement();

            int input;
            do
            {
                Console.WriteLine("Enter 1 to add new Employee\nEnter 2 to view all Employee\nEnter 3 to search Employee by ID\nEnter 4 to update Employee details\nEnter 5 to delete Employee\nEnter 6 to exit");
                input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        empman.AddNewEmployee();
                        break;
                    case 2:
                        empman.ViewAllEmployees();
                        break;
                    case 3:
                        empman.SearchEmployeeByID();
                        break;
                    case 4:
                        empman.UpdateEmployeeDetails();
                        break;
                    case 5:
                        empman.DeleteEmployee();
                        break;
                    case 6:
                        Console.WriteLine("Exiting the loop. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number from 1 to 6.");
                        break;
                }
            } while (input != 6);
            Console.ReadLine();
        }
    }
}
