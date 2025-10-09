using System.Data;
using System.Net.Http.Headers;

namespace Week_4_PDF_downloader {
    internal class DownloadManager {
        //  Constants
        private static String DEFAULT_DOWNLOAD_LOCATION = "../../../../Downloads";

        //  Fields
        private String downloadLocation;
        private static HttpClient httpClient = new HttpClient();
        private DataTable statusTable = new DataTable();

        //  Properties
        public List<String> allowedTypes = new List<String> { "application/pdf" };

        public DownloadManager() : this(DEFAULT_DOWNLOAD_LOCATION) {
        }

        public DownloadManager(String downloadLocation) {
            this.downloadLocation = downloadLocation.Trim();

            //  Prepare table for status output
            statusTable = new DataTable();
            DataColumn dataColumn;

            dataColumn = new DataColumn();
            dataColumn.DataType = typeof(String);
            dataColumn.ColumnName = "ID";
            statusTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.DataType = typeof(Boolean);
            dataColumn.ColumnName = "Status";
            statusTable.Columns.Add(dataColumn);
        }

        public DataTable getStatusList() {
            return statusTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="fileNameIndex"></param>
        /// <param name="urlIndex"></param>
        /// <param name="fallbackUrlIndex"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> tryDownloadAsync(DataRow tableRow, int fileNameIndex, int urlIndex, int fallbackUrlIndex) {
            HttpResponseMessage httpResponseMessage = null;
            Boolean isSuccess = false;
            int contentTypeIndexInAllowedTypes = -1;

            try {   //  Try download file
                httpResponseMessage = await httpClient.GetAsync(tableRow[urlIndex].ToString());
                contentTypeIndexInAllowedTypes = allowedTypes.IndexOf(httpResponseMessage.Content.Headers.ContentType.ToString());
                isSuccess = true;
            } catch (Exception exception) {
                try {   //  Try download file via fallback URL
                    httpResponseMessage = await httpClient.GetAsync(tableRow[fallbackUrlIndex].ToString());
                    contentTypeIndexInAllowedTypes = allowedTypes.IndexOf(httpResponseMessage.Content.Headers.ContentType.ToString());
                    isSuccess = true;
                } catch (Exception exception2) {
                    //  Regardless of error, probably continue
                }
            }

            //  Save to folder
            if (isSuccess && contentTypeIndexInAllowedTypes != -1) {
                    //  Save to folder
                    Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    FileStream fileStream = new FileStream(downloadLocation + '/' + tableRow[fileNameIndex] + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None);   //  Might download other types. Make data-driven.
                    await contentStream.CopyToAsync(fileStream);
            } else {
                isSuccess = false;
            }

            //  Write to status
            DataRow statusRow = statusTable.NewRow();
            statusRow[0] = tableRow[fileNameIndex];
            statusRow[1] = isSuccess;
            statusTable.Rows.Add(statusRow);

            return httpResponseMessage;
        }

    }
}
