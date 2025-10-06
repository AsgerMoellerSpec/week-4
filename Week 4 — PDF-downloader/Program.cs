namespace Week_4_PDF_downloader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileHandler fileHandler = new FileHandler();
            Console.WriteLine(fileHandler.discoveredFilesCount());
            fileHandler.readTableFromExcelFileWithHeaders(0);
            Console.WriteLine("DEBUG — IT FUCKING WORKS, BITCH!");
            Console.Write(fileHandler.getTable().Rows[1][38] + " ");
            Console.WriteLine(fileHandler.getTable().Rows[1][39]);
        }
    }
}
