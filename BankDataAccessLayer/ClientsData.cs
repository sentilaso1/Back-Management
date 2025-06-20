﻿using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Policy;

namespace BankDataAccessLayer
{
    public class ClientsData
    {

        public static bool GetClientInfoByClinetID(int ClientID, ref string AccountNumber, ref string firstName, ref string lastName, ref string email, ref string phoneNumber, ref string PinCode, ref decimal Balance)
        {
            bool isFound = false;

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select *
                            from Clients
                            where ClientID = @ClientID";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClientID", ClientID);

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    firstName = (string)reader["FirstName"];
                    lastName = (string)reader["LastName"];
                    email = (string)reader["Email"];
                    phoneNumber = (string)reader["Phone"];
                    PinCode = (string)reader["PinCode"];
                    AccountNumber = (string)reader["AccountNumber"];
                    Balance = (decimal)reader["Balance"];

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetClientInfoByAccountNumber(string AccountNumber, ref string firstName, ref string lastName, ref string email, ref string phoneNumber, ref string PinCode, ref decimal Balance, ref int ClientID)
        {
            bool isFound = false;

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select *
                            from Clients
                            where Clients.AccountNumber = @AccountNumber";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@AccountNumber", AccountNumber);

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    ClientID = (int)reader["ClientID"];
                    firstName = (string)reader["FirstName"];
                    lastName = (string)reader["LastName"];
                    email = (string)reader["Email"];
                    phoneNumber = (string)reader["Phone"];
                    PinCode = (string)reader["PinCode"];
                    Balance = (decimal)reader["Balance"];

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool DeleteClientByAccountNumber(string AccountNumber)
        {
            int RowsAffected = 0;

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete from Clients
                            where Clients.AccountNumber = @AccountNumber";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@AccountNumber", AccountNumber);

            try
            {

                connection.Open();

                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.ToString());
            }

            finally
            {

                connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool DeleteClientByID(int ClientID)
        {
            int RowsAffected = 0;

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete from Clients
                            where ClientID = @ClientID";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClientID", ClientID);

            try
            {

                connection.Open();

                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.ToString());
            }

            finally
            {

                connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool UpdateClient(string firstName, string lastName, string email, string phoneNumber, string AccountNumber, string PinCode, decimal Balance)
        {
            int RowsAffected = 0;

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Clients
                            Set 
                                FirstName = @FirstName,
                                 LastName = @LastName,
                                 Email = @Email,
                                 Phone = @Phone,
	                            Balance = @Balance,
	                            PinCode = @PinCode
	                            where AccountNumber = @AccountNumber
                            ";

            MySqlCommand Command = new MySqlCommand(query, connection);

            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            Command.Parameters.AddWithValue("@FirstName", firstName);
            Command.Parameters.AddWithValue("@LastName", lastName);
            Command.Parameters.AddWithValue("@Email", email);
            Command.Parameters.AddWithValue("@Phone", phoneNumber);
            Command.Parameters.AddWithValue("@PinCode", PinCode);
            Command.Parameters.AddWithValue("@Balance", Balance);

            try
            {
                connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;
        }

        public static DataTable GetAllClients()
        {
            DataTable dt = new DataTable();

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select AccountNumber, FirstName, LastName, Email, Phone, PinCode, Balance from Clients
                          ";

            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static int AddNewClient(string firstName, string lastName, string email, string phoneNumber, string AccountNumber, string PinCode, decimal Balance)
        {
            int ClientID = -1;

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                            insert into Clients(AccountNumber, FirstName, LastName, Email, Phone, PinCode, Balance)
                            Values (@AccountNumber, @FirstName, @LastName, @Email, @Phone, @PinCode, @Balance);
                            SELECT LAST_INSERT_ID()";

            MySqlCommand Command = new MySqlCommand(query, connection);

            Command.Parameters.AddWithValue("@FirstName", firstName);
            Command.Parameters.AddWithValue("@LastName", lastName);
            Command.Parameters.AddWithValue("@Email", email);
            Command.Parameters.AddWithValue("@Phone", phoneNumber);
            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            Command.Parameters.AddWithValue("@PinCode", PinCode);
            Command.Parameters.AddWithValue("@Balance", Balance);

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ClientID = insertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return ClientID;
        }

        public static bool isClientExist(string AccountNumber)
        {

            bool isFound = false;

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select 1 as Found from Clients
                                Where (Clients.AccountNumber = @AccountNumber)";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@AccountNumber", AccountNumber);

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool Transfer(string FromAccountNumber, string ToAccountNumber, decimal Amount)
        {
            int RowsAffected = 0;
            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Clients 
                            set Balance = Balance - @Amount
                            where AccountNumber = @FromAccountNumber ;

                            Update Clients
                            set Balance = Balance + @Amount
                            where AccountNumber = @ToAccountNumber ;";

            MySqlCommand Command = new MySqlCommand(query, connection);

            Command.Parameters.AddWithValue("@FromAccountNumber", FromAccountNumber);
            Command.Parameters.AddWithValue("@ToAccountNumber", ToAccountNumber);
            Command.Parameters.AddWithValue("@Amount", Amount);

            try
            {
                connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected >= 2;
        }

        public static bool Deposit(string AccountNumber, decimal Amount)
        {
            int RowsAffected = 0;
            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Clients 
                            set Balance = Balance + @Amount
                            where AccountNumber = @AccountNumber ;";

            MySqlCommand Command = new MySqlCommand(query, connection);

            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            Command.Parameters.AddWithValue("@Amount", Amount);

            try
            {
                connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;
        }

        public static bool Withdraw(string AccountNumber, decimal Amount)
        {
            int RowsAffected = 0;
            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Clients 
                            set Balance = Balance - @Amount
                            where AccountNumber = @AccountNumber ;";

            MySqlCommand Command = new MySqlCommand(query, connection);

            Command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            Command.Parameters.AddWithValue("@Amount", Amount);

            try
            {
                connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;
        }

        public static DataTable GetAllBalances()
        {
            DataTable dataTable = new DataTable();

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select AccountNumber, FirstName, LastName, Balance From Clients";

            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }

        public static int GetTotalClients()
        {
            int totalClients = 0;

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select Count(Clients.ClientID) from Clients";

            MySqlCommand Command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedTotalClients))
                {
                    totalClients = InsertedTotalClients;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            finally
            {
                connection.Close();
            }

            return totalClients;
        }

        public static decimal GetTotalBalances()
        {
            decimal TotalBalances = 0;

            MySqlConnection connection = new MySqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select Sum(Balance) from Clients";

            MySqlCommand Command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && decimal.TryParse(Result.ToString(), out decimal InsertedTotalBalances))
                {
                    TotalBalances = InsertedTotalBalances;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            finally
            {
                connection.Close();
            }

            return TotalBalances;
        }
    }
}
