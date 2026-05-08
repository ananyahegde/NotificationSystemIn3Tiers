using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.NotificationSenders;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class NotificationService : INotificationInteract
    {

        private NotificationRepository _repo = new NotificationRepository();

        private (Notification, INotificationSender) TakeNotificationDetails()
        {
            int typeChoice;

            Console.Write("\nPlease select the type of notification. 1 for Email, 2 for SMS: ");
            while (!int.TryParse(Console.ReadLine(), out typeChoice) || typeChoice < 1 || typeChoice > 2)
                Console.WriteLine("Invalid entry. Please try again.");

            Notification notification = new Notification();

            Console.Write("\nPlease enter the message: ");
            notification.Message = Console.ReadLine() ?? "";
            notification.SentDate = DateTime.Today;

            INotificationSender sender = typeChoice == 1 ? new EmailNotificationSender() : new SmsNotificationSender();
            return (notification, sender);
        }

        public void SendNotification()
        {
            var details = TakeNotificationDetails();
            SendNotificationToUser(details.Item1);
        }

        private void SendNotificationToUser(Notification notification)
        {
            Console.Write("\nEnter the id of the user to send notification to: ");

            string name = Console.ReadLine() ?? "";
            List<User> users = new UserService().ReadAllUsers();

            if (users == null)
            {
                Console.WriteLine("No users found.");
                return;
            }

            User? user = null;

            foreach (var u in users)
            {
                if (u.Name == name)
                {
                    user = u;
                    break;
                }
            }

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Phone: {user.Phone}");
            Console.WriteLine($"Message: {notification.Message}");
            Console.WriteLine($"Sent Date: {notification.SentDate}");
            Console.WriteLine($"Sent Via: {notification.NotifType}");
            Console.WriteLine("-----------------------------");
        }


        public void NotificationMenu()
        {
            while (true)
            {
                Console.WriteLine("\n Please enter what you wish to do.");
                Console.WriteLine("1. Create (Send) Notification");
                Console.WriteLine("2. Get Notification");
                Console.WriteLine("3. Get All Notifications");
                Console.WriteLine("4. Update Notification");
                Console.WriteLine("5. Delete Notification");
                Console.WriteLine("6. Go Back");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1: CreateNotification(); break;
                    case 2: ReadNotification(); break;
                    case 3: ReadAllNotifications(); break;
                    case 4: UpdateNotification(); break;
                    case 5: DeleteNotification(); break;
                    case 6: return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        public void CreateNotification()
        {
            var details = TakeNotificationDetails();
            Notification createdNotification = _repo.Create(details.Item1);
            Console.WriteLine($"\nNotification Created.\nNotificationId: {createdNotification.MessageId}\nMessage: {createdNotification.Message}\nDate: {createdNotification.SentDate}\nType: {createdNotification.NotifType}");
        }


        public List<Notification>? ReadAllNotifications()
        {
            List<Notification>? notifications = _repo.ReadAll();
            if (notifications == null)
            {
                Console.WriteLine("No notifications found.");
                return null;
            }
            foreach (var n in notifications)
                Console.WriteLine($"\nNotificationId: {n.MessageId}\nMessage: {n.Message}\nDate: {n.SentDate}\nType: {n.NotifType}\n");
            return notifications;
        }

        public void ReadNotification()
        {
            Console.Write("\nEnter the NotificationId: ");
            string id = Console.ReadLine() ?? "";
            Notification? notification = _repo.Read(id);
            if (notification == null)
            {
                Console.WriteLine("Notification not found.");
                return;
            }
            Console.WriteLine($"\nNotificationId: {notification.MessageId}\nMessage: {notification.Message}\nDate: {notification.SentDate}\nType: {notification.NotifType}");
        }

        public void UpdateNotification()
        {
            Console.Write("\nEnter the NotificationId to update: ");
            string id = Console.ReadLine() ?? "";
            var details = TakeNotificationDetails();
            Notification? updatedNotification = _repo.Update(details.Item1, id);
            if (updatedNotification == null)
            {
                Console.WriteLine("Notification not found.");
                return;
            }
            Console.WriteLine($"\nNotification Updated.\nNotificationId: {updatedNotification.MessageId}\nMessage: {updatedNotification.Message}\nDate: {updatedNotification.SentDate}\nType: {updatedNotification.NotifType}");
        }

        public void DeleteNotification()
        {
            Console.Write("\nEnter the NotificationId to delete: ");
            string id = Console.ReadLine() ?? "";
            Notification? deletedNotification = _repo.Delete(id);
            if (deletedNotification == null)
            {
                Console.WriteLine("Notification not found.");
                return;
            }
            Console.WriteLine($"\nNotification Deleted.\nNotificationId: {deletedNotification.MessageId}\nMessage: {deletedNotification.Message}\nDate: {deletedNotification.SentDate}\nType: {deletedNotification.NotifType}");
        }

    }
}
