using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingLibrary;

namespace CapstoneTests
{
    [TestClass]
    public class SnackTests
    {
        Chip chip = new Chip("chip", 1);
        Candy candy = new Candy("candy", 1);
        Drink drink = new Drink("drink", 1);
        Gum gum = new Gum("gum", 1);

        [TestMethod]
        public void SnackNoises()
        {
            string expectedChipNoise = "Crunch Crunch, Yum!";
            string expectedCandyNoise = "Munch Munch, Yum!";
            string expectedDrinkNoise = "Glug Glug, Yum!";
            string expectedGumNoise = "Chew Chew, Yum!";

            string actualChipNoise = chip.MakeNoise();
            string actualCandyNoise = candy.MakeNoise();
            string actualDrinkNoise = drink.MakeNoise();
            string actualGumNoise = gum.MakeNoise();

            Assert.AreEqual(expectedChipNoise, actualChipNoise, "Expecting Chip noise message 'Crunch Crunch, Yum!'");
            Assert.AreEqual(expectedCandyNoise, actualCandyNoise, "Expecting Candy noise message 'Munch Munch, Yum!'");
            Assert.AreEqual(expectedDrinkNoise, actualDrinkNoise, "Expecting Drink noise message 'Glug Glug, Yum!'");
            Assert.AreEqual(expectedGumNoise, actualGumNoise, "Expecting Gum noise message 'Chew Chew, Yum!'");
        }

        [TestMethod]
        public void SnackSoundSource()
        {
            string expectedChipSoundFile = "Sounds\\chomp.wav";
            string expectedCandySoundFile = "Sounds\\paper_tearing.wav";
            string expectedDrinkSoundFile = "Sounds\\can_pop.wav";
            string expectedGumSoundFile = "Sounds\\chomp.wav";

            string actualChipSoundFile = chip.PullSoundFile();
            string actualCandySoundFile = candy.PullSoundFile();
            string actualDrinkSoundFile = drink.PullSoundFile();
            string actualGumSoundFile = gum.PullSoundFile();

            Assert.AreEqual(expectedChipSoundFile, actualChipSoundFile, "Expecting Chip sound file \"Sounds\\chomp.wav\"");
            Assert.AreEqual(expectedCandySoundFile, actualCandySoundFile, "Expecting Candy sound file \"Sounds\\paper_tearing.wav\"");
            Assert.AreEqual(expectedDrinkSoundFile, actualDrinkSoundFile, "Expecting Drink sound file \"Sounds\\can_pop.wav\"");
            Assert.AreEqual(expectedGumSoundFile, actualGumSoundFile, "Expecting Gum sound file \"Sounds\\chomp.wav\"");
        }
    }
}
