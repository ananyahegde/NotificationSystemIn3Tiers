using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.NotificationSenders
{
    public class EmailNotificationSender : INotificationSender
    {
        public void Send(User user, Notification notification)
        {
            Console.WriteLine($"Email sent to {user.Email}: {notification.Message}");
        }
    }
}
