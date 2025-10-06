using System.Data;
using ClosedXML.Excel;

namespace Week_4_PDF_downloader {   //  Version 2.0
    public class FileHandler {
        //  Constants
        private static String DEFAULT_FOLDER_LOCATION = "../../../../Excel files";

        //  Fields
        private String[] files;
        private DataTable dataTable;

        //  Properties
        public String folderLocation = DEFAULT_FOLDER_LOCATION;

        /// <summary>
        /// Create a new FileHandler object which auto-detects files in default file path.
        /// </summary>
        public FileHandler() : this(DEFAULT_FOLDER_LOCATION) {
        }

        /// <summary>
        /// Create a new FileHandler object which auto-detects files in a given file path
        /// </summary>
        /// <param name="pathToFolder">File path literal. Can be relative or absolute.</param>
        public FileHandler(String pathToFolder) {
            folderLocation = pathToFolder;
            discoverFiles(pathToFolder);
        }

        /// <summary>
        /// Get table of items read from CSV file.
        /// </summary>
        /// <returns>Collection of string arrays. Should be parsed before pushing to database.</returns>
        public DataTable getTable() { 
            return dataTable;
        }

        /// <summary>
        /// Discover files and populate 'files' field with paths to files.
        /// </summary>
        /// <param name="pathToFolder">File path literal. Can be relative or absolute.</param>
        public void discoverFiles(String pathToFolder) {
            files = Directory.GetFiles(pathToFolder);
        }

        public int discoveredFilesCount() {
            return files.Length;
        }

        /// <summary>
        /// Read a given file (assuming CSV, breaking on a character). 
        /// Split into columns, then add as row of columns to table field.
        /// Takes first line as header; all other lines are content in table.
        /// </summary>
        /// <param name="discoveredFileIndex">Index of file-URIs as discovered by the FileHandler class.</param>
        /// <param name="breakOnChar">Character on which to break. Typically semicolon (;) for modern CSV-files.</param>
        public void readTableFromCsvFileWithHeaders(int  discoveredFileIndex, char breakOnChar) {
            dataTable = new DataTable();
            DataColumn dataColumn;
            DataRow dataRow;

            StreamReader streamReader = new StreamReader(files[0]);

            String line = streamReader.ReadLine();
            String[] row = null;
            int i = 0;
            while (line != null) {
                row = line.Split(breakOnChar);

                //  If first row (header), create columns
                if (i == 0) {
                    foreach (String column in row) {
                        dataColumn = new DataColumn();
                        dataColumn.DataType = typeof(String);
                        dataColumn.ColumnName = column;
                        dataColumn.Unique = true;

                        //  Add column to table. If duplicate column name exists, perform recursive function to add symbol to the end, then try again
                        try {
                            dataTable.Columns.Add(dataColumn);
                        } catch (DuplicateNameException dupeException) {
                            tryFixDuplicateName(dataColumn);
                        }
                    }
                } else {    //  Add data of StreamReader.ReadLine() to new row in DataTable, iterated per column
                    dataRow = dataTable.NewRow();
                    int j = 0;
                    foreach (DataColumn iteratedColumn in dataTable.Columns) {
                        Console.WriteLine("\nDEBUG - row " + i);
                        dataRow.SetField(iteratedColumn, row[j]);                   //  <-- possible error here
                        Console.Write(row[j].PadLeft(10));

                        j++;
                    }
                }

                i++;
                line = streamReader.ReadLine();
            }
            streamReader.Close();
        }

        /// <summary>
        /// Read a given excel file (.XLSX file type). 
        /// Assumes first line is header; all other lines are content in table.
        /// </summary>
        /// <param name="discoveredFileIndex">Index of file-URIs as discovered by the FileHandler class.</param>
        public void readTableFromExcelFileWithHeaders(int discoveredFileIndex) {
            dataTable = new DataTable();
            DataColumn dataColumn;
            DataRow dataRow;

            //  Use ClosedXML to read data.
            XLWorkbook workbook = new XLWorkbook(files[discoveredFileIndex]);
            IXLWorksheet worksheet = workbook.Worksheet(1);

            //  Iterate over rows
            int i = 1;
            foreach (IXLRow excelRow in worksheet.Rows()) {
                //  Iterate over columns
                int j = 1;
                foreach (IXLColumn excelColumn in worksheet.Columns()) {
                    //  If first row (header), create columns
                    if (i == 1) {
                        dataColumn = new DataColumn();
                        dataColumn.DataType = typeof(String);
                        dataColumn.ColumnName = excelColumn.Cell(1).GetString();
                        dataColumn.Unique = false;

                        //  Add column to table. If duplicate column name exists, or column has no name, perform recursive function to add symbol to the end, then try again
                        try {
                            dataTable.Columns.Add(dataColumn);
                        } catch (DuplicateNameException dupeException) {
                            tryFixDuplicateName(dataColumn);
                        }
                    } else {    //  If not first row, add data to DataTable
                        //  Subtract 1 because code runs "index starts at zero" while Excel runs "index starts at one".
                        dataRow = dataTable.NewRow();
                        dataRow[dataTable.Columns[j-1].ColumnName] = excelColumn.Cell(i).GetString();
                        dataTable.Rows.Add(dataRow);
                    }

                    j++;
                }

                j = 1;
                i++;
            }
        }

        /// <summary>
        /// Read a given excel file (.XLSX file type). 
        /// Provides data as-is, without headers
        /// </summary>
        /// <param name="discoveredFileIndex">Index of file-URIs as discovered by the FileHandler class.</param>
        public void readTableFromExcelFileNoHeaders(int discoveredFileIndex) {
            dataTable = new DataTable();
            DataColumn dataColumn;
            DataRow dataRow;

            //  Use ClosedXML to read data.
            XLWorkbook workbook = new XLWorkbook(files[discoveredFileIndex]);
            IXLWorksheet worksheet = workbook.Worksheet(1);

            //  Iterate over rows
            int i = 1;
            foreach (IXLRow excelRow in worksheet.Rows()) {
                dataRow = dataTable.NewRow();

                //  Iterate over columns
                foreach (IXLColumn excelColumn in worksheet.Columns()) {
                    //  If first row (header), create columns. Fill data regardless
                    if (i == 1) {
                        dataColumn = new DataColumn();
                        dataColumn.DataType = typeof(String);

                        dataTable.Columns.Add(dataColumn);
                    }

                    dataRow[excelColumn.Cell(1).GetString()] = excelColumn.Cell(i).GetString();
                }
            }
        }

        public void tryFixDuplicateName(DataColumn dataColumn) {
            dataColumn.ColumnName += '_';
            try {
                dataTable.Columns.Add(dataColumn);
            } catch (DuplicateNameException dupeException) {
                tryFixDuplicateName(dataColumn);
            }
        }
    }
}
