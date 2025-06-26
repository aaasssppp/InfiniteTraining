using System;

namespace Assignment3
{
    class Accounts
    {
        string accountNo, customerName, accountType;
        char transactionType;
        int amount, balance;
        public Accounts(string accNo, string custName, string accType)
        {
            accountNo = accNo;
            customerName = custName;
            accountType = accType;
        }
        public void Transaction()
        {
            Console.WriteLine("Enter the type of transaction (D->Deposit, W->Withdrawl): ");
            char transType = Convert.ToChar(Console.ReadLine().ToLower());

            BalanceUpdate(transType);
        }
        public void BalanceUpdate(char transType)
        {
            transactionType = transType;
            
            if (transactionType == 'd')
            {
                Console.WriteLine("Enter the amount to be deposited: ");
                amount = int.Parse(Console.ReadLine());

                Credit(amount);
            }
            else if (transactionType == 'w')
            {
                Console.WriteLine("Enter the amount to be withdrawn: ");
                int amount = int.Parse(Console.ReadLine());

                Debit(amount);
            }
        }
        public void Debit(int amount)
        {
            balance -= amount;
        }
        public void Credit(int amount)
        {
            balance += amount;
        }
        public void ShowData()
        {
            Console.WriteLine("Account Number: " + accountNo);
            Console.WriteLine("Customer Name: " + customerName);
            Console.WriteLine("Account Type: " + accountType);
            Console.WriteLine("Current Balance: " + balance);
        }
    }
    class Student
    {
        string RollNo, Name, Branch;
        int Class, Semester;
        int [] marks = new int[5];
        public Student(string rollno, string name, int Class, int Sem, string branch)
        {
            RollNo = rollno;
            Name = name;
            Branch = branch;
            this.Class = Class;
            Semester = Sem;
        }
        public void SetMarks()
        {
            Console.WriteLine("***Setting Marks***");
            for (int i = 0; i < marks.Length; i++)
            {
                Console.WriteLine($"Enter the Subject {i+1} Marks: ");
                marks[i] = Convert.ToInt32(Console.ReadLine());
            }
        }
        public void GetMarks()
        {
            Console.WriteLine("***Getting Marks***");
            for (int i = 0; i < marks.Length; i++)
            {
                Console.WriteLine($"Subject {i+1} Marks: {marks[i]}");
            }
        }
        public string DisplayResult()
        {
            int total=0;
            foreach(int mark in marks)
            {
                total += mark;
                if(mark < 35)
                    return "failed";
            }
            int Average = total / marks.Length;
            if (Average < 50)
                return "failed";
            return "passed";
        }
        public void DisplayData()
        {
            Console.WriteLine("***Displaying Data*****");
            Console.WriteLine($"Roll No: {RollNo} \nName: {Name}\nBranch: {Branch}\n Class: {Class}\nSemester: {Semester}");
        }

    }
    class SaleDetails
    {
        static string SalesNo, ProductNo;
        static int Price, Qty, TotalAmount;
        static DateTime DateOfSale;

        public SaleDetails(string salesno, string productno, int price, int qty, DateTime date)
        {
            SalesNo = salesno;
            ProductNo = productno;
            Price = price;
            Qty = qty;
            DateOfSale = date;
        }
        public void Sales(int qty, int price)
        {
            TotalAmount = qty * price;
        }
        public static void ShowData()
        {
            Console.WriteLine($"SalesNo: {SalesNo}\nProductNo: {ProductNo}\nPrice: {Price}\nQuantity: {Qty}\nDateOfSale: {DateOfSale}\nTotalAmount: {TotalAmount}");
        }
    }

    class main
    {
        static void Main(string[] args)
        {
            /* // Question 1
            Console.WriteLine("-----Accounts-----");
            string accNo, custName, accType;
            Console.WriteLine("Enter the Account Number: ");
            accNo = Console.ReadLine();
            Console.WriteLine("Enter the Customer Name: ");
            custName = Console.ReadLine();
            Console.WriteLine("Enter the Account Type: ");
            accType = Console.ReadLine();

            Accounts acc = new Accounts(accNo, custName, accType);

            acc.Transaction();

            acc.ShowData();
            */

            /* // Question 2
            Console.WriteLine("-----Student Result-----");
            Console.WriteLine("Enter the RollNo of the Student: ");
            string rollno = Console.ReadLine();
            Console.WriteLine("Enter the Name of the Student: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the Class: ");
            int Class = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Semester: ");
            int Sem = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Branch: ");
            string branch = Console.ReadLine();
            Student st = new Student(rollno, name, Class, Sem, branch);
            st.SetMarks();
            st.GetMarks();
            Console.WriteLine("***Displaying Result***");
            Console.WriteLine(st.DisplayResult())   ;
            st.DisplayData();
            */

            // Question 3
            Console.WriteLine("-----Sales Details-----");
            // Creating an object and passing values to the constructor
            SaleDetails sale = new SaleDetails("S001", "P123", 100, 5, DateTime.Now);

            // Calling the Sales method to calculate the total amount
            sale.Sales(5, 100);

            // Since ShowData is static, it's called using the class name
            SaleDetails.ShowData();



            Console.ReadLine();
        }
    }
}
