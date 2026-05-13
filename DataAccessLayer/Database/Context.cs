// used for checking if the database connection works.
// ConnectDb method returns a connection object.

using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;

namespace DataAccessLayer.Database
{
    public class Context : DbContext
    {
        public Context() { }

        //     public NpgsqlConnection ConnectDb()
        //     {
        //         DotNetEnv.Env.Load();
        //         var connectionString = $"Host={Environment.GetEnvironmentVariable("Host")};" +
        //             $"Port={Environment.GetEnvironmentVariable("Port")};" +
        //             $"Database={Environment.GetEnvironmentVariable("Database")};" +
        //             $"Username={Environment.GetEnvironmentVariable("Username")};" +
        //             $"Password={Environment.GetEnvironmentVariable("Password")}";
        //
        //         NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        //
        //         try
        //         {
        //             connection.Open();
        //             Console.WriteLine("Database Connected Successfully");
        //         }
        //         catch (Exception e)
        //         {
        //             Console.WriteLine(e.Message);
        //         }
        //         finally
        //         {
        //             connection?.Close();
        //         }
        //         return connection;
        //     }


        public DbSet<User>? users { get; set; }
        public DbSet<Notification>? notifications { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // DotNetEnv.Env.Load();
            // optionsBuilder.UseNpgsql($"Host={Environment.GetEnvironmentVariable("Host")};" +
            //    $"Port={Environment.GetEnvironmentVariable("Port")};" +
            //    $"Database={Environment.GetEnvironmentVariable("Database")};" +
            //    $"Username={Environment.GetEnvironmentVariable("Username")};" +
            //    $"Password={Environment.GetEnvironmentVariable("Password")}");

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=notificationsystemefcore;Username=postgres;Password=yourpassword");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(u => u.UserId).HasName("pk_users_userid");
                //seeding
                u.HasData(new User()
                {
                    UserId = "1",
                    Name = "User1",
                    Phone = "1234567891",
                    Email = "user1@gmail.com"
                });
            });

            modelBuilder.Entity<Notification>(n =>
            {
                n.HasKey(n => n.MessageId).HasName("pk_notifications_messageid");

                n.Property(n => n.SentDate).HasColumnType("timestamp without time zone");

                n.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .HasConstraintName("fk_notifications_users_userid")
                .OnDelete(DeleteBehavior.Restrict);

                n.HasData(new Notification()
                {
                    MessageId = "1",
                    Message = "First Message",
                    SentDate = DateTime.Now,
                    UserId = "1"
                });
            });
        }
    }
}
