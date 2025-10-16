using System.Data;

public class MockFileHandler : Week_4_PDF_downloader.IFileHandler
{
    private bool throwsIndexOutOfRangeException;

    public MockFileHandler(bool returnsIndexOutOfRangeException = false)
    {
        this.throwsIndexOutOfRangeException = throwsIndexOutOfRangeException;
    }

    public DataTable getTable()
    {
        return null;
    }

    public string getFolderLocation()
    {
        return "";
    }

    public void readTableFromExcelFileWithHeaders(int discoveredFileIndex)
    {
        if (throwsIndexOutOfRangeException) throw new IndexOutOfRangeException();
    }

    public void writeToCsvFileWithHeaders(DataTable dataTable, List<int> indicesOfColumnsToWrite, char separatorCharacter)
    {
        return;
    }
}