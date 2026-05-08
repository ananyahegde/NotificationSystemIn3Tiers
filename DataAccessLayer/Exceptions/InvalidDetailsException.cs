// Handles -
// 1. Message should not be empty.
// 2. Message length should be at least 5 characters.
// 3. Email notification should be sent only if the user has a valid email.
// 4. SMS notification should be sent only if the user has a valid phone number.
// 5. SMS message should not exceed 160 characters.

namespace DataAccessLayer.Exceptions
{
    public class InvalidDetailsExceptions : Exception
    {
        public InvalidDetailsExceptions(string message) : base(message) { }
    }
}
