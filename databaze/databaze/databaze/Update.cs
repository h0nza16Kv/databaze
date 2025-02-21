using db;
using System;
using System.Data.SqlClient;

namespace database_app.DAO
{
    internal class Update
    {
        public Update()
        {
        }

        /// <summary>
        /// Získá CustomerID na základě emailu
        /// </summary>
        /// <param name="email">Email zákazníka</param>
        /// <returns>CustomerID</returns>
        private int GetCustomerID(string email)
        {
            using (SqlConnection conn = Singleton.Connect())
            {
                string sql = "SELECT zakaznik_id FROM Zakaznik WHERE email = @Email";
                SqlCommand commad = new SqlCommand(sql, conn);
                commad.Parameters.AddWithValue("@Email", email);

                conn.Open();
                //AI
                var result = commad.ExecuteScalar();
                return result != null ? (int)result : -1;
            }
        }

        /// <summary>
        /// Získá ProductID na základě názvu knihy
        /// </summary>
        /// <param name="bookTitle">Název knihy</param>
        /// <returns>ProductID</returns>
        private int GetProductID(string bookTitle)
        {
            using (SqlConnection connection = Singleton.Connect())
            {
                string sql = "SELECT produkt_id FROM Produkt P INNER JOIN Kniha K ON P.kniha_id = K.kniha_id WHERE K.nazev = @BookTitle";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@BookTitle", bookTitle);

                connection.Open();
                //AI
                var result = command.ExecuteScalar();
                return result != null ? (int)result : -1;
            }
        }

        /// <summary>
        /// Přidá novou zapůjčku a aktualizuje produkt
        /// </summary>
        /// <param name="email">Email zákazníka</param>
        /// <param name="bookTitle">Název knihy</param>
        /// <param name="loanDate">Datum zapůjčení</param>
        /// <param name="returnDate">Datum vrácení</param>
        public void AddLoan(string email, string bookTitle, DateTime loanDate, DateTime returnDate)
        {
            int customerID = GetCustomerID(email);
        

            int productID = GetProductID(bookTitle);
            using (SqlConnection connection = Singleton.Connect())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string sqlLoan = "INSERT INTO Zapujcka (zakaznik_id, datum_zapujceni, datum_vraceni) VALUES (@CustomerID, @LoanDate, @ReturnDate); SELECT SCOPE_IDENTITY();";
                    SqlCommand commandLoan = new SqlCommand(sqlLoan, connection, transaction);
                    commandLoan.Parameters.AddWithValue("@CustomerID", customerID);
                    commandLoan.Parameters.AddWithValue("@LoanDate", loanDate);
                    commandLoan.Parameters.AddWithValue("@ReturnDate", returnDate);
                    int loanID = Convert.ToInt32(commandLoan.ExecuteScalar());

                    string sqlProduct = "UPDATE Produkt SET dostupnost = 0, zapujcka_id = @LoanID WHERE produkt_id = @ProductID";
                    SqlCommand commandProduct = new SqlCommand(sqlProduct, connection, transaction);
                    commandProduct.Parameters.AddWithValue("@LoanID", loanID);
                    commandProduct.Parameters.AddWithValue("@ProductID", productID);
                    commandProduct.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine("Zapůjčka byla úspěšně přidána a produkt byl aktualizován.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Došlo k chybě při přidávání zapůjčky a aktualizaci produktu: " + ex.Message);
                }
            }
        }
    }
}