using System;
using System.Collections.Generic;
using System.Linq;

namespace ATM
{
    public class Program
    {
        public static void Main()
        {
            ConsoleKeyInfo readKey;
            do
            {
                Console.Title = "ATM Simulator";
                Console.WriteLine("Please enter face values for money.");
                var input = Console.ReadLine();
                var values = input.Split(' ');

                var faceValues = new List<int>();
                foreach (var valueStr in values.Where(value => !string.IsNullOrEmpty(value)))
                {
                    int value;
                    if (int.TryParse(valueStr, out value))
                    {
                        faceValues.Add(Convert.ToInt32(valueStr));
                    }
                    else
                    {
                        Console.WriteLine("No valid");
                        Main();
                    }
                }
                faceValues.Sort();

                Console.WriteLine("If your wish get money press enter");
                Console.ReadKey();

                Console.WriteLine("Insert your cash: ");

                int cash;
                while (!int.TryParse(Console.ReadLine(), out cash))
                {
                    Console.WriteLine("Please enter a valid number for cash.");
                    Console.WriteLine("Insert your cash: ");
                }

                var ai = new AI(new Settings(cash, faceValues));
                var countRequiredFaceValues = ai.DefineMaxCountFaceValues();
                Console.WriteLine("Max count of Face Values: " + countRequiredFaceValues);

                Console.WriteLine("Best use face value");
                foreach (var faceValue in ai.FindSequenceFaceValues(countRequiredFaceValues))
                {
                    Console.WriteLine(faceValue.Value + " x " +
                                      faceValue.Amount);
                }

                bool check;
                do
                {
                    Console.Write("\nYou wish to get money again? (yes/no) ");
                    readKey = Console.ReadKey(true);
                    check = !((readKey.Key == ConsoleKey.Y) || (readKey.Key == ConsoleKey.N));
                } while (check);
                switch (readKey.Key)
                {
                    case ConsoleKey.Y: Console.WriteLine("Yes"); break;
                    case ConsoleKey.N: Console.WriteLine("No"); break;
                }
            } while (readKey.Key != ConsoleKey.N); 
        }
    }
}
