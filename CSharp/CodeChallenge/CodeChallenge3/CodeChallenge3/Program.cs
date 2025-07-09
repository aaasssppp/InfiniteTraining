using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge3
{
    // Question 1
    class CricketTeam
    {
        public (int, double, int) PointsCalculation(int no_of_matches)
        {
            int sumScore = 0;
            for (int i = 0; i < no_of_matches; i++)
            {
                Console.Write($"Enter the runs scored in the match {i + 1}: ");
                sumScore += Convert.ToInt32(Console.ReadLine());
            }
            return (no_of_matches, (sumScore / no_of_matches), sumScore);
        }
    }
    // Question 2
    class Box
    {
        public int Length, Breadth;

        public static Box operator +(Box b1, Box b2)
        {
            Box newBox = new Box();
            newBox.Length = b1.Length + b2.Length;
            newBox.Breadth = b1.Breadth + b2.Breadth;
            //Console.WriteLine($"b1 Length: {b1.Length} + b2.Length: {b2.Length} and b3.Length: {newBox.Length}");
            //Console.WriteLine($"b1 Bredth: {b1.Breadth} + b2.Breadth: {b2.Breadth} and b3.breadth: {newBox.Breadth}");
            return newBox;
        }
        public override string ToString()
        {
            return $"Length of newBox is: {this.Length}\nBreadth of newBox is: {this.Breadth}";
        }
    }
    // Question 3
    class AppendText
    {
        public void Append(string path, string texttoappend)
        {
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(texttoappend);
            }
        }   
    }
    // Question 4
    class Calculator
    {
        public delegate void Calculate(int num1, int num2);

        public void Addition(int num1, int num2)
        {
            Console.WriteLine($"Addition of {num1} and {num2} is: { num1 + num2}");
        }
        public void Subraction(int num1, int num2)
        {
            Console.WriteLine($"Subraction of {num1} and {num2} is: { num1 - num2}");
        }
        public void Multiplication(int num1, int num2)
        {
            Console.WriteLine($"Multiplication of {num1} and {num2} is: { num1 * num2}");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1
            Console.WriteLine("-----CricketTeam-----");
            CricketTeam crickteam = new CricketTeam();
            Console.Write("Enter the number of matches played: ");
            int numMatches = Convert.ToInt32(Console.ReadLine());
            var result = crickteam.PointsCalculation(numMatches);
            Console.WriteLine($"The count of Matches is: {result.Item1}\nThe Average score is: {result.Item2}\nThe sum of scores is: {result.Item3}");

            // Question 2
            Console.WriteLine("-----AddBox-----");
            Box b1 = new Box();
            Console.Write("Enter the Length of box1: ");
            b1.Length = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the Breadth of box1: ");
            b1.Breadth = Convert.ToInt32(Console.ReadLine());

            Box b2 = new Box();
            Console.Write("Enter the Length of box1: ");
            b2.Length = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the Breadth of box1: ");
            b2.Breadth = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine((b1 + b2).ToString());

            // Question 3
            Console.WriteLine("-----AppendText-----");

            string Path = @"C:\Syam Prasad\Training\git_Assignments\CSharp\CodeChallenge\CodeChallenge3\AppendText.txt";
            Console.WriteLine("Enter the string to append: ");
            string texttoappend = Console.ReadLine();
            AppendText appendtext = new AppendText();
            appendtext.Append(Path, texttoappend);

            // Question 4
            Console.WriteLine("-----DelegateCalculator-----");
            Calculator calc = new Calculator();
            Calculator.Calculate calcdeleg = calc.Addition;
            calcdeleg += calc.Subraction;
            calcdeleg += calc.Multiplication;

            Console.Write("Enter the number 1: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the number 2: ");
            int num2 = Convert.ToInt32(Console.ReadLine());
            calcdeleg(num1, num2);

            Console.Write("Press Enter to exit");
            Console.ReadLine();
        }
    }
}
