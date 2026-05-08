using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class NotificationRepository : IRepository<Notification, string>
    {
        Dictionary<string, Notification> _notifications = new Dictionary<string, Notification>();

        public Notification Create(Notification user)
        {
            var id = Guid.NewGuid().ToString();
            user.MessageId = id;
            _notifications[id] = user;
            return _notifications[id];
        }

        public List<Notification>? ReadAll()
        {
            if (_notifications.Count == 0)
                return null;

            List<Notification> users = _notifications.Values.ToList();
            users.Sort();
            return users;
        }

        public Notification? Read(string key)
        {
            if (!_notifications.ContainsKey(key))
                return null;
            return _notifications[key];
        }

        public Notification? Update(Notification user, string key)
        {
            if (!_notifications.ContainsKey(key))
                return null;
            _notifications[key] = user;
            return _notifications[key];
        }

        public Notification? Delete(string key)
        {
            if (!_notifications.ContainsKey(key))
                return null;
            Notification item = _notifications[key];
            _notifications.Remove(key);
            return item;
        }
    }
}
