using System.Security.Cryptography;

namespace RuoYi.Common.Encrypt
{
    public class SM4Encryptor
    {
        private static readonly byte[] DefaultKey = "RuoYiSM4Key2024!"u8.ToArray();

        public static string Encrypt(string plainText, byte[]? key = null)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            key ??= DefaultKey;

            using var aes = Aes.Create();
            aes.Key = Create256Key(key);
            aes.IV = new byte[16];
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plainText);
            sw.Flush();
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText, byte[]? key = null)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            key ??= DefaultKey;

            using var aes = Aes.Create();
            aes.Key = Create256Key(key);
            aes.IV = new byte[16];
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }

        private static byte[] Create256Key(byte[] key)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(key);
        }
    }
}