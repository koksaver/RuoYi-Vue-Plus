using RuoYi.Common.Core;
using RuoYi.Common.Mybatis;

namespace RuoYi.Common.Tenant
{
    public class TenantService
    {
        private readonly TenantConfig _config;

        public TenantService(TenantConfig config)
        {
            _config = config;
        }

        public string? GetCurrentTenantId()
        {
            return _config.DefaultTenantId;
        }

        public bool IsTenantEnabled()
        {
            return _config.Enabled;
        }
    }
}