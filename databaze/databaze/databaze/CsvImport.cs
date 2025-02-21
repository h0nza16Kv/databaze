using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace db
{
    internal class CsvImport
    {
        Insert insert;
        public CsvImport()
        {
            insert = new Insert();
        }

       

        /// <summary>
        /// Přidá nového autora do databáze
        /// </summary>
        /// <param name="firstName">Jméno autora</param>
        /// <param name="lastName">Příjmení autora</param>
        /// <param name="nationality">Národnost autora</param>
        /// <param name="birthDate">Datum narození autora</param>
        public void InsertAuthor(string firstName, string lastName, string nationality, DateTime birthDate)
        {
            using (SqlConnection connection = Singleton.Connect())
            {
                string sql = "INSERT INTO Autor (jmeno, prijmeni, narodnost, datum_narozeni) VALUES (@FirstName, @LastName, @Nationality, @BirthDate)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Nationality", nationality);
                command.Parameters.AddWithValue("@BirthDate", birthDate);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine("Autor byl úspěšně přidán.");
            }
        }
        
        // AI    
        /// <summary>
        /// Importuje zákazníky z CSV souboru do databáze
        /// </summary>
        /// <param name="filePath">Cesta k CSV souboru</param>
        public void CustomersCSV(string filePath)
        {
            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length >= 4)
                    {
                        string firstName = values[0];
                        string lastName = values[1];
                        string email = values[2];
                        string phone = values[3];

                        insert.InsertCustomer(firstName, lastName, email, phone);
                    }
                }
            }
        }

        //AI
        /// <summary>
        /// Importuje autory z CSV souboru do databáze
        /// </summary>
        /// <param name="filePath">Cesta k CSV souboru</param>
        public void AuthorsCSV(string filePath)
        {
            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length >= 4)
                    {
                        string firstName = values[0];
                        string lastName = values[1];
                        string nationality = values[2];
                        DateTime birthDate = DateTime.Parse(values[3]);

                        InsertAuthor(firstName, lastName, nationality, birthDate);
                    }
                }
            }
        }
    }
}
