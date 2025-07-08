using System;
using System.IO;

namespace Assignment6
{
    // Question 1

    // Question 2
    class WriteFile
    { 
        string[] strarr;
       public WriteFile(string[] arr)
        {
            strarr = arr;
        }

        string Path = @"C:\Syam Prasad\Training\git_Assignments\CSharp\Assignments\Assignment6\WriteFile.txt";
        public void Write() {
            using (StreamWriter sw = new StreamWriter(Path)) 
            {
                foreach( var str in strarr)
                {
                    sw.Write(str);
                    sw.WriteLine();
                }
            }
        }
    }

    // Question 3
    class CountLines
    {
        string Path = @"C:\Syam Prasad\Training\git_Assignments\CSharp\Assignments\Assignment6\WriteFile.txt";
        public void Count()
        {
            int count = 0;
            using (StreamReader sr = new StreamReader(Path))
            {
                string Line;
                while((Line = sr.ReadLine()) != null)
                {
                    count += 1;
                }
            }
            Console.WriteLine($"The file has {count} lines");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1
            // already done on previous assignment

            // Question 2
            Console.WriteLine("Enter the length of string array: ");
            int ArrayLength = Convert.ToInt32(Console.ReadLine());
            string[] stringArray = new string[ArrayLength];
            for (int i=0; i<ArrayLength; i++)
            {
                Console.WriteLine($"Enter the string at position {i+1}: ");
                stringArray[i] = Console.ReadLine();
            }

            WriteFile writefile = new WriteFile(stringArray);
            writefile.Write();

            // Question 3
            CountLines countlines = new CountLines();
            countlines.Count();

            Console.ReadLine();
        }
    }
}
