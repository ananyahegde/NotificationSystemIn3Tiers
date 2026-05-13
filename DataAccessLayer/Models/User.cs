namespace DataAccessLayer.Models

{
    public class User : IComparable<User>
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public ICollection<Notification> Notifications { get; set; }

        public User() { }

        public User(string userId, string name, string email, string phone)
        {
            this.UserId = userId;
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
        }

        public override string ToString()
        {
            return $"UserId: {UserId}" +
                   $"Name: {Name}" +
                   $"Email: {Email}" +
                   $"Phone: {Phone}";
        }

        public int CompareTo(User? other)
        {
            return this.UserId.CompareTo(other.UserId);
        }
    }
}
