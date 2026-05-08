using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface INotificationSender
    {
        public void Send(User user, Notification notification);
    }
}
