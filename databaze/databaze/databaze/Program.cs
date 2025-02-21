using database_app.DAO;
using db;
using System.Configuration;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        //Menu menu = new Menu();
        //menu.Interface();

        //IMPORT CSV
        string customerCsvPath = @"Zakaznik.csv";
        string authorCsvPath = @"Autor.csv";
        CsvImport csvImport = new CsvImport();

        try
        {
            csvImport.CustomersCSV(customerCsvPath);
            Console.WriteLine("Import zákazníků byl úspěšný.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při importu zákazníků: {ex.Message}");
        }

        try
        {
            csvImport.AuthorsCSV(authorCsvPath);
            Console.WriteLine("Import autorů byl úspěšný.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při importu autorů: {ex.Message}");
        }
    }
}





