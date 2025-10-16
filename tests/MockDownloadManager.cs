using System.Data;

public class MockDownloadManager : Week_4_PDF_downloader.IDownloadManager
{
    public MockDownloadManager()
    {
        return;
    }

    public DataTable getStatusList()
    {
        return null;
    }
    
    public Task<HttpResponseMessage> tryDownloadAsync(DataRow tableRow, int fileNameIndex, int urlIndex, int fallbackUrlIndex)
    {
        return null;
    }
}