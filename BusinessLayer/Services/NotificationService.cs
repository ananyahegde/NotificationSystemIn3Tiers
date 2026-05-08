using System.Text.RegularExpressions;
using BusinessLayer.Interfaces;
using BusinessLayer.Utilities;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.NotificationSenders;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Exceptions;

namespace BusinessLayer.Services
{
    public class NotificationService : INotificationInteract
    {
        ValidatorHelper helper = new ValidatorHelper();
        private NotificationRepository _repo = new NotificationRepository();

        private (Notification, INotificationSender, int) TakeNotificationDetails()
        {
            int typeChoice;

            Console.Write("\nPlease select the type of notification. 1 for Email, 2 for SMS: ");
            while (!int.TryParse(Console.ReadLine(), out typeChoice) || typeChoice < 1 || typeChoice > 2)
                Console.WriteLine("Invalid entry. Please try again.");
            INotificationSender sender = typeChoice == 1 ? new EmailNotificationSender() : new SmsNotificationSender();

            Notification notification = new Notification();
            // validate message
            while (true)
            {
                try
                {
                    Console.Write("\nPlease enter the message: ");
                    string message = Console.ReadLine() ?? "";
                    if (message.Length == 0)
                        throw new InvalidDetailsExceptions("Message cannot be empty.");
                    if (message.Length < 5)
                        throw new InvalidDetailsExceptions("Message should contain atleast 5 characters.");
                    if (message.Length > 160)
                        throw new InvalidDetailsExceptions("Message cannot exceed 160 characters.");
                    notification.Message = message;
                    break;
                }
                catch (InvalidDetailsExceptions ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            notification.SentDate = DateTime.Today;

            return (notification, sender, typeChoice);
        }


        private void SendNotificationToUser(Notification notification, INotificationSender sender, int type)
        {
            string email = "";
            string phone = "";
            string details = type == 1 ? "Email" : "Phone Number";

            Console.Write($"\nEnter the {details} of the user: ");
            // check for valid email/phone
            if (type == 1)
            {
                while (true)
                {
                    try
                    {
                        email = Console.ReadLine() ?? "";
                        if (!helper.IsValid(email))
                            throw new InvalidDetailsExceptions("Email is not valid");
                        break;
                    }
                    catch (InvalidDetailsExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                while (true)
                {
                    try
                    {
                        Regex regex = new Regex(@"^(0|\+91)?[789]\d{9}$");
                        phone = Console.ReadLine() ?? "";
                        Match match = regex.Match(phone);
                        if (match.Success)
                        {
                            break;
                        }
                        else
                            throw new InvalidDetailsExceptions("Phone number is not valid.");
                    }
                    catch (InvalidDetailsExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            List<User>? users = new UserService().ReadAllUsers();
            if (users == null)
            {
                return;
            }

            User? user = null;
            // find user by email/phone number
            if (type == 1)
                user = users.Where(u => u.Email == email).FirstOrDefault();
            else
                user = users.Where(u => u.Phone == phone).FirstOrDefault();
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine();
            sender.Send(user, notification);
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Phone: {user.Phone}");
            Console.WriteLine($"Message: {notification.Message}");
            Console.WriteLine($"Sent Date: {notification.SentDate}");
            Console.WriteLine($"Sent Via: {notification.NotifType}");
            Console.WriteLine("-----------------------------\n");
        }

        public void SendNotification()
        {
            var details = TakeNotificationDetails();
            SendNotificationToUser(details.Item1, details.Item2, details.Item3);
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
