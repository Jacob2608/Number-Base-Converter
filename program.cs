using System;

namespace Base_n_to_Base_m
{
    class Program
    {
        static string[] numerals = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        /*
          assigning decimal values to each numeral (0,1...9,A,B...Z)
          can handle up to base-36 (numerals.Length)
          numerals[10] = A (the value is the index)
        */
        static int ValidateB() //validates the inputs of the bases
        {
            int number;
            bool success;
            while (true)
            {
                string input = Console.ReadLine();
                success = int.TryParse(input, out number);

                if (success && number >= 1 && number <= numerals.Length) return number;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nINVALID INPUT:");
                    Console.ResetColor();
                    Console.WriteLine("You may only input an integer from 1-" + numerals.Length);
                }
            }
        }
        static string ValidateN(int n) //validates the input of the number converting from
        {
            while (true)
            {
                string number = Console.ReadLine().ToUpper();
                int counter = 0; //count how many valid characters
                for (int i = 0; i < number.Length; i++) //loop through the number
                {
                    for (int j = 0; j <= n - 1; j++) //loop through numerals up to the last digit in the base (n - 1)
                    {
                        if (number[i].ToString() == numerals[j]) counter++;
                    }
                }

                if (counter == number.Length) return number; //all characters valid
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nINVALID INPUT:");
                    Console.ResetColor();
                }
            }
        }
        static string Reverse(string numberB) //reverse an array
        {
            string reversed = "";
            for (int i = numberB.Length - 1; i >= 0; i--)
            {
                reversed += numberB[i];
            }
            return reversed;
        }
        static int Value(string numeral) //get a numeral's value (defined above)
        {
            int i = 0;
            while (numerals[i] != numeral) i++;
            return i;
        }
        static void Main(string[] args)
        {
            //input base converting from
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Base: ");
            Console.ResetColor();
            int n = ValidateB();

            //input base converting to
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nto Base: ");
            Console.ResetColor();
            int m = ValidateB();

            //input number converting 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nNumber (base-" + n + "): ");
            Console.ResetColor();
            string num = ValidateN(n);

            //to base-10
            int numD = 0;
            if (n == 1) numD = num.Length;
            else
            {
                num = Reverse(num); //the computer starts indexing at the left, numbers starts indexing on the right- this is so the lower index is on the left for both.
                for (int i = 0; i < num.Length; i++) numD += Convert.ToInt32(Value(Convert.ToString(num[i]))) * Convert.ToInt32(Math.Pow(n, i));
            }

            //making number in base 1
            string numM = "";
            if (m == 1) for (int i = 0; i < numD; i++) numM += "0";
            //making number in base m (m != 1)
            else
            {
                //calculating number of digits needed
                int digits = 1;
                while (Convert.ToInt32(Math.Pow(m, digits)) - 1 < numD) digits++;

                //assembling number
                int numCheck = 0;
                for (int i = digits; i >= 1; i--)
                {
                    for (int j = m - 1; j >= 0; j--)
                    {
                        if (numCheck + j * Convert.ToInt32(Math.Pow(m, i - 1)) <= numD)
                        {
                            numM += numerals[j];
                            numCheck += j * Convert.ToInt32(Math.Pow(m, i - 1));
                            break;
                        }
                    }
                }
            }

            //output
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nNumber (base-" + m + "): ");
            Console.ResetColor();
            Console.WriteLine(numM);
        }
    }
}