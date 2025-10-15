using System.Data;

interface IFileHandler
{
    DataTable getTable();
    void readTableFromExcelFileWithHeaders(int discoveredFileIndex);
    void writeToCsvFileWithHeaders(DataTable dataTable, List<int> indicesOfColumnsToWrite, char separatorCharacter);
}