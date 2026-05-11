using Npgsql;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class NotificationRepository : IRepository<Notification, string>
    {
        Connect connect;
        NpgsqlConnection connection;

        public NotificationRepository()
        {
            connect = new Connect();
            connection = connect.ConnectDb();
        }

        public Notification Create(Notification notification)
        {
            var id = Guid.NewGuid().ToString();
            notification.MessageId = id;

            // type casting is required for date and type fields
            string insertQuery = $"INSERT INTO notifications VALUES ('{notification.MessageId}', '{notification.Message}', '{notification.SentDate:yyyy-MM-dd}', {(int)notification.NotifType}, '{notification.UserId}')";
            NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);

            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("Notification created successfully!");
            }
            catch (PostgresException ex)
            {
                switch (ex.SqlState)
                {
                    case "23503":
                        Console.WriteLine("User does not exist.");
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
            return notification;
        }

        public List<Notification>? ReadAll()
        {
            List<Notification> notifications = new List<Notification>();
            string selectQuery = "SELECT * FROM notifications";
            NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection);

            try
            {
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                    return null;

                while (reader.Read())
                {
                    Notification notification = new Notification();
                    notification.MessageId = reader[0].ToString() ?? "";
                    notification.Message = reader[1].ToString() ?? "";
                    notification.SentDate = Convert.ToDateTime(reader[2]);
                    notification.NotifType = (NotifType)Convert.ToInt16(reader[3]);
                    notification.UserId = reader[4].ToString() ?? "";
                    notifications.Add(notification);
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
            return notifications;
        }

        public Notification? Read(string key)
        {
            string selectQuery = $"SELECT * FROM notifications WHERE messageid='{key}'";
            NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection);
            Notification notification = new Notification();

            try
            {
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                    return null;

                while (reader.Read())
                {
                    notification.MessageId = reader[0].ToString() ?? "";
                    notification.Message = reader[1].ToString() ?? "";
                    notification.SentDate = Convert.ToDateTime(reader[2]);
                    notification.NotifType = (NotifType)Convert.ToInt16(reader[3]);
                    notification.UserId = reader[4].ToString() ?? "";
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
            return notification;
        }

        public Notification? Update(Notification notification, string key)
        {
            string updateQuery = $"UPDATE notifications SET message='{notification.Message}', sentdate='{notification.SentDate:yyyy-MM-dd}', notiftype={(int)notification.NotifType} WHERE messageid='{key}'";
            NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection);

            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("Notification updated successfully!");
                else
                {
                    Console.WriteLine($"No notification found with id {key}");
                    return null;
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

            Notification? updated = Read(key);
            return updated;
        }

        public Notification? Delete(string key)
        {
            string deleteQuery = $"DELETE FROM notifications WHERE messageid='{key}'";
            NpgsqlCommand command = new NpgsqlCommand(deleteQuery, connection);
            Notification? notification = new Notification();

            try
            {
                notification = Read(key);
                if (notification == null)
                {
                    Console.WriteLine($"No notification found with id {key}");
                    return null;
                }
                else
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                        Console.WriteLine("Notification deleted successfully!");
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
            return notification;
        }
    }
}
