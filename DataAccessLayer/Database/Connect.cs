// used for checking if the database connection works.
// ConnectDb method returns a connection object.

using Npgsql;

namespace DataAccessLayer
{
    public class Connect
    {
        public Connect()
        {
            StoreEnv storeEnv = new StoreEnv();
            storeEnv.StoreEnvVariables();
        }

        public NpgsqlConnection ConnectDb()
        {
            string? host = System.Environment.GetEnvironmentVariable("Host");
            string? port = System.Environment.GetEnvironmentVariable("Port");
            string? database = System.Environment.GetEnvironmentVariable("Database");
            string? username = System.Environment.GetEnvironmentVariable("Username");
            string? password = System.Environment.GetEnvironmentVariable("Password");

            string connectionString =
              $"Host={host};Port={port};Database={database};Username={username};Password={password}";

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Database Connected Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return connection;
        }
    }
}
