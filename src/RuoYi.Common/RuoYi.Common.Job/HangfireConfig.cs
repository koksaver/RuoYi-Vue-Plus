using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RuoYi.Common.Job
{
    public class HangfireConfig
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddHangfire(config =>
                config.UseSqlServerStorage(connectionString));

            services.AddHangfireServer();
        }

        public static void ConfigureWithRedis(IServiceCollection services, string redisConnectionString)
        {
            services.AddHangfire(config =>
                config.UseRedisStorage(redisConnectionString));

            services.AddHangfireServer();
        }
    }
}