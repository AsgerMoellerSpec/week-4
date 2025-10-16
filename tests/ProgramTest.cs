namespace tests;

public class ProgramTest
{
    [Fact]
    public void TestHandlesIndexOutOfRangeException()

    {
        MockFileHandler excelFileHandler = new(true);
        MockDownloadManager downloadManager = new();
        MockFileHandler logFileManager = new();

        Week_4_PDF_downloader.Program.HandleReports(excelFileHandler, downloadManager, logFileManager);
    }

}