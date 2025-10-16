using System.Data;

public class MockFileHandler : Week_4_PDF_downloader.IFileHandler
{
    private DataTable table;
    private bool throwsIndexOutOfRangeException;
    private DataTable latestTableWritten;
    private List<int> latestIndicesWritten;
    private char latestSeparatorWritten;

    public MockFileHandler(int numberOfRows = 0, int numberOfColumns = 39, bool throwsIndexOutOfRangeException = false)
    {
        this.throwsIndexOutOfRangeException = throwsIndexOutOfRangeException;
        this.table = createMockTable(numberOfRows, numberOfColumns);
    }

    public DataTable getTable()
    {
        return table;
    }

    public string getFolderLocation()
    {
        return "mock folder location";
    }

    public DataTable getLatestTableWritten()
    {
        return latestTableWritten;
    }

    public List<int> getLatestIndicesWritten()
    {
        return latestIndicesWritten;
    }

    public char getLatestSeparatorWritten()
    {
        return latestSeparatorWritten;
    }

    public void readTableFromExcelFileWithHeaders(int discoveredFileIndex)
    {
        if (throwsIndexOutOfRangeException) throw new IndexOutOfRangeException();
    }

    public void writeToCsvFileWithHeaders(DataTable dataTable, List<int> indicesOfColumnsToWrite, char separatorCharacter)
    {
        latestTableWritten = dataTable;
        latestIndicesWritten = indicesOfColumnsToWrite;
        latestSeparatorWritten = separatorCharacter;
    }

    private DataTable createMockTable(int numberOfRows, int numberOfColumns)
    {
        DataTable mockTable = new();
        for (int i = 0; i < numberOfColumns; i++)
        {
            mockTable.Columns.Add(i.ToString(), typeof(string));
        }
        for (int j = 0; j < numberOfRows; j++)
        {
            DataRow row = mockTable.NewRow();
            for (int i = 0; i < numberOfColumns; i++)
            {
                row[i.ToString()] = "row " + i + ", column " + j;
            }
            mockTable.Rows.Add(row);
        }
        return mockTable;
    }
}