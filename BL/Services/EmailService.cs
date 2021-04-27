using BL.Services.Interfaces;
using System.Text.RegularExpressions;

namespace BL.Services
{
    public class EmailService : IEmailService
    {
        public bool ValideEmail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
