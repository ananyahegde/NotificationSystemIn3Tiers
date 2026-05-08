namespace DataAccessLayer.Models
{
    public enum NotifType
    {
        EmailNotificationSender = 1,
        SmsNotificationSender = 2
    }

    public class Notification : IComparable<Notification>
    {
        public string MessageId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime SentDate { get; set; }
        public NotifType NotifType { get; set; }

        public Notification() { }

        public Notification(string messageId, string message, DateTime sentDate)
        {
            this.MessageId = messageId;
            this.Message = message;
            this.SentDate = sentDate;
        }

        public override string ToString()
        {
            return $"Message Id: {MessageId}" +
            $"message: {Message}" +
            $"sent date: {SentDate}";
        }

        public int CompareTo(Notification? other)
        {
            return this.MessageId.CompareTo(other.MessageId);
        }
    }
}
