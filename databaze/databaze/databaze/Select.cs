using db;
using System;
using System.Data.SqlClient;

namespace db
{
    internal class Select
    {
        public Select()
        {
        }

        /// <summary>
        /// Získá všechny informace o knihách v databázi
        /// </summary>
        public void SelectBooks()
        {
            try
            {
                using (SqlConnection connection = Singleton.Connect())
                {
                    string query = @"
                        select 
                            k.kniha_id,
                            k.nazev,
                            k.isbn,
                            k.datum_vydani,
                            k.pocet_stran,
                            k.nakladatelstvi,
                            a.jmeno AS autor_jmeno,
                            a.prijmeni AS autor_prijmeni,
                            p.dostupnost,
                            z.datum_zapujceni,
                            z.datum_vraceni
                        from 
                            Kniha k
                        inner join 
                            Autor a on k.autor_id = a.autor_id
                        left join 
                            Produkt p on k.kniha_id = p.kniha_id
                        left join 
                            Zapujcka z on p.zapujcka_id = z.zapujcka_id";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("Detaily knih:");
                    while (reader.Read())
                    {
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.WriteLine($"ID: {reader["kniha_id"]}, Název: {reader["nazev"]}");
                        Console.WriteLine($"ISBN: {reader["isbn"]}, Datum vydání: {reader["datum_vydani"]}");
                        Console.WriteLine($"Počet stran: {reader["pocet_stran"]}, Nakladatelství: {reader["nakladatelstvi"]}");
                        Console.WriteLine($"Autor: {reader["autor_jmeno"]} {reader["autor_prijmeni"]}");
                        Console.WriteLine($"Dostupnost: {((bool)reader["dostupnost"] ? "Dostupná" : "Nedostupná")}");

                        if (reader["datum_zapujceni"] != DBNull.Value)
                        {
                            Console.WriteLine($"Datum zapůjčení: {reader["datum_zapujceni"]}, Datum vrácení: {reader["datum_vraceni"]}");
                        }
                        Console.WriteLine("-------------------------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Došlo k chybě při získávání dat :( " + ex.Message);
            }
        }
    }
}
