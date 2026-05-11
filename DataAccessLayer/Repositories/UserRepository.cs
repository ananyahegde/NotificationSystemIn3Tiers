using Npgsql;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{

    public class UserRepository : IRepository<User, string>
    {
        Connect connect;
        NpgsqlConnection connection;

        public UserRepository()
        {
            connect = new Connect();
            connection = connect.ConnectDb();
        }


        public User Create(User user)
        {
            var id = Guid.NewGuid().ToString();
            user.UserId = id;

            string insertQuery = $"INSERT INTO users VALUES ('{user.UserId}', '{user.Name}', '{user.Email}', '{user.Phone}')";

            NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);
            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("User created successfully!");
            }
            catch (PostgresException ex)
            {
                switch (ex.SqlState)
                {
                    case "23505":
                        Console.WriteLine("Username or Email or Phone already exists.");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection?.Close();
            }
            return user;
        }


        public List<User>? ReadAll()
        {
            List<User> users = new List<User>();
            string selectQuery = "SELECT * FROM users";
            NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection);

            try
            {
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                    return null;

                while (reader.Read())
                {
                    User user = new User();
                    user.UserId = reader[0].ToString() ?? "";
                    user.Name = reader[1].ToString() ?? "";
                    user.Email = reader[2].ToString() ?? "";
                    user.Phone = reader[3].ToString() ?? "";
                    users.Add(user);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception occurred: {e.Message}");
            }
            finally
            {
                connection.Close();
            }
            return users;
        }

        public User? Read(string key)
        {
            string selectQuery = $"SELECT * FROM users WHERE userid='{key}'";
            NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection);
            User user = new User();

            try
            {
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                    return null;

                while (reader.Read())
                {
                    user.UserId = reader[0].ToString() ?? "";
                    user.Name = reader[1].ToString() ?? "";
                    user.Email = reader[2].ToString() ?? "";
                    user.Phone = reader[3].ToString() ?? "";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception occurred: {e.Message}");
            }
            finally
            {
                connection.Close();
            }
            return user;
        }


        public User? Update(User user, string key)
        {
            string updateQuery = $"UPDATE users SET name='{user.Name}', email='{user.Email}', phone='{user.Phone}' WHERE userid='{user.UserId}'";
            NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection);

            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("User updated successfully!");
                else
                {
                    Console.WriteLine($"No user found with username {key}");
                    return null;
                }
            }
            catch (PostgresException ex)
            {
                switch (ex.SqlState)
                {
                    case "23505":
                        Console.WriteLine("Username or Email or Phone already exists.");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            User? updatedUser = Read(user.UserId);
            return updatedUser;
        }

        public User? Delete(string key)
        {
            string deleteQuery = $"DELETE FROM users WHERE userid='{key}'";
            NpgsqlCommand command = new NpgsqlCommand(deleteQuery, connection);
            User? user = new User();
            try
            {
                user = Read(key);
                if (user == null)
                {
                    Console.WriteLine($"No user found with username {key}");
                    return null;
                }
                else
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                        Console.WriteLine("User deleted successfully!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection?.Close();
            }
            return user;
        }
    }
}
