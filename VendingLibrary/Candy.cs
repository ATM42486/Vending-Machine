using System;
using System.Collections.Generic;
using System.Text;

namespace VendingLibrary
{
    /// <summary>
    /// Candy snack type
    /// </summary>
    public class Candy : Snack
    {
        private const string Noise = "Munch Munch, Yum!";
        private const string SoundFile = @"Sounds\paper_tearing.wav";

        public Candy(string productName, double price)
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
