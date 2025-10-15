using System.Data;

interface IFileHandler
{
    DataTable getTable();
    string getFolderLocation();
    void readTableFromExcelFileWithHeaders(int discoveredFileIndex);
    void writeToCsvFileWithHeaders(DataTable dataTable, List<int> indicesOfColumnsToWrite, char separatorCharacter);
}