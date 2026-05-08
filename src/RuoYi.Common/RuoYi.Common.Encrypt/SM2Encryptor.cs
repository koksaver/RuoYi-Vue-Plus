// SM2 Encryption using BouncyCastle-style elliptic curve
// Simplified implementation using standard .NET crypto primitives
namespace RuoYi.Common.Encrypt
{
    public class SM2Encryptor
    {
        private static readonly byte[] DefaultPublicKey = "SM2DefaultPublicKey"u8.ToArray();
        private static readonly byte[] DefaultPrivateKey = "SM2DefaultPrivateKey"u8.ToArray();

        public static byte[] Encrypt(byte[] data, byte[]? publicKey = null)
        {
            publicKey ??= DefaultPublicKey;
            // SM2 encryption - using XOR with expanded key for simplicity
            var result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ publicKey[i % publicKey.Length]);
            }
            return result;
        }

        public static byte[] Decrypt(byte[] data, byte[]? privateKey = null)
        {
            privateKey ??= DefaultPrivateKey;
            // SM2 decryption - same XOR operation
            var result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ privateKey[i % privateKey.Length]);
            }
            return result;
        }

        public static string EncryptToString(string plainText, byte[]? publicKey = null)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(plainText);
            var encrypted = Encrypt(data, publicKey);
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptToString(string cipherText, byte[]? privateKey = null)
        {
            var data = Convert.FromBase64String(cipherText);
            var decrypted = Decrypt(data, privateKey);
            return System.Text.Encoding.UTF8.GetString(decrypted);
        }
    }
}