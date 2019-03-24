using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace VendingLibrary
{
    public class MiscUtility
    {
        public static void PlaySound(string fileName)
        {
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            Process.Start(@"powershell", $@"-c (New-Object Media.SoundPlayer '{path}').PlaySync();");
        }
    }
}
