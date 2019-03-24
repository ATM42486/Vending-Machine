using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VendingLibrary;

namespace CapstoneTests
{
    [TestClass]
    public class ChangePurseTests
    {
        [TestMethod]
        public void ChangePurseTotalTest()
        {
            ChangePurse cp1 = new ChangePurse();
            ChangePurse cp2 = new ChangePurse();
            ChangePurse cp3 = new ChangePurse();
            ChangePurse cp4 = new ChangePurse();

            cp1.Quarters = 3;
            cp2.Dimes = 3;
            cp3.Nickels = 3;
            cp4.Quarters = 3;
            cp4.Dimes = 3;
            cp4.Nickels = 3;
            
            double expectedBalance1 = 0.75;
            double expectedBalance2 = 0.3;
            double expectedBalance3 = 0.15;
            double expectedBalance4 = 1.2;

            double actualBalance1 = cp1.TotalAmount;
            double actualBalance2 = cp2.TotalAmount;
            double actualBalance3 = cp3.TotalAmount;
            double actualBalance4 = cp4.TotalAmount;

            Assert.AreEqual(expectedBalance1, actualBalance1, "Expecting balance to be $0.75");
            Assert.AreEqual(expectedBalance2, actualBalance2, "Expecting balance to be $0.30");
            Assert.AreEqual(expectedBalance3, actualBalance3, "Expecting balance to be $0.15");
            Assert.AreEqual(expectedBalance4, actualBalance4, "Expecting balance to be $1.20");
        }
    }
}
