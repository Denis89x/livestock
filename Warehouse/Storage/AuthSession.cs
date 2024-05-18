using System.Security.Cryptography;
using System.Text;

namespace Warehouse.Storage
{
    internal class AuthSession
    {
        public static string currentUsername;

        public static string getSHA256Hash(string input)
        {
            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
