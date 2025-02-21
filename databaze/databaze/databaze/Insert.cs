using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace db
{
    internal class Insert
    {
        public Insert()
        {
        }

        /// <summary>
        /// Přidá nového zákazníka do databáze
        /// </summary>
        /// <param name="firstName">Jméno zákazníka</param>
        /// <param name="lastName">Příjmení zákazníka</param>
        /// <param name="email">Email zákazníka</param>
        /// <param name="phone">Telefon zákazníka</param>
        public void InsertCustomer(string firstName, string lastName, string email, string phone)
        {
            if (IsValidCustomer(firstName, lastName, email, phone))
            {
                using (SqlConnection connection = Singleton.Connect())
                {
                    string sql = "INSERT INTO Zakaznik (jmeno, prijmeni, email, telefon) VALUES (@FirstName, @LastName, @Email, @Phone)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);

                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Zákazník byl úspěšně přidán.");
                }
            }
            else
            {
                Console.WriteLine("Neplatné údaje o zákazníkovi.");
            }
        }

        private bool IsValidCustomer(string firstName, string lastName, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || !Regex.IsMatch(firstName, "^[A-Z].*"))
                return false;
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || !Regex.IsMatch(lastName, "^[A-Z].*"))
                return false;
            //AI
            if (!Regex.IsMatch(email, @"^.+@.+\..+$"))
                return false;
            if (!Regex.IsMatch(phone, @"^\+?[\d\s]+$"))
                return false;
            //
            return true;
        }
    }
}
