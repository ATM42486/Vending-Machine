using System;
using System.Collections.Generic;
using System.Text;

namespace VendingLibrary
{
    public class ChangePurse
    {
        private const double _valueQuarter = 0.25;
        private const double _valueDime = 0.10;
        private const double _valueNickel = 0.05;

        public int Quarters { get; set; } = 0;
        public int Dimes { get; set; } = 0;
        public int Nickels { get; set; } = 0;
        public double TotalAmount
        {
            get
            {
                double total = ((Quarters * _valueQuarter) + (Dimes * _valueDime) + (Nickels * _valueNickel));
                return Math.Round(total, 2);
            }
        }

        public ChangePurse() { }

        public override bool Equals(object obj)
        {
            bool areEqual = false;

            if (obj is ChangePurse)
            {
                bool quarterCountSame = ((ChangePurse)obj).Quarters == this.Quarters;
                bool dimeCountSame = ((ChangePurse)obj).Dimes == this.Dimes;
                bool nickelCountSame = ((ChangePurse)obj).Nickels == this.Nickels;

                areEqual = quarterCountSame && dimeCountSame && nickelCountSame;
            }

            return areEqual;
        }
    }
}
