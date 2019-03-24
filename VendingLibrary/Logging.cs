using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VendingLibrary
{
    public class Logging
    {
        const string _dateTimeFormat = "MM/dd/yyyy hh:mm:ss tt";
        const string _feedMoney = " FEED MONEY: ";
        const string _giveChange = " GIVE CHANGE ";
        const string _zeroBalance = "$0.00";

        public static void LogFeed(double amount, VendingMachine vm)
        {
            using (StreamWriter sw = new StreamWriter("Log.txt", true))
            {
                string time = DateTime.Now.ToString(_dateTimeFormat);
                string strAmount = amount.ToString("C");
                string strBalance = vm.UserBalance.ToString("C");

                sw.WriteLine(time + (_feedMoney).PadRight(25) + strAmount.PadRight(7) + strBalance.PadLeft(7));
                sw.WriteLine();
            }
        }

        public static void LogPurchase(string menuOption, VendingMachine vm)
        {
            using (StreamWriter sw = new StreamWriter("Log.txt", true))
            {
                string time = DateTime.Now.ToString(_dateTimeFormat);
                string selectedItem = " " + vm.FetchItemName(menuOption) + " " + menuOption + " ";
                string balanceBefore =(vm.UserBalance + vm.FetchItemPrice(menuOption)).ToString("C");
                string balanceNow = vm.UserBalance.ToString("C");

                sw.WriteLine(time + selectedItem.PadRight(25) + balanceBefore.PadRight(7) + balanceNow.PadLeft(7));
                sw.WriteLine();
            }
        }

        public static void LogChange(VendingMachine vm)
        {
            using (StreamWriter sw = new StreamWriter("Log.txt", true))
            {
                string time = DateTime.Now.ToString(_dateTimeFormat);
                string strBalance = vm.UserBalance.ToString("C");

                sw.WriteLine(time + (_giveChange).PadRight(25) + strBalance.PadRight(7) + _zeroBalance.PadLeft(7));
                sw.WriteLine();
            }
        }
    }
}
