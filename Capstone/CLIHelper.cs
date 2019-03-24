using System;
using System.Collections.Generic;
using System.Text;
using VendingLibrary;

namespace Capstone
{
    public class CLIHelper
    {
        public static int GetSingleInteger(string message, int startRange, int endRange)
        {
            string userInput = String.Empty;
            int intValue = 0;
            int numberOfAttempts = 0;

            bool exit = false;
            do
            {
                if (numberOfAttempts > 0)
                {
                    MiscUtility.PlaySound("Sounds\\ahem.wav");
                    Console.WriteLine($"\nInvalid input format. Selection must be a number between { startRange} and { endRange}.");
                }

                Console.Write(message + " ");
                userInput = Console.ReadKey().KeyChar.ToString();
                numberOfAttempts++;

                if (int.TryParse(userInput, out intValue))
                {
                    if (intValue >= startRange && intValue <= endRange)
                    {
                        exit = true;
                    }
                }
            }
            while (!exit);

            return intValue;

        }
    }
}
