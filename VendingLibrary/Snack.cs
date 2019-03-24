using System;

namespace VendingLibrary
{
    /// <summary>
    /// Base mold of Snacks
    /// </summary>
    public abstract class Snack
    {
        /// <summary>
        /// Returns the Name of the snack item
        /// </summary>
        public string ProductName { get; protected set; }

        /// <summary>
        /// Returns the Price of the snack item
        /// </summary>
        public double Price { get; protected set; }

        /// <summary>
        /// Current inventory count
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Returns the string location of the snack item's sound file
        /// </summary>
        public abstract string PullSoundFile();

        /// <summary>
        /// Returns the Noise string of the snack item
        /// </summary>
        public abstract string MakeNoise();
    }
}
