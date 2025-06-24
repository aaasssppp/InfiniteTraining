using System;


namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            Swap2Numbers();
            NumberDisplay();
            DayNumber();
            Arrays.AssignValuesToArrayNCompute();
            Arrays.TenMarksNCompute();
            Arrays.CopyArray();
            StringsWNoFunction.DisplayWordLength();
            StringsWNoFunction.DisplayReverseWord();
            StringsWNoFunction.TwoWordsSameONot();
            StringsWFunction.DisplayWordLength();
            StringsWFunction.TwoWordsSameONot();
            Console.Read();
        }
        static void Swap2Numbers()
        {
            Console.WriteLine("-----Swap2Numbers-----");
            Console.WriteLine("Enter the value of num1: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the value of num2: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("***Before Swapping***");
            Console.WriteLine($"num1 = {num1}, num2 = {num2}");

            // code for swapping
            // using temporary variable
            int temp = num1;
            num1 = num2;
            num2 = temp;

            Console.WriteLine("***After Swapping***");
            Console.WriteLine($"num1 = {num1}, num2 = {num2}");

        }

        static void NumberDisplay()
        {
            Console.WriteLine("-----NumberDisplay-----");
            Console.WriteLine("Enter the number to be displayed: ");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("{0} {0} {0} {0}\n{0}{0}{0}\n{0} {0} {0} {0}\n{0}{0}{0}", num);
        }
        enum Days { Monday = 1, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };
        static void DayNumber()
        {
            Console.WriteLine("-----DayNumber-----");
            Console.WriteLine("Enter the day number: ");
            int dayNum = Convert.ToInt32(Console.ReadLine());

            switch (dayNum)
            {
                case (int)Days.Monday:
                    Console.WriteLine("Monday");
                    break;
                case (int)Days.Tuesday:
                    Console.WriteLine("Tuesday");
                    break;
                case (int)Days.Wednesday:
                    Console.WriteLine("Wednesday");
                    break;
                case (int)Days.Thursday:
                    Console.WriteLine("Thursday");
                    break;
                case (int)Days.Friday:
                    Console.WriteLine("Friday");
                    break;
                case (int)Days.Saturday:
                    Console.WriteLine("Saturday");
                    break;
                case (int)Days.Sunday:
                    Console.WriteLine("Sunday");
                    break;
            }
        }
    }
    class Arrays
    {
        public static void AssignValuesToArrayNCompute()
        {
            Console.WriteLine("-----AssignValuesToArrayNCompute-----");
            Console.WriteLine("Enter the number of elements in array: ");
            int numElts = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[numElts];
            for (int i= 0; i<numElts; i++)
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }

            // Average, Min and Max
            int sum = 0;
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (int i in arr)
            {
                if(i<min)
                {
                    min = i;
                }
                if(i>max)
                {
                    max = i;
                }
                sum += i;
            }
            Console.WriteLine($"Average: {sum/numElts}");
            Console.WriteLine("Maximum value: " + max);
            Console.WriteLine("Minimum value: " + min);

        }
        public static void TenMarksNCompute()
        {
            Console.WriteLine("-----TenMarksNCompute-----");
            int numElts = 10;
            int[] marksArr = new int[numElts];
            for(int i=0; i<numElts ;i++)
            {
                marksArr[i] = Convert.ToInt32(Console.ReadLine());
            }

            // Total, Average, Min, Max
            int total = 0;
            int minimum = int.MaxValue;
            int maximum = int.MinValue;
            foreach (int i in marksArr)
            {
                if(i<minimum)
                {
                    minimum = i;
                }
                if(i>maximum)
                {
                    maximum = i;
                }
                total += i;
            }
            Console.WriteLine("Total marks: " + total);
            Console.WriteLine($"Average marks: {total/numElts}");
            Console.WriteLine("Maximim marks: " + maximum);
            Console.WriteLine("Minimum marks: " + minimum);

            // Ascending order

            Console.WriteLine("***Ascending order***");

            for (int i= 0; i<numElts; i++)
            {
                for(int j=i+1; j<numElts; j++)
                {
                    if (marksArr[i] > marksArr[j])
                    {
                        int temp = marksArr[j];
                        marksArr[j] = marksArr[i];
                        marksArr[i] = temp;
                    }
                }
            }
            foreach(int i in marksArr)
            {
                Console.Write(i);
                Console.Write(' ');
            }
            Console.WriteLine();

            // descending order

            Console.WriteLine("***Descending order***");

            for (int i=numElts-1; i>=0; i--)
            {
                Console.Write(marksArr[i]);
                Console.Write(' ');
            }
            Console.WriteLine();
        }
        public static void CopyArray()
        {
            Console.WriteLine("-----CopyArray-----");
            // Enter your number of elements
            Console.WriteLine("Enter the number of elements in your array: ");
            int numElts = Convert.ToInt32(Console.ReadLine());

            // Enter your array elements
            Console.WriteLine("Enter the elements in your array: ");
            int[] yourArr = new int[numElts];
            for(int i=0; i<numElts; i++)
            {
                yourArr[i] = Convert.ToInt32(Console.ReadLine());
            }

            // Displaying yourArr
            Console.WriteLine("***yourArray***");
            foreach(int i in yourArr)
            {
                Console.Write(i);
                Console.Write(' ');
            }
            Console.WriteLine();


            int[] copyArr = new int[numElts];

            // Copying yourArr into copyArr
            for(int i=0; i<numElts; i++)
            {
                copyArr[i] = yourArr[i];
            }

            // Displaying copyArr
            Console.WriteLine("***copyArray***");
            foreach (int i in yourArr)
            {
                Console.Write(i);
                Console.Write(' ');
            }
            Console.WriteLine();
        }
    }

    class StringsWFunction
    {
        public static void DisplayWordLength()
        {
            Console.WriteLine("-----DisplayLength-----");

            Console.WriteLine("Enter a word: ");

            Console.WriteLine("Your word length is: " + Console.ReadLine().Length);
        }
        //public static void DisplayReverseWord()
        //{
        //    Console.WriteLine("-----DisplayReverseWord-----");

        //    Console.WriteLine("Enter a word: ");

        //    Console.WriteLine("Reverse of your word is: " + Console.ReadLine());
        //}
        public static void TwoWordsSameONot()
        {
            Console.WriteLine("-----TwoWordsSameONot-----");

            Console.WriteLine("Enter the first word: ");
            string wordOne = Console.ReadLine();

            Console.WriteLine("Enter the second word: ");
            string wordTwo = Console.ReadLine();

            if (wordOne.Equals(wordTwo))
            {
                Console.WriteLine($"The first word: {wordOne} and the second word: {wordTwo} are same");
            }
            else
            {
                Console.WriteLine($"The first word: {wordOne} and the second word: {wordTwo} are not same");
            }
        }
    }
    class StringsWNoFunction
    {
        public static void DisplayWordLength()
        {
            Console.WriteLine("-----DisplayLength-----");

            Console.WriteLine("Enter a word: ");

            string yourWord = Console.ReadLine();

            int yourWordLength = 0;
            foreach(char i in yourWord)
            {
                yourWordLength += 1;
            }
            Console.WriteLine("Your word length is: " + yourWordLength);
        }
        public static void DisplayReverseWord()
        {
            Console.WriteLine("-----DisplayReverseWord-----");

            Console.WriteLine("Enter a word: ");
            string yourWord = Console.ReadLine();

            Console.WriteLine("Reverse of your word is: ");
            for(int i = yourWord.Length-1; i>=0; i--)
            {
                Console.Write(yourWord[i]);
            }
            Console.WriteLine();
        }
        public static void TwoWordsSameONot()
        {
            Console.WriteLine("-----TwoWordsSameONot-----");

            Console.WriteLine("Enter the first word: ");
            string wordOne = Console.ReadLine();

            Console.WriteLine("Enter the second word: ");
            string wordTwo = Console.ReadLine();

            if(wordOne == wordTwo)
            {
                Console.WriteLine($"The first word: {wordOne} and the second word: {wordTwo} are same");
            }
            else
            {
                Console.WriteLine($"The first word: {wordOne} and the second word: {wordTwo} are not same");
            }
        }
    }
}
