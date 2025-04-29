using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _4Time.DataCore
{
    internal static class Crypto
    {
        static string key = Connector.GetCurrentUser().Item1;
        internal static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, 32);
            aes.Key = keyBytes;
            aes.IV = new byte[16];
            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        internal static string Decrypt(string cipherText)
        {
            string userName = Environment.UserName;
            string[] userNameSplitted = userName.Split(".");
            string key2 = $"{userNameSplitted[0]}D209135{userNameSplitted[1]}";

            try
            {
                using var aes = Aes.Create();
                var keyBytes = Encoding.UTF8.GetBytes(key2);
                Array.Resize(ref keyBytes, 32);
                aes.Key = keyBytes;
                aes.IV = new byte[16];
                using var decryptor = aes.CreateDecryptor();
                var cipherBytes = Convert.FromBase64String(cipherText);
                var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                return Encoding.UTF8.GetString(plainBytes);

            }
            catch
            {
                using var aes = Aes.Create();
                var keyBytes = Encoding.UTF8.GetBytes(key);
                Array.Resize(ref keyBytes, 32);
                aes.Key = keyBytes;
                aes.IV = new byte[16];
                using var decryptor = aes.CreateDecryptor();
                var cipherBytes = Convert.FromBase64String(cipherText);
                var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                return Encoding.UTF8.GetString(plainBytes);
            }
            
        }
    }
}
