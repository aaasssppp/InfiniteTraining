using System;

namespace CodeChallenge1
{
    class CodeChallenge1
    {
        static string StringCharRemove(string word, int index)
        {
            string removedWord = "";
            for (int i=0; i<word.Length; i++)
            {
                if (i == index)
                    continue;
                removedWord += word[i];
            }
            return removedWord;
        }
        static string ExchangeChars(string word)
        {
            string exchangedWord = "";
            if (word.Length == 1)
                return word;
            for(int i=1; i<word.Length-1; i++)
            {
                exchangedWord += word[i];
            }
            return (word[word.Length - 1] + exchangedWord + word[0]);
        }
        static int LargestNumber(int num1, int num2, int num3)
        {
            int maxNum = int.MinValue;
            if (num1 > maxNum)
                maxNum = num1;
            if (num2 > maxNum)
                maxNum = num2;
            if (num3 > maxNum)
                maxNum = num3;
            return maxNum;
        }

        static void Main(string[] args)
        {
            // Question 1
            Console.WriteLine("-----StringCharRemove-----");
            Console.WriteLine("Enter the word: ");
            string word = Console.ReadLine();
            Console.WriteLine("Enter the index number of word to be removed: ");
            int index = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(StringCharRemove(word, index));


            // Question 2
            Console.WriteLine("-----ExchangeChars-----");
            Console.WriteLine("Enter the word: ");
            string word1 = Console.ReadLine();
            Console.WriteLine(ExchangeChars(word1));


            // Question 3
            Console.WriteLine("-----LargestNumber-----");
            Console.WriteLine("Enter the number 1: ");
            int num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the number 2: ");
            int num2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the number 3: ");
            int num3 = int.Parse(Console.ReadLine());
            Console.WriteLine(LargestNumber(num1, num2, num3));

            Console.ReadLine();
        }
    }
}
