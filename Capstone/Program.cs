using System;
using System.IO;
using System.Diagnostics;
using VendingLibrary;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendoMatic = null;

            try
            {
                vendoMatic = new VendingMachine();
                VendingMachineCLI vendCLI = new VendingMachineCLI(vendoMatic);
                vendCLI.OpeningSplash();
            }
            catch (OutOfOrderException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("\nPress any key to exit");
                Console.ReadKey();
            }
        }
    }
}
