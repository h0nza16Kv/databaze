using db;
using System;
using System.Data.SqlClient;

namespace db
{
    internal class Delete
    {
        public Delete()
        {
        }

        /// <summary>
        /// Odstraní zápůjčku z databáze
        /// </summary>
        /// <param name="bookTitle"> Název knihy</param>
        public void RemoveLoan(string bookTitle)
        {
            using (SqlConnection connection = Singleton.Connect())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string findProductIdQuery = @"
                        SELECT P.produkt_id, P.zapujcka_id 
                        FROM Produkt P
                        INNER JOIN Kniha K ON P.kniha_id = K.kniha_id 
                        WHERE K.nazev = @BookTitle";
                    SqlCommand findProductIdCommand = new SqlCommand(findProductIdQuery, connection, transaction);
                    findProductIdCommand.Parameters.AddWithValue("@BookTitle", bookTitle);
                    SqlDataReader reader = findProductIdCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Kniha s daným názvem nebyla nalezena.");
                        return;
                    }

                    reader.Read();
                    int productID = (int)reader["produkt_id"];
                    object loanID = reader["zapujcka_id"];

                    if (loanID == DBNull.Value)
                    {
                        Console.WriteLine("Kniha není zapůjčená, není co odstranit.");
                        return;
                    }

                    reader.Close();

                    string deleteLoanQuery = "DELETE FROM Zapujcka WHERE zapujcka_id = @LoanID";
                    SqlCommand deleteLoanCommand = new SqlCommand(deleteLoanQuery, connection, transaction);
                    deleteLoanCommand.Parameters.AddWithValue("@LoanID", loanID);
                    deleteLoanCommand.ExecuteNonQuery();

                    string updateProductQuery = "UPDATE Produkt SET dostupnost = 1 WHERE produkt_id = @ProductID";
                    SqlCommand updateProductCommand = new SqlCommand(updateProductQuery, connection, transaction);
                    updateProductCommand.Parameters.AddWithValue("@ProductID", productID);
                    updateProductCommand.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine("Zapůjčka byla úspěšně odstraněna a produkt byl aktualizován na dostupný.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Došlo k chybě při odstraňování zapůjčky a aktualizaci produktu: " + ex.Message);
                }
            }
        }
    }
}