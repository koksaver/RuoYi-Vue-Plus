using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RuoYi.Common.Cache;
using RuoYi.Common.Core.Exceptions;
using RuoYi.System.Services;
using SqlSugar;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RuoYi-Net API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Secret"] ?? "RuoYiNetDefaultSecretKeyForJwtToken2024";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"] ?? "RuoYi-Net",
            ValidAudience = jwtSettings["Audience"] ?? "RuoYi-Net",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var dbConfig = builder.Configuration.GetSection("Database");
var connectionString = dbConfig["ConnectionString"] ?? "Server=localhost;Database=ruoyi_net;Uid=root;Pwd=root;";
var dbType = dbConfig["DbType"] ?? "MySql";

builder.Services.AddSingleton<ISqlSugarClient>(sp =>
{
    return new SqlSugarClient(new ConnectionConfig
    {
        ConnectionString = connectionString,
        DbType = dbType switch
        {
            "MySql" => DbType.MySql,
            "SqlServer" => DbType.SqlServer,
            "PostgreSQL" => DbType.PostgreSQL,
            "Oracle" => DbType.Oracle,
            _ => DbType.MySql
        },
        IsAutoCloseConnection = true,
        InitKeyType = InitKeyType.Attribute
    });
});

var redisConnection = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
builder.Services.AddSingleton(sp =>
{
    var logger = sp.GetRequiredService<ILogger<CacheService>>();
    return new CacheService(redisConnection, logger);
});

builder.Services.AddScoped<SysUserService>();
builder.Services.AddScoped<SysRoleService>();
builder.Services.AddScoped<SysMenuService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
