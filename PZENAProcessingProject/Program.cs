// See https://aka.ms/new-console-template for more information
using PZENAProcessingProject;
using System.Reflection.Metadata;

class Program
{
    static async Task Main(string[] args)
    {

        var downloader = new DownloadFile();
        var extracter = new ExtractFile();

        string baseUrl = "https://www.alphaforge.net"; //These variables can be set in a config file
        string folderUrl = "A0B1C3"; //URL extension between CSV files were in similar base format
        string fileName = "TICKERS";
        string tableName = "TICKERS";
        string downloadDestinationPath = @"C:\FileStorage\";
        string uploadFilePath = downloadDestinationPath + fileName + "\\" + fileName + ".csv";
        string connectionString = "data source=DESKTOP-U24U6VN;initial catalog=EquityDataProcessing;trusted_connection=true";
        
        var uploader = new UploadFile(connectionString);

        await downloader.Download(baseUrl, folderUrl, fileName, downloadDestinationPath); //download Tickers file
        extracter.Extract(downloadDestinationPath + fileName + ".zip", downloadDestinationPath + fileName); //unzip and extract tickers.zip
        uploader.Upload(uploadFilePath, tableName); //upload csv file to sql DB


        fileName = "PRICES";
        tableName = "PRICES"; //download and upload Prices csv
        await downloader.Download(baseUrl, folderUrl, fileName, downloadDestinationPath);
        extracter.Extract(downloadDestinationPath + fileName + ".zip", downloadDestinationPath + fileName);
        uploader.Upload(uploadFilePath, tableName);


    }
}