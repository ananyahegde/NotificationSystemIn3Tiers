using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Database;

namespace DataAccessLayer.Repositories
{
    public class NotificationRepository : IRepository<Notification, string>
    {
        Context context;

        public NotificationRepository()
        {
            context = new Context();
        }

        public Notification Create(Notification notification)
        {
            var id = Guid.NewGuid().ToString();
            notification.MessageId = id;

            context.Add(notification);
            context.SaveChanges();

            return notification;
        }

        public List<Notification>? ReadAll()
        {
            return context.Set<Notification>().ToList();
        }

        public Notification? Read(string key)
        {
            Notification notification = context.notifications.Find(key);
            return notification;
        }

        public Notification? Update(Notification notification, string key)
        {
            var getNotification = Read(key);
            if (getNotification == null)
                throw new Exception("No users found.");

            getNotification.Message = notification.Message;

            context.SaveChanges();
            return notification;
        }

        public Notification? Delete(string key)
        {
            var getNotification = Read(key);
            if (getNotification == null)
                throw new Exception("No notification found.");
            context.Remove(getNotification);
            context.SaveChanges();
            return getNotification;
        }
    }
}
