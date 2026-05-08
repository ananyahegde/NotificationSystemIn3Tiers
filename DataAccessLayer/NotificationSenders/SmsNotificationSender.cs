using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.NotificationSenders
{
    public class SmsNotificationSender : INotificationSender
    {
        public void Send(User user, Notification notification)
        {
            Console.WriteLine($"SMS sent to {user.Phone}: {notification.Message}");
        }
    }
}
