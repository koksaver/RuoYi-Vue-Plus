using System.Security.Cryptography;

namespace RuoYi.Common.Encrypt
{
    public class AESEncryptor
    {
        private static readonly byte[] DefaultKey = "RuoYiNet2024Key!"u8.ToArray();
        private static readonly byte[] DefaultIv = "RuoYiNet2024Iv!!"u8.ToArray();

        public static string Encrypt(string plainText, byte[]? key = null, byte[]? iv = null)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            key ??= DefaultKey;
            iv ??= DefaultIv;

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plainText);
            sw.Flush();
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText, byte[]? key = null, byte[]? iv = null)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            key ??= DefaultKey;
            iv ??= DefaultIv;

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}