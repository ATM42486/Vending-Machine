using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VendingLibrary;

namespace Capstone
{
    public class VendingMachineCLI
    {
        private Random rnd = new Random();
        private VendingMachine _vm = null;

        public VendingMachineCLI(VendingMachine vm)
        {
            _vm = vm;
        }

        public void OpeningSplash()
        {
            MiscUtility.PlaySound("Sounds\\LOZ_Fanfare.wav");
            using (StreamReader sr = new StreamReader("SplashArt.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }
            Console.WriteLine("\nAn Umbrella Corporation Creation\n\n\n\n\n\n");

            ResidentEvil();

            PressAnyToContinue();
            MainMenu();
        }

        private void MainMenu()
        {
            bool exitVM = false;
            while (!exitVM)
            {
                Console.Clear();
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) Exit");
                Console.WriteLine();

                ResidentEvil();

                int selection = CLIHelper.GetSingleInteger("Select an option...", 1, 4);

                if (selection == 1)
                {
                    DisplayItems();
                    PressAnyToContinue();
                }
                else if (selection == 2)
                {
                    PurchaseMenu();
                }
                else if (selection == 3)
                {
                    exitVM = true;
                }
                else if (selection == 4)
                {
                    PrintSalesReport();
                }
            }

            Console.Clear();
            Console.WriteLine("Thank for your patronage!\nHave a nice day!");
            Console.ReadKey();
        }

        private void PressAnyToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void DisplayItems()
        {
            Console.Clear();
            Console.WriteLine("Slot ".PadRight(8, '-') + " Product ".PadRight(22, '-') + " Price ".PadRight(10, '-') + " Quantity");
            Console.WriteLine("-----".PadRight(8, '-') + "---------".PadRight(22, '-') + "-------".PadRight(10, '-') + "---------");

            foreach (string[] item in _vm.InventoryList())
            {
                string menuButton = item[0] + " ";
                string productName = " " + item[1] + " ";
                string price = " " + item[2] + " ";
                string quantity = " " + item[3];

                Console.WriteLine(menuButton.PadRight(8, '-') + productName.PadRight(22, '-') + price + quantity.PadLeft(12, '-'));
            }

            Console.WriteLine("-----".PadRight(8, '-') + "---------".PadRight(22, '-') + "-------".PadRight(10, '-') + "---------");
        }

        private void PurchaseMenu()
        {
            bool exitPurchase = false;
            while (!exitPurchase)
            {
                Console.Clear();
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");
                Console.WriteLine($"Current Money Provided: {_vm.UserBalance.ToString("C")}");
                Console.WriteLine();

                ResidentEvil();

                int selection = CLIHelper.GetSingleInteger("Select an option...", 1, 3);

                if (selection == 1)
                {
                    Console.Clear();
                    FeedMoneyMenu();
                }
                else if (selection == 2)
                {
                    ProductSelectionMenu();
                }
                else if (selection == 3)
                {
                    ProduceChange();
                    exitPurchase = true;
                }
            }
        }

        private void FeedMoneyMenu()
        {
            bool exitFeed = false;
            const double one = 1D;
            const double two = 2D;
            const double five = 5D;
            const double ten = 10D;

            Console.WriteLine("(1) Add $1");
            Console.WriteLine("(2) Add $2");
            Console.WriteLine("(3) Add $5");
            Console.WriteLine("(4) Add $10");
            Console.WriteLine("(5) Exit");
            Console.WriteLine($"\nCurrent Money Provided: {_vm.UserBalance.ToString("C")}");

            ResidentEvil();

            while (!exitFeed)
            {
                Console.WriteLine();

                int selection = CLIHelper.GetSingleInteger("Select an option...", 1, 5);

                if (selection == 1)/*Add $1 to balance*/
                {
                    FeedMoney(one);
                    ResidentEvil();
                }
                else if (selection == 2)/*Add $2 to balance*/
                {
                    FeedMoney(two);
                    ResidentEvil();
                }
                else if (selection == 3)/*Add $5 to balance*/
                {
                    FeedMoney(five);
                    ResidentEvil();
                }
                else if (selection == 4)/*Add $10 to balance*/
                {
                    FeedMoney(ten);
                    ResidentEvil();
                }
                else if (selection == 5)
                {
                    exitFeed = true;
                }
            }
        }

        private void FeedMoney(double amount)
        {
            _vm.AddToBalance(amount);
            MiscUtility.PlaySound("Sounds\\coin.wav");
            Logging.LogFeed(amount, _vm);
            Console.WriteLine($"\n{amount.ToString("C")} was fed into the machine.");
            Console.WriteLine($"Current Money Provided is now {_vm.UserBalance.ToString("C")}");
        }

        private void ProductSelectionMenu()
        {
            bool exitSelection = false;
            while (!exitSelection)
            {
                DisplayItems();
                Console.WriteLine();
                Console.WriteLine($"Current Money Provided: {_vm.UserBalance.ToString("C")}");
                Console.WriteLine();
                Console.Write("Enter Your Selection or (Q)uit: ");

                ResidentEvil();

                string userSelection = Console.ReadLine().ToUpper();

                if (_vm.ItemButtonExists(userSelection))
                {
                    //purchase item if available
                    if (_vm.ItemAvailable(userSelection) && _vm.CanPurchase(userSelection))
                    {
                        PurchaseItem(userSelection);
                    }
                    else if (!_vm.ItemAvailable(userSelection))
                    {
                        Console.WriteLine("We're sorry, but the selected item is currently unavailable");
                        PressAnyToContinue();
                    }
                    else
                    {
                        Console.WriteLine($"Selected item costs more than the availabe balance of {_vm.UserBalance.ToString("C")}.");
                        Console.WriteLine("Please insert more money.");
                        PressAnyToContinue();
                    }
                }
                else if (userSelection.Equals("Q"))
                {
                    //leave item selection
                    exitSelection = true;
                }
                else
                {
                    Console.WriteLine("Selection not recognized, please try again.");
                    PressAnyToContinue();
                }
            }
        }

        private void PurchaseItem(string menuSelection)
        {
            _vm.DispenseItem(menuSelection);
            MiscUtility.PlaySound(_vm.FetchItemSoundFile(menuSelection));
            Console.WriteLine(_vm.FetchItemNoiseMessage(menuSelection));
            Logging.LogPurchase(menuSelection, _vm);
            Reporting.ReportSale(menuSelection, _vm);
            PressAnyToContinue();
        }

        private void ProduceChange()
        {
            Logging.LogChange(_vm);
            ChangePurse change = _vm.MakeChange();
            Console.WriteLine($"\n{change.TotalAmount.ToString("C")} in change returned consisting of:");
            if (change.Quarters > 0)
            {
                Console.WriteLine($"{change.Quarters} quarters");
            }
            if (change.Dimes > 0)
            {
                Console.WriteLine($"{change.Dimes} dimes");
            }
            if (change.Nickels > 0)
            {
                Console.WriteLine($"{change.Nickels} nickels");
            }
            PressAnyToContinue();
        }

        private void PrintSalesReport()
        {
            Console.WriteLine("\nPlease enter admin code");
            string input = Console.ReadLine();
            Console.Clear();

            if (input.Equals("admin code"))
            {
                try
                {
                    using (StreamReader sr = new StreamReader("SalesReport.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            Console.WriteLine(sr.ReadLine());
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("No sales have been made. Bummer.");
                }
            }
            else
            {
                Console.WriteLine("Admin credentials not recognized.");
            }

            ResidentEvil();

            PressAnyToContinue();
        }

        private void ResidentEvil()
        {
            int number = rnd.Next(1, 101);

            if (number == 100)
            {
                Console.WriteLine("\nYou feel an evil presence...\n");
            }
            else if (number == 99)
            {
                Console.WriteLine("\nYou hear something evil stirring...\n");
            }
            else if (number == 98)
            {
                Console.WriteLine("\nWait a minute... Didn't Umbrella Corp. unleash zombies?\n");
            }
        }
    }
}
