using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Hashes a secure string (EG: Password hashing purposes) using SHA1 BASE64.
        /// </summary>
        public static string Hash(this string clearString)
        {
            UnicodeEncoding Ue = new UnicodeEncoding();

            byte[] ByteSourceText = Ue.GetBytes(clearString);
            System.Security.Cryptography.SHA1CryptoServiceProvider SHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] ByteHash = SHA1.ComputeHash(ByteSourceText);

            return Convert.ToBase64String(ByteHash);
        }
    }
}
