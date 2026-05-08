namespace RuoYi.Common.Tenant
{
    public class TenantConfig
    {
        public bool Enabled { get; set; } = false;
        public string? DefaultTenantId { get; set; }
        public List<string> IgnoreTables { get; set; } = new()
        {
            "sys_config",
            "sys_dict_data",
            "sys_dict_type"
        };
    }
}