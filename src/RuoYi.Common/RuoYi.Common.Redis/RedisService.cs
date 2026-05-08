using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace RuoYi.Common.Redis
{
    public class RedisService
    {
        private readonly IDatabase _db;
        private readonly ConnectionMultiplexer _redis;

        public RedisService(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _db = _redis.GetDatabase();
        }

        public RedisService(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(value);
            return await _db.StringSetAsync(key, json, expiry);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (value.IsNullOrEmpty)
                return default;

            return System.Text.Json.JsonSerializer.Deserialize<T>(value!);
        }

        public async Task<string?> GetStringAsync(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await _db.StringSetAsync(key, value, expiry);
        }

        public async Task<bool> DeleteAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        public async Task<bool> HasKeyAsync(string key)
        {
            return await _db.KeyExistsAsync(key);
        }

        public async Task<bool> ExpireAsync(string key, TimeSpan expiry)
        {
            return await _db.KeyExpireAsync(key, expiry);
        }

        public async Task<long> IncrementAsync(string key, long value = 1)
        {
            return await _db.StringIncrementAsync(key, value);
        }

        public async Task<long> DecrementAsync(string key, long value = 1)
        {
            return await _db.StringDecrementAsync(key, value);
        }

        public async Task<bool> SetAddAsync<T>(string key, T value)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(value);
            return await _db.SetAddAsync(key, json);
        }

        public async Task<bool> SetRemoveAsync<T>(string key, T value)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(value);
            return await _db.SetRemoveAsync(key, json);
        }

        public async Task<List<T>> SetMembersAsync<T>(string key)
        {
            var values = await _db.SetMembersAsync(key);
            var result = new List<T>();
            foreach (var v in values)
            {
                if (!v.IsNullOrEmpty)
                    result.Add(System.Text.Json.JsonSerializer.Deserialize<T>(v!)!);
            }
            return result;
        }
    }
}