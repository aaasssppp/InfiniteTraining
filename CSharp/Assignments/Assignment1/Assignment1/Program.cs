using System;


namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            EqualOrNot();
            Console.WriteLine();
            PositiveOrNegative();
            Console.WriteLine();
            MathOperations();
            Console.WriteLine();
            MultiplicationTable();
            Console.WriteLine();
            SumOrTripleSum();
            Console.Read();
        }
        static void EqualOrNot()
        {
            Console.WriteLine("------EqualOrNot-----");
            Console.WriteLine("Enter the first number: ");
            int firstNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the second number: ");
            int secondNumber = Convert.ToInt32(Console.ReadLine());

            if(firstNumber == secondNumber)
            {
                Console.WriteLine("The first number is {0}, the second number is {1} and are equal", firstNumber, secondNumber);
            }
            else
            {
                Console.WriteLine("The first number is {0}, the second number is {1} and are not equal", firstNumber, secondNumber);
            }
        }
        static void PositiveOrNegative()
        {
            Console.WriteLine("-----PositiveOrNegative-----");
            Console.WriteLine("Enter a number to check whether it is positive or not: ");
            int PoNnum = int.Parse(Console.ReadLine());
            if(PoNnum >= 0)
            {
                Console.WriteLine("{0} is positive",PoNnum);
            }
            else
            {
                Console.WriteLine("{0} is negative",PoNnum);
            }
        }
        static void MathOperations()
        {
            Console.WriteLine("-----MathOperations-----");
            Console.WriteLine("Input first number: ");
            int firstNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Input operation: ");
            char operation = Convert.ToChar(Console.ReadLine());

            Console.WriteLine("Input second number: ");
            int secondNumber = Convert.ToInt32(Console.ReadLine());

            switch (operation)
                {
                case '-':
                    int ans = firstNumber - secondNumber;
                    Console.WriteLine("Subracting {0} and {1} ({0}-{1}) is {2}",firstNumber,secondNumber,ans);
                    break;
                case '+':
                    ans = firstNumber + secondNumber;
                    Console.WriteLine("" + "Adding {0} and {1} ({0}+{1}) is {2}", firstNumber, secondNumber, ans);
                    break;
                case '*':
                    ans = firstNumber * secondNumber;
                    Console.WriteLine("Multiplying {0} and {1} ({0}*{1}) is {2}", firstNumber, secondNumber, ans);
                    break;
                case '/':
                    ans = firstNumber / secondNumber;
                    Console.WriteLine("Dividing {0} and {1} ({0}/{1}) is {2}", firstNumber, secondNumber, ans);
                    break;
            }
        }
        static void MultiplicationTable()
        {
            Console.WriteLine("-----MultiplicatoinTable-----");
            Console.WriteLine("Enter the number: ");
            int val = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Multiplication table of {0} is",val);
            for(int i=0; i<=10; i++)
            {
                Console.WriteLine("{0} * {1} = {2}",val,i,val*i);
            }
        }
        static void SumOrTripleSum()
        {
            Console.WriteLine("-----SumOrTripleSum-----");
            Console.WriteLine("Enter the first number: ");
            int firstNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the second number: ");
            int secondNumber = Convert.ToInt32(Console.ReadLine());

            if(firstNumber == secondNumber)
            {
                Console.WriteLine("The two values are same, so the triple of their sum is: " + 3*(firstNumber+secondNumber));
            }
            else
            {
                Console.WriteLine("The sum is: " + firstNumber+secondNumber);
            }
        }
    }
}
