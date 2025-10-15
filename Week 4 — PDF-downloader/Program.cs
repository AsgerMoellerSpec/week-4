using System.Data;

namespace Week_4_PDF_downloader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            FileHandler excelFileHandler = new FileHandler("../../../../Excel files");
            DownloadManager downloadManager = new DownloadManager();
            FileHandler logFileHandler = new FileHandler("../../../../Logs");
            await HandleReports(excelFileHandler, downloadManager, logFileHandler);
        }

        static async Task HandleReports(IFileHandler excelFileHandler, IDownloadManager downloadManager, IFileHandler logFileHandler) {
            Console.WriteLine("PDF Download Application");
            Console.WriteLine("Status: Reading Excel file");

            //  Read excel file
            try {
                excelFileHandler.readTableFromExcelFileWithHeaders(0);
            } catch (IndexOutOfRangeException exception) {
                Console.WriteLine("Status: No Excel file found in folder " + Path.GetFullPath(excelFileHandler.getFolderLocation()));
            }


            Console.WriteLine("Status: Excel file finished reading");
            Console.WriteLine("Status: Attempting download");

            //  Download files
            int i = 0;
            foreach (DataRow tableRow in excelFileHandler.getTable().Rows) {
                Console.WriteLine("Download status:");
                Console.WriteLine("".PadRight(8) + tableRow[0]);

                HttpResponseMessage httpResponseMessage = await downloadManager.tryDownloadAsync(tableRow, 0, 37, 38);

                Console.WriteLine("".PadRight(8) + (httpResponseMessage != null ? httpResponseMessage.StatusCode : "NULL"));

                if (i >= 8) {   //  Limit to TEN (10) during on-site test
                    break;
                }
                i++;
            }


            Console.WriteLine("Status: Writing log");

            logFileHandler.writeToCsvFileWithHeaders(downloadManager.getStatusList(), new List<int> { 0, 1 }, ';');

            Console.WriteLine("Status: Log written");
        }
    }
}
