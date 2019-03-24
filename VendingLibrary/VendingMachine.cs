using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VendingLibrary
{
    public class VendingMachine
    {
        private const double _valueQuarter = 0.25;
        private const double _valueDime = 0.10;
        private const double _valueNickel = 0.05;
        private Dictionary<string, Snack> _inventory = new Dictionary<string, Snack>();

        /// <summary>
        /// Buyer's current balance fed into the machine
        /// </summary>
        public double UserBalance { get; private set; } = 0D;

        public VendingMachine()
        {
            ResetInventory();
        }

        /// <summary>
        /// Returns a string array list of the Vending Machine's current inventory
        /// </summary>
        /// <returns></returns>
        public List<string[]> InventoryList()
        {
            List<string[]> inventory = new List<string[]>();

            foreach (KeyValuePair<string, Snack> pair in _inventory)
            {
                string menuButton = pair.Key;
                string productName = pair.Value.ProductName;
                string price = pair.Value.Price.ToString("C");
                string quantity;

                if (pair.Value.Quantity == 0)
                {
                    quantity = "SOLD OUT";
                }
                else
                {
                    quantity = pair.Value.Quantity.ToString();
                }

                inventory.Add(new string[] { menuButton, productName, price, quantity });
            }

            return inventory;
        }

        /// <summary>
        /// Resets the Vending Machine's Inventory by filling the machine with Quantity 5 of each item
        /// </summary>
        private void ResetInventory()
        {
            _inventory.Clear();
            try
            {
                using (StreamReader sr = new StreamReader("VendingMachine.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] item = sr.ReadLine().Split('|');

                        if (double.Parse(item[2]) > 0D)
                        {
                            if (item[3].Equals("Chip"))
                            {
                                _inventory.Add(item[0].ToUpper(), new Chip(item[1], double.Parse(item[2])));
                            }
                            else if (item[3].Equals("Candy"))
                            {
                                _inventory.Add(item[0].ToUpper(), new Candy(item[1], double.Parse(item[2])));
                            }
                            else if (item[3].Equals("Drink"))
                            {
                                _inventory.Add(item[0].ToUpper(), new Drink(item[1], double.Parse(item[2])));
                            }
                            else if (item[3].Equals("Gum"))
                            {
                                _inventory.Add(item[0].ToUpper(), new Gum(item[1], double.Parse(item[2])));
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new OutOfOrderException("We're sorry, but the Vendo-Matic 600 is currently out of order.\nPlease call maintenance or try again later.");
            }
        }

        /// <summary>
        /// Adds the input amount to the user's available balance
        /// </summary>
        /// <param name="amount"></param>
        public void AddToBalance(double amount)
        {
            UserBalance += amount;
        }

        /// <summary>
        /// Dispenses the selected item and deducts that item's price from user's balance
        /// </summary>
        /// <param name="itemButton"></param>
        public void DispenseItem(string itemButton)
        {
            DeductBalance(itemButton);
            DeductInventory(itemButton);
        }

        /// <summary>
        /// Return's the item's Quantity (used in unit test)
        /// </summary>
        /// <returns></returns>
        public int ButtonQuantity(string button)
        {
            return _inventory[button].Quantity;
        }

        private void DeductBalance(string itemButton)
        {
            UserBalance -= _inventory[itemButton].Price;
        }

        private void DeductInventory(string itemButton)
        {
            _inventory[itemButton].Quantity -= 1;
        }

        /// <summary>
        /// Returns true if the menu button is contained in the vending machine's inventory list
        /// </summary>
        /// <param name="itemButton"></param>
        /// <returns></returns>
        public bool ItemButtonExists(string itemButton)
        {
            return _inventory.ContainsKey(itemButton);
        }

        /// <summary>
        /// Returns true if the selected item's Quantity is above 0
        /// </summary>
        /// <param name="itemButton"></param>
        /// <returns></returns>
        public bool ItemAvailable(string itemButton)
        {
            return _inventory[itemButton].Quantity > 0;
        }

        /// <summary>
        /// Returns true if the User has enough money to purchase item
        /// </summary>
        /// <param name="itemButton"></param>
        /// <returns></returns>
        public bool CanPurchase(string itemButton)
        {
            return _inventory[itemButton].Price <= UserBalance;
        }

        /// <summary>
        /// Returns the Item's sound file name as a string
        /// </summary>
        /// <param name="itemButton"></param>
        /// <returns></returns>
        public string FetchItemSoundFile(string itemButton)
        {
            return _inventory[itemButton].PullSoundFile();
        }

        /// <summary>
        /// Returns the Item's written sound message as a string
        /// </summary>
        /// <param name="itemButton"></param>
        /// <returns></returns>
        public string FetchItemNoiseMessage(string itemButton)
        {
            return _inventory[itemButton].MakeNoise();
        }

        /// <summary>
        /// Returns the Item's Product Name
        /// </summary>
        /// <param name="itemButton"></param>
        /// <returns></returns>
        public string FetchItemName(string itemButton)
        {
            return _inventory[itemButton].ProductName;
        }

        /// <summary>
        /// Returns the Item's price
        /// </summary>
        /// <param name="itemButton"></param>
        /// <returns></returns>
        public double FetchItemPrice(string itemButton)
        {
            return _inventory[itemButton].Price;
        }

        /// <summary>
        /// Returns a list of available Item Buttons
        /// </summary>
        /// <returns></returns>
        public List<string> ItemButtons()
        {
            List<string> buttons = new List<string>();

            foreach (KeyValuePair<string, Snack> item in _inventory)
            {
                buttons.Add(item.Key);
            }

            return buttons;
        }

        /// <summary>
        /// Returns a bundle of change and sets balance to 0
        /// </summary>
        /// <returns></returns>
        public ChangePurse MakeChange()
        {
            ChangePurse cp = new ChangePurse();

            cp.Quarters = (int)(UserBalance / _valueQuarter);
            UserBalance -= Math.Round(cp.Quarters * _valueQuarter, 2);

            cp.Dimes = (int)(UserBalance / _valueDime);
            UserBalance -= Math.Round(cp.Dimes * _valueDime, 2);

            cp.Nickels = (int)(UserBalance / _valueNickel);
            UserBalance -= Math.Round(cp.Nickels * _valueNickel, 2);

            return cp;
        }
    }
}
