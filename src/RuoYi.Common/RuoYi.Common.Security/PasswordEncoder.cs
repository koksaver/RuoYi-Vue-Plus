using System.Security.Cryptography;
using System.Text;

namespace RuoYi.Common.Security
{
    public class PasswordEncoder
    {
        private static readonly char[] HexDigits = "0123456789abcdef".ToCharArray();

        public static string Encode(string rawPassword)
        {
            if (string.IsNullOrEmpty(rawPassword))
                return string.Empty;

            var salt = GenerateSalt();
            return EncodeWithSalt(rawPassword, salt);
        }

        public static bool Matches(string rawPassword, string encodedPassword)
        {
            if (string.IsNullOrEmpty(rawPassword) || string.IsNullOrEmpty(encodedPassword))
                return false;

            if (encodedPassword.Length < 16)
                return false;

            var salt = encodedPassword[..16];
            var expected = EncodeWithSalt(rawPassword, salt);
            return encodedPassword == expected;
        }

        private static string EncodeWithSalt(string password, string salt)
        {
            var combined = salt + password;
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hash = SHA256.HashData(bytes);
            var hashHex = BytesToHex(hash);
            return salt + hashHex;
        }

        private static string GenerateSalt()
        {
            var saltBytes = new byte[8];
            RandomNumberGenerator.Fill(saltBytes);
            return BytesToHex(saltBytes);
        }

        private static string BytesToHex(byte[] bytes)
        {
            var hex = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                hex[i * 2] = HexDigits[bytes[i] >> 4];
                hex[i * 2 + 1] = HexDigits[bytes[i] & 0x0f];
            }
            return new string(hex);
        }
    }
}