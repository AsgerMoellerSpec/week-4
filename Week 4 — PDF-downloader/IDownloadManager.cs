using System.Data;

interface IDownloadManager
{
    DataTable getStatusList();
    Task<HttpResponseMessage> tryDownloadAsync(DataRow tableRow, int fileNameIndex, int urlIndex, int fallbackUrlIndex);
}