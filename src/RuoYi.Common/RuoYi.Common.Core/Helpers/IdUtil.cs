namespace RuoYi.Common.Core.Helpers
{
    public class IdUtil
    {
        private static readonly long Epoch = 1640966400000L; // 2022-01-01
        private static readonly object _lock = new();
        private static long _lastTimestamp = -1L;
        private static long _sequence = 0L;
        private static readonly long WorkerId = 1L;
        private static readonly long DatacenterId = 1L;

        private static long _currentSequence = 0;

        public static long NextId()
        {
            lock (_lock)
            {
                var timestamp = GetTimestamp();

                if (timestamp < _lastTimestamp)
                {
                    throw new Exception($"Clock moved backwards. Refusing to generate id for {_lastTimestamp - timestamp} milliseconds");
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & 4095;
                    if (_sequence == 0)
                    {
                        timestamp = WaitNextMillis();
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;

                return ((timestamp - Epoch) << 22)
                       | (DatacenterId << 17)
                       | (WorkerId << 12)
                       | _sequence;
            }
        }

        public static string NextIdStr()
        {
            return NextId().ToString();
        }

        private static long GetTimestamp()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }

        private static long WaitNextMillis()
        {
            var timestamp = GetTimestamp();
            while (timestamp <= _lastTimestamp)
            {
                timestamp = GetTimestamp();
            }
            return timestamp;
        }
    }
}