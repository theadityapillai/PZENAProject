using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZENAProcessingProject
{
    public class ExtractFile
    {
        public ExtractFile() { }
        public void Extract(string zipFilePath, string destinationPath)
        {
            try
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, destinationPath, true);
                Console.WriteLine($"File {zipFilePath} has been extracted to {destinationPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
