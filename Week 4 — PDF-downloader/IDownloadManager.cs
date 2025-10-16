using System.Data;

namespace Week_4_PDF_downloader
{
    public interface IDownloadManager
    {
        DataTable getStatusList();
        Task<HttpResponseMessage> tryDownloadAsync(DataRow tableRow, int fileNameIndex, int urlIndex, int fallbackUrlIndex);
    }
}