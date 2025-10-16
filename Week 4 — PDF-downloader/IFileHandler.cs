using System.Data;

namespace Week_4_PDF_downloader
{
    public interface IFileHandler
    {
        DataTable getTable();
        string getFolderLocation();
        void readTableFromExcelFileWithHeaders(int discoveredFileIndex);
        void writeToCsvFileWithHeaders(DataTable dataTable, List<int> indicesOfColumnsToWrite, char separatorCharacter);
    }
}