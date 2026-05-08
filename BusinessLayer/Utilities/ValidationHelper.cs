using System.Net.Mail;

namespace BusinessLayer.Utilities
{
    public class ValidatorHelper
    {
        // validate email
        public bool IsValid(string? email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
