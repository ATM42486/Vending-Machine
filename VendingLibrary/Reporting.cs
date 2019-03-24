using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VendingLibrary
{
    public class Reporting
    {
        private const string fileName = "SalesReport.txt";

        /// <summary>
        /// Updates the Sales Report file; Creates the file if it does not exist
        /// </summary>
        /// <param name="pushedButton"></param>
        /// <param name="vm"></param>
        public static void ReportSale(string pushedButton, VendingMachine vm)
        {
            if (!File.Exists(fileName))
            {
                CreateSalesReport(pushedButton, vm);
            }
            else
            {
                UpdateSalesReport(pushedButton, vm);
            }
        }

        private static void CreateSalesReport(string pushedButton, VendingMachine vm)
        {
            List<string> buttons = vm.ItemButtons();

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach(string button in buttons)
                {
                    if (button.Equals(pushedButton))
                    {
                        sw.WriteLine(vm.FetchItemName(button) + "|" + (1).ToString());
                    }
                    else
                    {
                        sw.WriteLine(vm.FetchItemName(button) + "|" + (0).ToString());
                    }
                }

                sw.WriteLine();
                sw.WriteLine($"**TOTAL SALES** {vm.FetchItemPrice(pushedButton).ToString("C")}");
            }
        }

        private static void UpdateSalesReport(string pushedButton, VendingMachine vm)
        {
            Dictionary<string, int> salesList = new Dictionary<string, int>();
            double salesTotal = 0D;

            using (StreamReader sr = new StreamReader(fileName))
            {
                while(!sr.EndOfStream)
                {
                    string fileLine = sr.ReadLine();

                    if (fileLine.Contains("|"))
                    {
                        string[] lineArray = fileLine.Split('|');
                        salesList.Add(lineArray[0], int.Parse(lineArray[1]));
                    }
                    else if (fileLine.Contains("**TOTAL SALES**"))
                    {
                        salesTotal += double.Parse(fileLine.Replace("**TOTAL SALES** $", "").Replace(",",""));
                    }
                }
            }

            salesList[vm.FetchItemName(pushedButton)]++;

            salesTotal += vm.FetchItemPrice(pushedButton);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (KeyValuePair<string, int> item in salesList)
                {
                    sw.WriteLine(item.Key + "|" + item.Value);
                }

                sw.WriteLine();
                sw.WriteLine($"**TOTAL SALES** {salesTotal.ToString("C")}");
            }
        }
    }
}
