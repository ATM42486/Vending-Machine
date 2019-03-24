using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VendingLibrary;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void VMAddBalance()
        {
            VendingMachine vm1 = new VendingMachine();
            VendingMachine vm2 = new VendingMachine();
            VendingMachine vm3 = new VendingMachine();
            VendingMachine vm4 = new VendingMachine();

            double expectedBalance1 = 1D;
            double expectedBalance2 = 2D;
            double expectedBalance3 = 5D;
            double expectedBalance4 = 10D;

            vm1.AddToBalance(1);
            vm2.AddToBalance(2);
            vm3.AddToBalance(5);
            vm4.AddToBalance(10);

            double actualBalance1 = vm1.UserBalance;
            double actualBalance2 = vm2.UserBalance;
            double actualBalance3 = vm3.UserBalance;
            double actualBalance4 = vm4.UserBalance;

            Assert.AreEqual(expectedBalance1, actualBalance1, "Expecting balance to be increased by $1");
            Assert.AreEqual(expectedBalance2, actualBalance2, "Expecting balance to be increased by $2");
            Assert.AreEqual(expectedBalance3, actualBalance3, "Expecting balance to be increased by $5");
            Assert.AreEqual(expectedBalance4, actualBalance4, "Expecting balance to be increased by $10");
        }

        [TestMethod]
        public void VMDeductBalance()
        {
            VendingMachine vm1 = new VendingMachine();
            VendingMachine vm2 = new VendingMachine();
            VendingMachine vm3 = new VendingMachine();
            VendingMachine vm4 = new VendingMachine();

            vm1.AddToBalance(10D);
            vm2.AddToBalance(10D);
            vm3.AddToBalance(10D);
            vm4.AddToBalance(10D);

            vm1.DispenseItem("A1");
            vm2.DispenseItem("B4");
            vm3.DispenseItem("C3");
            vm4.DispenseItem("D2");

            double expectedBalance1 = 6.95;
            double expectedBalance2 = 8.25;
            double expectedBalance3 = 8.50;
            double expectedBalance4 = 9.05;


            double actualBalance1 = vm1.UserBalance;
            double actualBalance2 = vm2.UserBalance;
            double actualBalance3 = vm3.UserBalance;
            double actualBalance4 = vm4.UserBalance;

            Assert.AreEqual(expectedBalance1, actualBalance1, "Expecting balance to be $6.95");
            Assert.AreEqual(expectedBalance2, actualBalance2, "Expecting balance to be $8.25");
            Assert.AreEqual(expectedBalance3, actualBalance3, "Expecting balance to be $8.50");
            Assert.AreEqual(expectedBalance4, actualBalance4, "Expecting balance to be $9.05");
        }

        [TestMethod]
        public void VMDeductInventory()
        {
            VendingMachine vm1 = new VendingMachine();
            VendingMachine vm2 = new VendingMachine();
            VendingMachine vm3 = new VendingMachine();
            VendingMachine vm4 = new VendingMachine();

            vm1.DispenseItem("A1");
            vm2.DispenseItem("B4");
            vm3.DispenseItem("C3");
            vm4.DispenseItem("D2");

            int expectedInventory1 = 4;
            int expectedInventory2 = 4;
            int expectedInventory3 = 4;
            int expectedInventory4 = 4;


            double actualInventory1 = vm1.ButtonQuantity("A1");
            double actualInventory2 = vm2.ButtonQuantity("B4");
            double actualInventory3 = vm3.ButtonQuantity("C3");
            double actualInventory4 = vm4.ButtonQuantity("D2");

            Assert.AreEqual(expectedInventory1, actualInventory1, "Expecting an inventory of 4");
            Assert.AreEqual(expectedInventory2, actualInventory2, "Expecting an inventory of 4");
            Assert.AreEqual(expectedInventory3, actualInventory3, "Expecting an inventory of 4");
            Assert.AreEqual(expectedInventory4, actualInventory4, "Expecting an inventory of 4");
        }

        [TestMethod]
        public void VMItemButtonExists()
        {
            VendingMachine vm1 = new VendingMachine();
            VendingMachine vm2 = new VendingMachine();
            VendingMachine vm3 = new VendingMachine();
            VendingMachine vm4 = new VendingMachine();

            bool expectedButton1 = true;
            bool expectedButton2 = true;
            bool expectedButton3 = false;
            bool expectedButton4 = false;


            bool actualButton1 = vm1.ItemButtonExists("A1");
            bool actualButton2 = vm2.ItemButtonExists("B3");
            bool actualButton3 = vm3.ItemButtonExists("L12");
            bool actualButton4 = vm4.ItemButtonExists("BOB");

            Assert.AreEqual(expectedButton1, actualButton1, "Expecting true");
            Assert.AreEqual(expectedButton2, actualButton2, "Expecting true");
            Assert.AreEqual(expectedButton3, actualButton3, "Expecting false");
            Assert.AreEqual(expectedButton4, actualButton4, "Expecting false");
        }

        [TestMethod]
        public void VMItemAvailable()
        {
            VendingMachine vm = new VendingMachine();

            vm.DispenseItem("A1");

            vm.DispenseItem("B2");
            vm.DispenseItem("B2");

            vm.DispenseItem("C3");
            vm.DispenseItem("C3");
            vm.DispenseItem("C3");
            vm.DispenseItem("C3");
            vm.DispenseItem("C3");

            vm.DispenseItem("D4");
            vm.DispenseItem("D4");
            vm.DispenseItem("D4");
            vm.DispenseItem("D4");
            vm.DispenseItem("D4");

            bool expectedAvailable1 = true;
            bool expectedAvailable2 = true;
            bool expectedAvailable3 = false;
            bool expectedAvailable4 = false;


            bool actualAvailable1 = vm.ItemAvailable("A1");
            bool actualAvailable2 = vm.ItemAvailable("B2");
            bool actualAvailable3 = vm.ItemAvailable("C3");
            bool actualAvailable4 = vm.ItemAvailable("D4");

            Assert.AreEqual(expectedAvailable1, actualAvailable1, "Expecting true");
            Assert.AreEqual(expectedAvailable2, actualAvailable2, "Expecting true");
            Assert.AreEqual(expectedAvailable3, actualAvailable3, "Expecting false");
            Assert.AreEqual(expectedAvailable4, actualAvailable4, "Expecting false");
        }

        [TestMethod]
        public void VMCanPurchase()
        {
            VendingMachine vm = new VendingMachine();

            vm.AddToBalance(2D);

            bool expectedCanPurchase1 = true;
            bool expectedCanPurchase2 = true;
            bool expectedCanPurchase3 = false;
            bool expectedCanPurchase4 = false;


            bool actualCanPurchase1 = vm.CanPurchase("D4");
            bool actualCanPurchase2 = vm.CanPurchase("B2");
            bool actualCanPurchase3 = vm.CanPurchase("A1");
            bool actualCanPurchase4 = vm.CanPurchase("A3");

            Assert.AreEqual(expectedCanPurchase1, actualCanPurchase1, "Expecting true");
            Assert.AreEqual(expectedCanPurchase2, actualCanPurchase2, "Expecting true");
            Assert.AreEqual(expectedCanPurchase3, actualCanPurchase3, "Expecting false");
            Assert.AreEqual(expectedCanPurchase4, actualCanPurchase4, "Expecting false");
        }

        [TestMethod]
        public void VMFetchItemSoundFile()
        {
            VendingMachine vm = new VendingMachine();
            
            string expectedFile1 = @"Sounds\chomp.wav";
            string expectedFile2 = @"Sounds\paper_tearing.wav";
            string expectedFile3 = @"Sounds\can_pop.wav";
            string expectedFile4 = @"Sounds\chomp.wav";


            string actualFile1 = vm.FetchItemSoundFile("A1");
            string actualFile2 = vm.FetchItemSoundFile("B2");
            string actualFile3 = vm.FetchItemSoundFile("C3");
            string actualFile4 = vm.FetchItemSoundFile("D4");

            Assert.AreEqual(expectedFile1, actualFile1, "Expecting \"Sounds\\chomp.wav\"");
            Assert.AreEqual(expectedFile2, actualFile2, "Expecting \"Sounds\\paper_tearing.wav\"");
            Assert.AreEqual(expectedFile3, actualFile3, "Expecting \"Sounds\\can_pop.wav\"");
            Assert.AreEqual(expectedFile4, actualFile4, "Expecting \"Sounds\\chomp.wav\"");
        }

        [TestMethod]
        public void VMFetchItemNoiseMessage()
        {
            VendingMachine vm = new VendingMachine();

            string expectedMessage1 = "Crunch Crunch, Yum!";
            string expectedMessage2 = "Munch Munch, Yum!";
            string expectedMessage3 = "Glug Glug, Yum!";
            string expectedMessage4 = "Chew Chew, Yum!";

            string actualMessage1 = vm.FetchItemNoiseMessage("A1");
            string actualMessage2 = vm.FetchItemNoiseMessage("B2");
            string actualMessage3 = vm.FetchItemNoiseMessage("C3");
            string actualMessage4 = vm.FetchItemNoiseMessage("D4");

            Assert.AreEqual(expectedMessage1, actualMessage1, "Expecting \"Crunch Crunch, Yum!\"");
            Assert.AreEqual(expectedMessage2, actualMessage2, "Expecting \"Munch Munch, Yum!\"");
            Assert.AreEqual(expectedMessage3, actualMessage3, "Expecting \"Glug Glug, Yum!\"");
            Assert.AreEqual(expectedMessage4, actualMessage4, "Expecting \"Chew Chew, Yum!\"");
        }

        [TestMethod]
        public void VMFetchItemName()
        {
            VendingMachine vm = new VendingMachine();

            string expectedName1 = "Potato Crisps";
            string expectedName2 = "Cowtales";
            string expectedName3 = "Mountain Melter";
            string expectedName4 = "Triplemint";

            string actualName1 = vm.FetchItemName("A1");
            string actualName2 = vm.FetchItemName("B2");
            string actualName3 = vm.FetchItemName("C3");
            string actualName4 = vm.FetchItemName("D4");

            Assert.AreEqual(expectedName1, actualName1, "Expecting \"Potato Crisps\"");
            Assert.AreEqual(expectedName2, actualName2, "Expecting \"Cowtales\"");
            Assert.AreEqual(expectedName3, actualName3, "Expecting \"Mountain Melter\"");
            Assert.AreEqual(expectedName4, actualName4, "Expecting \"Triplemint\"");
        }

        [TestMethod]
        public void VMFetchItemPrice()
        {
            VendingMachine vm = new VendingMachine();

            double expectedPrice1 = 3.05;
            double expectedPrice2 = 1.50;
            double expectedPrice3 = 1.50;
            double expectedPrice4 = 0.75;

            double actualPrice1 = vm.FetchItemPrice("A1");
            double actualPrice2 = vm.FetchItemPrice("B2");
            double actualPrice3 = vm.FetchItemPrice("C3");
            double actualPrice4 = vm.FetchItemPrice("D4");

            Assert.AreEqual(expectedPrice1, actualPrice1, "Expecting $3.05");
            Assert.AreEqual(expectedPrice2, actualPrice2, "Expecting $1.50");
            Assert.AreEqual(expectedPrice3, actualPrice3, "Expecting $1.50");
            Assert.AreEqual(expectedPrice4, actualPrice4, "Expecting $0.75");
        }

        [TestMethod]
        public void VMMakeChange()
        {
            VendingMachine vm1 = new VendingMachine();
            VendingMachine vm2 = new VendingMachine();
            VendingMachine vm3 = new VendingMachine();
            VendingMachine vm4 = new VendingMachine();

            vm1.AddToBalance(0.25);
            vm2.AddToBalance(0.10);
            vm3.AddToBalance(0.05);
            vm4.AddToBalance(0.40);

            ChangePurse expectedCP1 = new ChangePurse();
            expectedCP1.Quarters = 1;

            ChangePurse expectedCP2 = new ChangePurse();
            expectedCP2.Dimes = 1;

            ChangePurse expectedCP3= new ChangePurse();
            expectedCP3.Nickels = 1;

            ChangePurse expectedCP4 = new ChangePurse();
            expectedCP4.Quarters = 1;
            expectedCP4.Dimes = 1;
            expectedCP4.Nickels = 1;

            ChangePurse actualPrice1 = vm1.MakeChange();
            ChangePurse actualPrice2 = vm2.MakeChange();
            ChangePurse actualPrice3 = vm3.MakeChange();
            ChangePurse actualPrice4 = vm4.MakeChange();

            Assert.AreEqual(expectedCP1, actualPrice1, "Expecting $3.05");
            Assert.AreEqual(expectedCP2, actualPrice2, "Expecting $1.50");
            Assert.AreEqual(expectedCP3, actualPrice3, "Expecting $1.50");
            Assert.AreEqual(expectedCP4, actualPrice4, "Expecting $0.75");
        }
    }
}
