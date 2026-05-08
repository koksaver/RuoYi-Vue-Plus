using Microsoft.Extensions.Logging;
using RuoYi.Common.Core;
using RuoYi.Common.Mybatis;
using RuoYi.Common.Security;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysUserService
    {
        private readonly ISqlSugarClient _db;
        private readonly ILogger<SysUserService> _logger;

        public SysUserService(ISqlSugarClient db, ILogger<SysUserService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<PageResult<SysUser>> GetPageAsync(SysUser user, int pageNum, int pageSize)
        {
            var query = _db.Queryable<SysUser>()
                .Where(u => u.DelFlag == "0")
                .WhereIF(!string.IsNullOrEmpty(user.UserName), u => u.UserName!.Contains(user.UserName!))
                .WhereIF(!string.IsNullOrEmpty(user.PhoneNumber), u => u.PhoneNumber!.Contains(user.PhoneNumber!))
                .WhereIF(!string.IsNullOrEmpty(user.Status), u => u.Status == user.Status)
                .WhereIF(!string.IsNullOrEmpty(user.CreateBy), u => u.CreateBy == user.CreateBy)
                .OrderBy(u => u.UserId, OrderByType.Desc);

            var total = await query.Clone().CountAsync();
            var rows = await query.Clone().Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PageResult<SysUser>(rows, total);
        }

        public async Task<SysUser?> GetByIdAsync(long userId)
        {
            return await _db.Queryable<SysUser>()
                .Where(u => u.UserId == userId)
                .FirstAsync();
        }

        public async Task<SysUser?> GetByUserNameAsync(string userName)
        {
            return await _db.Queryable<SysUser>()
                .Where(u => u.UserName == userName && u.DelFlag == "0")
                .FirstAsync();
        }

        public async Task<int> AddAsync(SysUser user)
        {
            user.Password = PasswordEncoder.Encode(user.Password ?? UserConstants.DEFAULT_PASSWORD);
            user.CreateTime = DateTime.Now;
            user.DelFlag = "0";
            return await _db.Insertable(user).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(SysUser user)
        {
            user.UpdateTime = DateTime.Now;
            return await _db.Updateable(user).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(long[] userIds)
        {
            var users = await _db.Queryable<SysUser>()
                .Where(u => userIds.Contains(u.UserId))
                .ToListAsync();

            foreach (var user in users)
            {
                user.DelFlag = "2";
                user.UpdateTime = DateTime.Now;
            }

            return await _db.Updateable(users).ExecuteCommandAsync();
        }

        public async Task<bool> ResetPasswordAsync(long userId, string newPassword)
        {
            var user = await GetByIdAsync(userId);
            if (user == null) return false;

            user.Password = PasswordEncoder.Encode(newPassword);
            user.UpdateTime = DateTime.Now;
            await _db.Updateable(user).UpdateColumns(u => new { u.Password, u.UpdateTime }).ExecuteCommandAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(long userId, string status)
        {
            var user = await GetByIdAsync(userId);
            if (user == null) return false;

            user.Status = status;
            user.UpdateTime = DateTime.Now;
            return await _db.Updateable(user).UpdateColumns(u => new { u.Status, u.UpdateTime }).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> CheckUserNameUniqueAsync(string userName)
        {
            var count = await _db.Queryable<SysUser>()
                .Where(u => u.UserName == userName && u.DelFlag == "0")
                .CountAsync();
            return count == 0;
        }
    }
}