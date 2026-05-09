using Microsoft.Extensions.Configuration;
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

        public static IServiceCollection AddRedisSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Redis:Connection"]
                ?? "localhost:6379,password=,defaultDatabase=0";
            return services.AddRedisService(connectionString);
        }
    }
}