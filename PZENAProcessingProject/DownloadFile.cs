using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PZENAProcessingProject
{
    public class DownloadFile
    {
        private readonly HttpClient _httpclient;
        private int timeOut = 25; //could be set in a config file
        public DownloadFile()
        {
            _httpclient = new HttpClient();
            _httpclient.Timeout = TimeSpan.FromMinutes(timeOut);
        }

        public async Task Download(string baseUrl, string folder, string fileName, string destinationPath)
        {
            PathCheck(destinationPath);

            try
            {
                using (var response = await _httpclient.GetAsync(LinkCreator(baseUrl, folder, fileName)))
                {
                    response.EnsureSuccessStatusCode();
                    using (var fileStream = new FileStream(destinationPath + fileName + ".zip", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fileStream);

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"File {fileName} has been downloaded to {destinationPath}");
            }
        }


        private string LinkCreator(string baseUrl, string folder, string fileName)
        {
            return (baseUrl + "/" + folder + "/" + fileName + ".zip");
        }
        private void PathCheck(string path)
        {

            if (!Directory.Exists(path)) // Check if the directory exists
            {

                Directory.CreateDirectory(path);  // Create the directory if it doesn't exist
                Console.WriteLine($"Directory created at: {path}");
            }
            else
            {
                Console.WriteLine($"Directory already exists at: {path}");
            }
        }

    }


}
