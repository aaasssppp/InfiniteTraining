using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    // Question 1
    class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() : base("You don’t have sufficient balance to withdraw"){ }
    }
    class BankingSystem
    {
        static int Balance = 0;
        
        internal void Deposit()
        {
            Console.WriteLine("Enter the amount to deposit: ");
            Balance += Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Amount deposited successfully!");
        }
        // Exception raising
        internal void Withdraw()
        {
            Console.WriteLine("Enter the amount to withdraw: ");
            int WithdrawAmount = Convert.ToInt32(Console.ReadLine());
            if (Balance > WithdrawAmount)
            {
                Balance -= WithdrawAmount;
                Console.WriteLine("Amount withdrawn successfully!");
            }
            else
                throw new InsufficientBalanceException();

        }
        internal void BalanceCheck()
        {
            Console.WriteLine("Your Balance amount is: " + Balance);
        }

    }
    // Question 2
    class InvalidMarkError : Exception
    {
        public InvalidMarkError(string message) : base(message) { }
    }
    class Scholorship
    {
        float ScholarshipAmount;
        public float Merit(int marks, int fees)
        {
            if (marks < 0 || marks > 100)
                throw new InvalidMarkError("Invalid mark!");
            if (marks >= 70 && marks <= 80)
                ScholarshipAmount = (float)0.2 * fees;
            else if (marks > 80 && marks <= 90)
                ScholarshipAmount = (float)0.3 * fees;
            else if (marks > 90 && marks > 100)
                ScholarshipAmount = (float)0.5 * fees;
            else
                throw new InvalidMarkError("Marks are insufficient for scholorship");
            return ScholarshipAmount;
        }
    }
    // Question 3
    class Books
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public Books(string bookname, string authorname)
        {
            BookName = bookname;
            AuthorName = authorname;
        }
        void Display()
        {
            Console.WriteLine($"Book name: {BookName}, Author name: {AuthorName}");
        }
    }
    class BookShelf
    {
        public Books[] books = new Books[5];

        public Books this[int index]
        {
            get
            {
                if (index >= 0 && index < books.Length)
                    return books[index];
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < books.Length)
                    books[index] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        public void DiplayBookShelf()
        {
            foreach(var book in books)
            {
                Console.WriteLine($"Book name: {book.BookName}, Author name: {book.AuthorName}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1
            Console.WriteLine("-----BankingSystem-----");
            try
            {
                BankingSystem banksys = new BankingSystem();
                banksys.Deposit();
                banksys.BalanceCheck();
                banksys.Withdraw();
                banksys.BalanceCheck();
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Request!");
            }

            // Question 2
            try
            {
                Console.WriteLine("-----ScholarshipAmount-----");
                Console.WriteLine("Enter the mark: ");
                int mark = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the fees: ");
                int fees = Convert.ToInt32(Console.ReadLine());
                Scholorship scsh = new Scholorship();
                Console.WriteLine("Your Scholarship amount is: " + scsh.Merit(mark, fees));
            }
            catch(InvalidMarkError ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception)
            {
                Console.WriteLine("Invalid input");
            }

            // Question 3

            BookShelf bkslf = new BookShelf();
            Console.WriteLine("-----BookShelf-----");
            for (int i=0; i<5; i++)
            {
                Console.WriteLine($"Enter the book name of Book {i +1}: ");
                string bookname = Console.ReadLine();
                Console.WriteLine($"Enter the author name of Book {i + 1}: ");
                string authorname = Console.ReadLine();
                bkslf[i] = new Books(bookname, authorname);
            }
            bkslf.DiplayBookShelf();

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
