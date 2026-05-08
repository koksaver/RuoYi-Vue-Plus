namespace RuoYi.Common.Security
{
    public class LoginUser
    {
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserType { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public long? DeptId { get; set; }
        public List<string>? RoleKeys { get; set; }
        public List<string>? Permissions { get; set; }
        public List<long>? RoleIds { get; set; }
        public DateTime? LoginTime { get; set; }
        public string? LoginIp { get; set; }
        public string? Token { get; set; }

        public bool IsAdmin()
        {
            return RoleKeys != null && RoleKeys.Contains("admin");
        }
    }
}