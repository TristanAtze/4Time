using System.Security.Cryptography;
using System.Text;

namespace _4Time.DataCore;

internal static class Crypto
{
    private static readonly string Key = $"{Connector.GetCurrentUser().Item1}D209135{Connector.GetCurrentUser().Item2}";
    internal static string Encrypt(string plainText)
    {
        var aes = Aes.Create();
        var keyBytes = Encoding.UTF8.GetBytes(Key);
        Array.Resize(ref keyBytes, 32);
        aes.Key = keyBytes;
        aes.IV = new byte[16];
        var encryptor = aes.CreateEncryptor();
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
            var aes = Aes.Create();
            var keyBytes = Encoding.UTF8.GetBytes(key2);
            Array.Resize(ref keyBytes, 32);
            aes.Key = keyBytes;
            aes.IV = new byte[16];
            var decryptor = aes.CreateDecryptor();
            var cipherBytes = Convert.FromBase64String(cipherText);
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(plainBytes);

        }
        catch
        {
            var aes = Aes.Create();
            var keyBytes = Encoding.UTF8.GetBytes(Key);
            Array.Resize(ref keyBytes, 32);
            aes.Key = keyBytes;
            aes.IV = new byte[16];
            var decryptor = aes.CreateDecryptor();
            var cipherBytes = Convert.FromBase64String(cipherText);
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}