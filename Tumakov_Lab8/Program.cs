using System;
using System.IO;

namespace Tumakov_Lab8
{
    public class BankAccount
    {
        protected int AccountNumber;
        protected decimal Balance;
        protected AccountType AccountType;

        public BankAccount( decimal balance, AccountType accountType)
        {
            AccountNumber = UniqueBankAccount.GenerateAccountNumber();
            Balance = balance;
            AccountType = accountType;
        }

        public int GetAccountNumber()
        {
            return AccountNumber;
        }

        public void SetAccountNumber(int accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public decimal GetBalance()
        {
            return Balance;
        }

        public void SetBalance(decimal balance)
        {
            Balance = balance;
        }

        public AccountType GetAccountType()
        {
            return AccountType;
        }

        public void SetAccountType(AccountType accountType)
        {
            AccountType = accountType;
        }

        public override string ToString()
        {
            return $"Номер счета: {AccountNumber}, баланс: {Balance}, тип счета: {AccountType}";
        }
        /// <summary>
        /// снятие со счёта с средств, если это возможно
        /// </summary>
        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                throw new ArgumentException("Недостаточно средств");
            }

            Balance -= amount;
        }
        /// <summary>
        /// Пополнение счёта
        /// </summary>
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
        /// <summary>
        /// Перевод денег с одного счета на другой
        /// </summary>
        public void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
        {
            if (amount > fromAccount.Balance)
            {
                throw new ArgumentException("Недостаточно средств");
            }

            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
        }
    }

    public enum AccountType
    {
        Current,
        Saving
    }
    /// <summary>
    /// генерация различных номеров для аккаунтов
    /// </summary>
    public class UniqueBankAccount : BankAccount
    {
        public static int nextAccountNumber = 1;

        public UniqueBankAccount(decimal balance, AccountType accountType)
            : base(balance, accountType)
        {
        }

        public static int GenerateAccountNumber()
        {
            int accountNumber = nextAccountNumber;
            nextAccountNumber++;
            return accountNumber;
        }
    }
    class Song
    {
        string name; 
        string author;
        Song prev;

        public Song(string name, string author)
        {
            this.name = name;
            this.author = author;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetAuthor(string author)
        {
            this.author = author;
        }

        public void SetPrev(Song prev)
        {
            this.prev = prev;
        }

        public string Title()
        {
            return this.name + " - " + this.author;
        }

        public override bool Equals(object d)
        {
            if (d is Song)
            {
                Song otherSong = (Song)d;
                return this.name == otherSong.name && this.author == otherSong.author;
            }

            return false;
        }
    }


    internal class Program
    {
        public static string ReverseString(string input)
        {
            char[] reversed = new char[input.Length];

            for (int i = input.Length - 1; i >= 0; i--)
            {
                reversed[input.Length - i - 1] = input[i];
            }

            return new string(reversed);
        }
        public static bool IsFormattable(object value)
        {
            return value is IFormattable;
        }


        static void Main(string[] args)
        {
            void SearchMail(ref string s)
            {
                int index = s.IndexOf('#');
                if (index != -1)
                {
                    s = s.Substring(index + 1);
                }
                else
                {
                    s = "";
                }
            }
            Console.WriteLine("  Лабораторная работа 8\nУпражнение 8.1:\n В класс банковский счет, созданный в упражнениях 7.1- 7.3 добавить\r\n метод, который переводит деньги с одного счета на другой. У метода два параметра: ссылка\r\n на объект класса банковский счет откуда снимаются деньги, второй параметр – сумма.");
            BankAccount Account1 = new BankAccount( 10000, AccountType.Current);
            BankAccount Account2 = new BankAccount( 5000, AccountType.Saving);
            Console.WriteLine("Два банковский аккаунта:");
            Console.WriteLine(Account1);
            Console.WriteLine(Account2);
            Console.WriteLine("Выполним перевод 5000 с первого на второй");
            Account1.Transfer(Account1,Account2, 5000);
            Console.WriteLine(Account1);
            Console.WriteLine(Account2);
            Console.WriteLine("Нажмите Enter");
            Console.ReadKey();

            Console.WriteLine(" Упражнение 8.2: Реализовать метод, который в качестве входного параметра принимает\r\n строку string, возвращает строку типа string, буквы в которой идут в обратном порядке.\r\n Протестировать метод.");
            string input = "Hello, world!";
            Console.WriteLine($"Исходная строка {input}");
            string reversed = ReverseString(input);
            Console.WriteLine($"Перевернутая строка: {reversed}");
            Console.WriteLine("Нажмите Enter");
            Console.ReadKey();

            Console.WriteLine(" Упражнение 8.3: Написать программу, которая спрашивает у пользователя имя файла. Если\r\n такого файла не существует, то программа выдает пользователю сообщение и заканчивает\r\n работу, иначе в выходной файл записывается содержимое исходного файла, но заглавными\r\n буквами.");
            Console.WriteLine("Введите имя файла:");
            string inputFileName = Console.ReadLine();
            FileInfo inputFile = new FileInfo(inputFileName); // файл с названием test.txt существует
            if (!inputFile.Exists )
            {
                Console.WriteLine($"Файл {inputFileName} не существует.");
                
            }
            else
            {
                StreamReader reader = new StreamReader(inputFileName);
                string outputFileName = "upper." + inputFileName;
                StreamWriter outputWriter = new StreamWriter(outputFileName);
                string line;
                while ((line = reader.ReadLine()) != null)
                { outputWriter.WriteLine(line.ToUpper()); }
                reader.Close();
                outputWriter.Close();
            }
            Console.WriteLine("Нажмите Enter");
            Console.ReadKey();

            Console.WriteLine("Упражнение 8.4: Реализовать метод, который проверяет реализует ли входной параметр\r\nметода интерфейс System.IFormattable. Использовать оператор is и as.");
            Console.WriteLine("Введите что хотите проверить");
            int number = 10;
            string text = "Hello, world!";
            Console.WriteLine($"Число является форматируемым: {IsFormattable(number)}");
            Console.WriteLine($"Текст является форматируемым: {IsFormattable(text)}");
            Console.WriteLine("Нажмите Enter");
            Console.ReadKey();

            Console.WriteLine("Домашнее задание 8.1: Работа со строками. Дан текстовый файл, содержащий ФИО и e-mail\r\nадрес.Сформировать новый файл, содержащий список адресов электронной почты.");
            StreamReader readermail = new StreamReader("input.txt");
            StreamWriter writermail = new StreamWriter("output.txt");
            string linee;
            while ((linee = readermail.ReadLine()) != null)
            {
                SearchMail(ref linee);
                writermail.WriteLine(linee);
            }
            readermail.Close();
            writermail.Close();
            Console.WriteLine("Mail записаны в файл output.txt");
            Console.WriteLine("Нажмите Enter");
            Console.ReadKey();

            Console.WriteLine("Домашнее задание 8.2: Список песен. В методе Main создать список из четырех песен. В\r\nцикле вывести информацию о каждой песне. Сравнить между собой первую и вторую\r\nпесню в списке.");
            Song[] songs = new Song[]
            {
            new Song("Imagine", "John Lennon"),
            new Song("Yesterday", "The Beatles"),
            new Song("Bohemian Rhapsody", "Queen"),
            new Song("Hotel California", "Eagles")
            };
            Console.WriteLine("Список песен:");
            foreach (Song s in songs)
            {
                Console.WriteLine(s.Title());
            }
            if (songs[0].Equals(songs[1]))
            {
                Console.WriteLine("Певрая и вторая песня равны");
            }
            else
            {
                Console.WriteLine("Певрая и вторая песня Не равны");
            }
            Console.WriteLine("Нажмите Enter");
            Console.ReadKey();
        }
    }
}
