public class MockDownloadManager : Week_4_PDF_downloader.IDownloadManager
{
    DataTable getStatusList()
    {
        return null;
    }
    
    Task<HttpResponseMessage> tryDownloadAsync(DataRow tableRow, int fileNameIndex, int urlIndex, int fallbackUrlIndex)
    {
        return;
    }
}