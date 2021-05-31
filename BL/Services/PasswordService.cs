using Core.Enums;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using BL.Services.Interfaces;
using System.Text;

namespace BL.Services
{
    /// <summary>
    /// Service to check emails
    /// </summary>
    public class PasswordService : IPasswordService
    {
        /// <summary>
        /// Method that will return the strength level of given password
        /// </summary>
        public PasswordStrength CheckPasswordStrength(string password)
        {
            int score = 0;
            Dictionary<string, int> patterns = new Dictionary<string, int> { { @"\d", 5 }, //включает цифры
                                                                         { @"[a-zA-Z]", 10 }, //буквы
                                                                         { @"[!,@,#,\$,%,\^,&,\*,?,_,~]", 15 } }; //символы
            if (password.Length > 6)
                foreach (var pattern in patterns)
                    score += Regex.Matches(password, pattern.Key).Count * pattern.Value;

            PasswordStrength result;
            switch (score / 50)
            {
                case 0: result = PasswordStrength.Low; break;
                case 1: result = PasswordStrength.Medium; break;
                case 2: result = PasswordStrength.High; break;
                case 3: result = PasswordStrength.VeryHigh; break;
                default: result = PasswordStrength.Paranoid; break;
            }
            return result;
        }

        /// <summary>
        /// Hashing password
        /// </summary>
        public string GetHashString(string password)
        {
            //convert string in byte array
            byte[] bytes = Encoding.Unicode.GetBytes(password);

            //create object to get hash resources
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //compute hash in bytes  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //create one string from array
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        /// <summary>
        /// Method to check if given password is strong
        /// </summary>
        public bool IsPasswordStrong(string password)
        {
            if (CheckPasswordStrength(password) < PasswordStrength.Medium)
            {
                return false;
            }
            return true;
        }
    }
}
