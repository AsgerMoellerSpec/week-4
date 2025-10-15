public class MockFileHandler : Week_4_PDF_downloader.IFileHandler
{
    DataTable getTable()
    {
        return null;
    }

    string getFolderLocation()
    {
        return "";
    }

    void readTableFromExcelFileWithHeaders(int discoveredFileIndex)
    {
        return;
    }

    void writeToCsvFileWithHeaders(DataTable dataTable, List<int> indicesOfColumnsToWrite, char separatorCharacter)
    {
        return;
    }
}