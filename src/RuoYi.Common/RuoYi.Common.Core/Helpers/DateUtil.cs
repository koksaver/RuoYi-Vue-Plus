namespace RuoYi.Common.Core.Helpers
{
    public class DateUtil
    {
        public static DateTime Now()
        {
            return DateTime.Now;
        }

        public static DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }

        public static string ToString(DateTime? date, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return date?.ToString(format) ?? string.Empty;
        }

        public static string ToDateString(DateTime? date)
        {
            return date?.ToString("yyyy-MM-dd") ?? string.Empty;
        }

        public static string ToTimeString(DateTime? date)
        {
            return date?.ToString("HH:mm:ss") ?? string.Empty;
        }

        public static DateTime? Parse(string dateStr)
        {
            if (DateTime.TryParse(dateStr, out var result))
                return result;
            return null;
        }

        public static long GetTimestamp()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }

        public static long GetTimestampMilliseconds()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }

        public static int GetAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
                age--;
            return age;
        }

        public static TimeSpan Diff(DateTime start, DateTime end)
        {
            return end - start;
        }
    }
}