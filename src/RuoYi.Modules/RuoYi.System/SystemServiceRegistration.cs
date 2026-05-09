namespace RuoYi.System
{
    public static class SystemServiceRegistration
    {
        public static IServiceCollection AddSystemServices(this IServiceCollection services)
        {
            services.AddScoped<Services.SysUserService>();
            services.AddScoped<Services.SysRoleService>();
            services.AddScoped<Services.SysMenuService>();
            services.AddScoped<Services.SysDeptService>();
            services.AddScoped<Services.SysDictTypeService>();
            services.AddScoped<Services.SysDictDataService>();
            services.AddScoped<Services.SysConfigService>();
            services.AddScoped<Services.SysNoticeService>();
            services.AddScoped<Services.SysOperLogService>();
            services.AddScoped<Services.SysLoginInfoService>();
            return services;
        }
    }
}