using DataAccessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface INotificationInteract
    {
        public void CreateNotification();
        public List<Notification>? ReadAllNotifications();
        public void ReadNotification();
        public void UpdateNotification();
        public void DeleteNotification();
        public void SendNotification();
    }
}
