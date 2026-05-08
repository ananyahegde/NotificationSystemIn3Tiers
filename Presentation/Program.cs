using BusinessLayer.Interfaces;
using BusinessLayer.Services;

namespace Presentation
{
    internal class Program
    {
        INotificationInteract notificationInteract;
        IUserInteract userInteract;

        public Program()
        {
            notificationInteract = new NotificationService();
            userInteract = new UserService();
        }

        internal void DoCrudOperations()
        {
            UserService userService = new UserService();
            NotificationService notificationService = new NotificationService();

            while (true)
            {
                Console.WriteLine("\nPlease enter what you wish to do.");
                Console.WriteLine("1. User Management");
                Console.WriteLine("2. Notification Management");
                Console.WriteLine("3. Go Back");

                int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1:
                        userService.UserMenu();
                        break;
                    case 2:
                        notificationService.NotificationMenu();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        internal void SendNotification()
        {
            UserService userService = new UserService();

            while (true)
            {
                Console.WriteLine("______________________________________");
                Console.WriteLine("Welcome To Simple Notification System!");
                Console.WriteLine("______________________________________\n");

                Console.WriteLine("Please enter what you wish to do.");
                Console.WriteLine("1. Add a User");
                Console.WriteLine("2. Send a Notification to existing User");
                Console.WriteLine("3. Do Crud Operations on User and Notification (Optional Feature)");
                Console.WriteLine("4. Exit");

                int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1:
                        userInteract.CreateUser();
                        break;
                    case 2:
                        notificationInteract.SendNotification();
                        break;
                    case 3:
                        DoCrudOperations();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            new Program().SendNotification();
        }
    }
}
