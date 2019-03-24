using System;
using System.Collections.Generic;
using System.Text;

namespace VendingLibrary
{
    /// <summary>
    /// Chip snack type
    /// </summary>
    public class Chip : Snack
    {
        private const string Noise = "Crunch Crunch, Yum!";
        private const string SoundFile = @"Sounds\chomp.wav";

        public Chip(string productName, double price)
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
