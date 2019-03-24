using System;
using System.Collections.Generic;
using System.Text;

namespace VendingLibrary
{
    /// <summary>
    /// Gum snack type
    /// </summary>
    public class Gum : Snack
    {
        private const string Noise = "Chew Chew, Yum!";
        private const string SoundFile = @"Sounds\chomp.wav";

        public Gum(string productName, double price)
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
