using System.Data;

public class MockDownloadManager : Week_4_PDF_downloader.IDownloadManager
{
    private int numberOfDownloads = 0;
    private DataRow latestRow;
    public MockDownloadManager()
    {
        return;
    }

    public DataTable getStatusList()
    {
        return null;
    }

    public async Task<HttpResponseMessage> tryDownloadAsync(DataRow tableRow, int fileNameIndex, int urlIndex, int fallbackUrlIndex)
    {
        numberOfDownloads++;
        latestRow = tableRow;
        return null;
    }

    public int getNumberOfDownloads()
    {
        return numberOfDownloads;
    }
    
    public DataRow getLatestRow()
    {
        return latestRow;
    }
}