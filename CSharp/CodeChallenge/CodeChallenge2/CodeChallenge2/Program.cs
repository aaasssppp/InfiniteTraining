using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge2
{
    // Question 1
    abstract class Student
    {
        public string Name;
        public int StudentId;
        public float Grade;

        // abstract method
        abstract public bool IsPassed(float grade);
    }
    class Undergraduate : Student
    {
        public override bool IsPassed(float grade)
        {
            if (grade > 70)
                return true;
            return false;
        }
    }
    class Graduate : Student
    {
        public override bool IsPassed(float grade)
        {
            if (grade > 80)
                return true;
            return false;
        }
    }
    // Question 2
    class Products : IComparable<Products>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }

        // Implement CompareTo to sort by Price
        public int CompareTo(Products other)
        {
            return this.Price.CompareTo(other.Price); // Ascending order
        }

        public void Display()
        {
            Console.WriteLine($"ID: {ProductId}, Name: {ProductName}, Price: {Price}");
        }

    }

    // Question 3
    class NegativeException : Exception
    {
        public NegativeException(string message) : base(message) { }
    }
    class CheckNegative
    {
        public CheckNegative(int checkinteger)
        {
            try
            {
                ValidateNegative(checkinteger);
            }
            catch (NegativeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ValidateNegative(int checkinteger)
        {
            if (checkinteger < 0)
                throw new NegativeException("Custom error: The number is negative!");
            Console.WriteLine("Yay! your number is positive!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1
            Console.WriteLine("-----Student passed or not-----");
            Console.WriteLine("Enter the Education level: (Undergraduate or Graduate): ");
            string edulevel = Console.ReadLine();

            Console.WriteLine("Enter the Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the Student Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Student Grade: ");
            float grade = Convert.ToSingle(Console.ReadLine());

            if (edulevel == "Undergraduate")
            {
                Undergraduate undgrad = new Undergraduate() { Name = name, StudentId = id, Grade = grade };
                if (undgrad.IsPassed(undgrad.Grade) == true)
                    Console.WriteLine("Passed!");
                else
                    Console.WriteLine("Failed!");
            }
            else if (edulevel == "Graduate"){
                Graduate grad = new Graduate() { Name = name, StudentId = id, Grade = grade };
                if (grad.IsPassed(grad.Grade) == true)
                    Console.WriteLine("Passed!");
                else
                    Console.WriteLine("Failed!");
            }
            else
                Console.WriteLine("Invalid Education Level!");


            // Question 2
            Console.WriteLine("-----Sort Products by price-----");
            IList<Products> productList = new List<Products>();

            Console.WriteLine("Enter details of 10 products:");

            // Getting 10 products
            for (int i = 0; i < 10; i++)
            {
                Products p = new Products();

                Console.Write($"Enter Product ID of the product {i + 1}: ");
                p.ProductId = Convert.ToInt32(Console.ReadLine());

                Console.Write($"Enter Product Name of the product {i + 1}: ");
                p.ProductName = Console.ReadLine();

                Console.Write($"Enter Price of the product {i + 1}: ");
                p.Price = Convert.ToDouble(Console.ReadLine());

                productList.Add(p);
                Console.WriteLine();
            }

            // Sorting
            List<Products> sortedList = new List<Products>(productList);
            sortedList.Sort();

            Console.WriteLine("Products sorted by Price (Ascending):");
            foreach (Products p in sortedList)
            {
                p.Display();
            }

            // Question 3
            Console.WriteLine("-----Exception handling for negative numbers-----");
            Console.WriteLine("-----CheckNegative-----");
            Console.WriteLine("Enter the number :");
            CheckNegative check = new CheckNegative(Convert.ToInt32(Console.ReadLine()));

            Console.ReadLine();
        }
    }
}
