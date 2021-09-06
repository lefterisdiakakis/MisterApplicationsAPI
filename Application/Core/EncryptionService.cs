using System.Security.Cryptography;
using System.Text;
using RtelEncryptionLibrary;

namespace Application.Core
{
    public class EncryptionService
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string EncryptString(string value)
        {
            var x = new RtelEncryption();
            x.EncryptString(ref value);
            return value;
        }

        public static string DencryptString(string value)
        {
            var x = new RtelEncryption();
            x.DecryptString(ref value);
            return value;
        }
    }
}
