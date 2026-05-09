using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace RuoYi.Common.Mybatis
{
    public static class SqlSugarSetup
    {
        public static void AddSqlSugar(this IServiceCollection services, string connectionString, DbType dbType = DbType.MySql)
        {
            services.AddSingleton<ISqlSugarClient>(sp =>
            {
                var db = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = connectionString,
                    DbType = (SqlSugar.DbType)dbType,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute,
                    ConfigureExternalServices = new ConfigureExternalServices
                    {
                    }
                });

                db.Aop.OnLogExecuting = (sql, parameters) =>
                {
                    Console.WriteLine($"SQL: {sql}");
                };

                return db;
            });
        }

        public static IServiceCollection AddSqlSugarSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? configuration["ConnectionStrings:DefaultConnection"]
                ?? "Server=localhost;Database=ruoyi;User=root;Password=root;Charset=utf8mb4";
            services.AddSqlSugar(connectionString, DbType.MySql);
            return services;
        }
    }
}