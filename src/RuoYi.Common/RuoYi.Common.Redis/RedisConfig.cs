using Microsoft.Extensions.DependencyInjection;

namespace RuoYi.Common.Redis
{
    public static class RedisConfig
    {
        public static IServiceCollection AddRedisService(this IServiceCollection services, string connectionString)
        {
            var redis = StackExchange.Redis.ConnectionMultiplexer.Connect(connectionString);
            services.AddSingleton(redis);
            services.AddSingleton(sp =>
            {
                var conn = sp.GetRequiredService<StackExchange.Redis.ConnectionMultiplexer>();
                return new RedisService(conn);
            });
            services.AddSingleton(sp =>
            {
                var conn = sp.GetRequiredService<StackExchange.Redis.ConnectionMultiplexer>();
                return new RedisLock(conn.GetDatabase());
            });
            return services;
        }
    }
}