// used for checking if the database connection works.
// ConnectDb method returns a connection object.

using Npgsql;
namespace DataAccessLayer
{
    public class Connect
    {
        public NpgsqlConnection ConnectDb()
        {
            DotNetEnv.Env.Load();
            var connectionString = $"Host={Environment.GetEnvironmentVariable("Host")};" +
                $"Port={Environment.GetEnvironmentVariable("Port")};" +
                $"Database={Environment.GetEnvironmentVariable("Database")};" +
                $"Username={Environment.GetEnvironmentVariable("Username")};" +
                $"Password={Environment.GetEnvironmentVariable("Password")}";

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
