using System.Data;

public class MockDownloadManager : Week_4_PDF_downloader.IDownloadManager
{
    private int numberOfDownloads = 0;
    private DataRow latestRow;
    private DataTable statusList;
    public MockDownloadManager(DataTable statusList = null)
    {
        this.statusList = statusList;
        return;
    }

    public DataTable getStatusList()
    {
        return statusList;
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