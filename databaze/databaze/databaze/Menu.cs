using database_app.DAO;
using db;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

internal class Menu
{
    private Select select;
    private Update update;
    private Delete delete;
    private Insert insert;

    public Menu()
    {
        select = new Select();
        insert = new Insert();
        delete = new Delete();
        update = new Update();
    }

    public void Interface()
    {
        while (true)
        {
            Console.WriteLine("Vítejte v databázové aplikaci");
            Console.WriteLine("1 --> Zobrazení seznamu knih (Select)");
            Console.WriteLine("2 --> Registrace nového uživatele (Insert)");
            Console.WriteLine("3 --> Navrácení knihy - konec zápůjčky (Delete)");
            Console.WriteLine("4 --> Nová rezervace/zápůjčka (Update)");
            Console.WriteLine("5 --> Konec programu");
            Console.WriteLine("Vyberte co chcete udělat:");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        select.SelectBooks();
                        break;
                    case "2":
                        Console.WriteLine("Zadejte jméno:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Zadejte příjmení:");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Zadejte email:");
                        string email = Console.ReadLine();
                        Console.WriteLine("Zadejte telefon:");
                        string phone = Console.ReadLine();
                        insert.InsertCustomer(firstName, lastName, email, phone);
                        break;
                    case "3":
                        Console.WriteLine("Zadejte název knihy:");
                        string bookName = Console.ReadLine();
                        delete.RemoveLoan(bookName);
                        break;
                    case "4":
                        Console.WriteLine("Zadejte email zákazníka:");
                        email = Console.ReadLine();
                        Console.WriteLine("Zadejte název knihy:");
                        bookName = Console.ReadLine();
                        Console.WriteLine("Zadejte datum zapůjčení (YYYY-MM-DD):");
                        string loanDateInput = Console.ReadLine();
                        DateTime loanDate = DateTime.Parse(loanDateInput);
                        Console.WriteLine("Zadejte datum vrácení (YYYY-MM-DD):");
                        string returnDateInput = Console.ReadLine();
                        DateTime returnDate = DateTime.Parse(returnDateInput);
                        update.AddLoan(email, bookName, loanDate, returnDate);
                        break;
                    case "5":
                        Console.WriteLine("Ukončuji aplikaci :)");
                        return;
                    default:
                        Console.WriteLine("Cti poradne!!! Na vyber jsou jen varianty 1-5 :(");
                        break;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Nastala chyba při zpracování data: " + ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Nastala chyba při přístupu k databázi: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nastala neočekávaná chyba: " + ex.Message);
            }

            Console.WriteLine("Stiskněte libovolnou klávesu pro návrat do menu :)");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
