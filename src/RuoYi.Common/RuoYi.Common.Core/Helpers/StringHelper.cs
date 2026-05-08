using System.Text;
using System.Text.RegularExpressions;

namespace RuoYi.Common.Core.Helpers
{
    public class StringHelper
    {
        public static bool IsEmpty(string? str)
        {
            return string.IsNullOrEmpty(str) || str.Trim().Length == 0;
        }

        public static bool IsNotEmpty(string? str)
        {
            return !IsEmpty(str);
        }

        public static bool IsBlank(string? str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNotBlank(string? str)
        {
            return !IsBlank(str);
        }

        public static string Trim(string? str)
        {
            return str?.Trim() ?? string.Empty;
        }

        public static string Substring(string str, int start, int length)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            if (start >= str.Length)
                return string.Empty;

            if (start + length > str.Length)
                length = str.Length - start;

            return str.Substring(start, length);
        }

        public static string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var parts = str.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            sb.Append(parts[0].ToLower());

            for (int i = 1; i < parts.Length; i++)
            {
                if (parts[i].Length > 0)
                {
                    sb.Append(char.ToUpper(parts[i][0]));
                    if (parts[i].Length > 1)
                        sb.Append(parts[i].Substring(1).ToLower());
                }
            }

            return sb.ToString();
        }

        public static string ToPascalCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var camelCase = ToCamelCase(str);
            return char.ToUpper(camelCase[0]) + camelCase.Substring(1);
        }

        public static string Format(string template, params object[] args)
        {
            return string.Format(template, args);
        }

        public static bool Matches(string pattern, string input)
        {
            return Regex.IsMatch(input, pattern);
        }
    }
}