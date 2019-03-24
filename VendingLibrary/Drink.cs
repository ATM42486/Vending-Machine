using System;
using System.Collections.Generic;
using System.Text;

namespace VendingLibrary
{
    /// <summary>
    /// Drink snack type
    /// </summary>
    public class Drink : Snack
    {
        private const string Noise = "Glug Glug, Yum!";
        private const string SoundFile = @"Sounds\can_pop.wav";

        public Drink(string productName, double price)
        {
            ProductName = productName;
            Price = price;
            Quantity = 5;
        }

        public override string MakeNoise()
        {
            return Noise;
        }

        public override string PullSoundFile()
        {
            return SoundFile;
        }
    }
}
