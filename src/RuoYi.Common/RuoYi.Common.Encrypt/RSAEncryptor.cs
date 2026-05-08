using System.Security.Cryptography;

namespace RuoYi.Common.Encrypt
{
    public class RSAEncryptor
    {
        public static (string PublicKey, string PrivateKey) GenerateKeys(int keySize = 2048)
        {
            using var rsa = RSA.Create(keySize);
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            return (publicKey, privateKey);
        }

        public static string Encrypt(string plainText, string publicKey)
        {
            using var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

            var data = System.Text.Encoding.UTF8.GetBytes(plainText);
            var encrypted = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string cipherText, string privateKey)
        {
            using var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

            var data = Convert.FromBase64String(cipherText);
            var decrypted = rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
            return System.Text.Encoding.UTF8.GetString(decrypted);
        }
    }
}