using System.Data;
using System.Globalization;
using System.Reflection;

namespace tests;

public class ProgramTest
{
    [Fact]
    public void TestHandlesIndexOutOfRangeException()

    {
        MockFileHandler excelFileHandler = new(throwsIndexOutOfRangeException: true);
        MockDownloadManager downloadManager = new();
        MockFileHandler logFileManager = new();

        Week_4_PDF_downloader.Program.HandleReports(excelFileHandler, downloadManager, logFileManager);
    }

    [Fact]
    public void TestReportIsDownloaded()
    {
        int numberOfRows = 1;
        MockFileHandler excelFileHandler = new(numberOfRows: numberOfRows);
        MockDownloadManager downloadManager = new();
        MockFileHandler logFileManager = new();

        Week_4_PDF_downloader.Program.HandleReports(excelFileHandler, downloadManager, logFileManager);

        int numberOfDownloads = downloadManager.getNumberOfDownloads();
        Assert.Equal(numberOfRows, numberOfDownloads);
        DataRow mockRow = excelFileHandler.getTable().Rows[0];
        DataRow latestRow = downloadManager.getLatestRow();
        Assert.Same(mockRow, latestRow);
    }

    [Fact]
    public void TestMultipleReportsAreDownloaded()
    {
        int numberOfRows = 3;
        MockFileHandler excelFileHandler = new(numberOfRows: numberOfRows);
        MockDownloadManager downloadManager = new();
        MockFileHandler logFileManager = new();

        Week_4_PDF_downloader.Program.HandleReports(excelFileHandler, downloadManager, logFileManager);

        int numberOfDownloads = downloadManager.getNumberOfDownloads();
        Assert.Equal(numberOfRows, numberOfDownloads);
    }

    [Fact]
    public void TestMakesCorrectLogRequest()

    {
        MockFileHandler excelFileHandler = new();
        DataTable mockStatusTable = new();
        MockDownloadManager downloadManager = new(statusList: mockStatusTable);
        MockFileHandler logFileManager = new();

        Week_4_PDF_downloader.Program.HandleReports(excelFileHandler, downloadManager, logFileManager);

        List<int> expectedIndicesWritten = new List<int> { 0, 1 };
        char expectedSeparatorWritten = ';';
        DataTable latestTableWritten = logFileManager.getLatestTableWritten();
        List<int> latestIndicesWritten = logFileManager.getLatestIndicesWritten();
        char latestSeparatorWritten = logFileManager.getLatestSeparatorWritten();

        Assert.Same(mockStatusTable, latestTableWritten);
        Assert.Equal(expectedIndicesWritten, latestIndicesWritten);
        Assert.Equal(expectedSeparatorWritten, latestSeparatorWritten);
    }
    
    /*[Fact]
    public void TestHandles()

    {
        MockFileHandler excelFileHandler = new(throwsIndexOutOfRangeException: true);
        MockDownloadManager downloadManager = new();
        MockFileHandler logFileManager = new();

        Week_4_PDF_downloader.Program.HandleReports(excelFileHandler, downloadManager, logFileManager);
    }*/
}