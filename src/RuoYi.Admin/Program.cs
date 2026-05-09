using Serilog;
using RuoYi.Common.Doc;
using RuoYi.Common.Mybatis;
using RuoYi.Common.Redis;
using RuoYi.Common.Security;
using RuoYi.Common.Web;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    // 注册服务
    builder.Services.AddControllers();
    builder.Services.AddSwaggerConfig();
    builder.Services.AddJwtAuth(builder.Configuration);
    builder.Services.AddSqlSugarSetup(builder.Configuration);
    builder.Services.AddRedisSetup(builder.Configuration);
    builder.Services.AddCorsSetup();
    builder.Services.AddEndpointsApiExplorer();

    // 注册模块服务
    builder.Services.AddSystemServices();

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.UseCorsSetup();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseGlobalExceptionHandler();
    app.MapControllers();

    Log.Information("RuoYi.NET 启动成功");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "应用程序启动失败");
}
finally
{
    Log.CloseAndFlush();
}