using StackExchange.Redis;

namespace RuoYi.Common.Redis
{
    public class RedisLock
    {
        private readonly IDatabase _db;
        private static readonly Random _random = new();

        public RedisLock(IDatabase db)
        {
            _db = db;
        }

        public async Task<bool> TryLockAsync(string lockKey, string lockValue, TimeSpan expiry)
        {
            return await _db.StringSetAsync(lockKey, lockValue, expiry, When.NotExists);
        }

        public async Task<bool> ReleaseLockAsync(string lockKey, string lockValue)
        {
            var script = @"
                if redis.call('get', KEYS[1]) == ARGV[1] then
                    return redis.call('del', KEYS[1])
                else
                    return 0
                end";

            var result = await _db.ScriptEvaluateAsync(script, new RedisKey[] { lockKey }, new RedisValue[] { lockValue });
            return (long)result == 1;
        }

        public async Task<bool> LockAsync(string lockKey, string lockValue, TimeSpan expiry, int retryCount = 3, int retryDelayMs = 200)
        {
            for (int i = 0; i < retryCount; i++)
            {
                if (await TryLockAsync(lockKey, lockValue, expiry))
                {
                    return true;
                }

                if (i < retryCount - 1)
                {
                    await Task.Delay(retryDelayMs + _random.Next(50));
                }
            }

            return false;
        }
    }
}