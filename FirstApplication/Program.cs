using System;

namespace FirstApplication
{
    class Program
    {
        public static double first = 0, SECOND_VALUE = 0;
        public static char Ope;
        public static DateTime CURRENT_TIME = DateTime.Now;
        public static ConsoleColor BackgroundColor { get; set; }

        
        static void Main(String[]args)
        {
            bool exitFlag = false ;
            //Introduction to Program

            Console.WriteLine("*****Welcome to basic Operation Program*****");            
            Console.WriteLine("INITIAL TIME: " + CURRENT_TIME +"\n\n");
            Console.WriteLine("CURRENT TIME: " + DateTime.Now + "\n\n");

            while (!exitFlag)
            {
            try
            {
                Console.WriteLine("Input First Number:");
                first = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input Second Number: ");
                SECOND_VALUE = Convert.ToInt64(Console.ReadLine());
            }
            catch(FormatException e)
            {
                Console.Error.WriteLine(e);
            }
            Console.WriteLine("What would you like to do? + for addition, - for subtraction, * for multiplication, / for division.");
            Ope = Console.ReadKey().KeyChar;
            switch (Ope)
            {
                case ('+'):                    
                    Console.Write("\nYour answer for addition operation({0}+{1}) is {2}.",first,SECOND_VALUE, add(first, SECOND_VALUE));
                    break;
                case ('-'):
                    Console.Write("\nYour answer for subtraction operation of ({0}-{1}) is {2}.", first, SECOND_VALUE, Math.Abs(subtract(first, SECOND_VALUE)));
                    break;
                case ('*'):
                    Console.Write("\nYour answer for multiplication operation({0}*{1}) is {2}.", first, SECOND_VALUE,multiplication(first, SECOND_VALUE));
                    break;
                case ('/'):
                    Console.Write("\nYour answer for division operation({0}/{1}) is {2}.", first, SECOND_VALUE, division(first, SECOND_VALUE));
                    break;
                default:
                    Console.Write("\nInvalid Input. Exiting the program...");
                    break;
            }

            Console.WriteLine("\nCURRENT ENDING TIME: " + DateTime.Now + "\n\n");
            Console.Error.Write("Would you like to redo the operation? (y/n)? ");
            Char response = Console.ReadKey(true).KeyChar;
            Console.Error.WriteLine(response);
            if (!Console.IsOutputRedirected)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();                    
                }
            if (Char.ToUpperInvariant(response) == 'N')
                exitFlag = true;
           }
            
            Console.ReadKey();
        }
        
        public static double add(double  x,double y)
        {
            return x + y;
        }

        public static double subtract(double x,double y)
        {
            return (x > y) ? x - y : y - x;
        }

        public static double multiplication(double x,double y)
        {
            return x * y;
        }

        public static double division(double x, double y)
        {
            return (x > y) ? x / y : y / x;
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
