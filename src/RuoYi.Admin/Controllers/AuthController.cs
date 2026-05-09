using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.Common.Redis;
using RuoYi.Common.Security;
using RuoYi.System.Models;
using RuoYi.System.Services;

namespace RuoYi.Admin.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly JwtService _jwtService;
        private readonly SysUserService _userService;
        private readonly RedisService _redisService;

        public AuthController(JwtService jwtService, SysUserService userService, RedisService redisService)
        {
            _jwtService = jwtService;
            _userService = userService;
            _redisService = redisService;
        }

        [HttpPost("login")]
        public async Task<AjaxResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return Error("用户名或密码不能为空");
            }

            var user = await _userService.GetByUserNameAsync(request.Username);
            if (user == null)
            {
                return Error("用户不存在");
            }

            if (user.Status == UserConstants.DISABLE)
            {
                return Error("用户已停用，请联系管理员");
            }

            if (!PasswordEncoder.Matches(request.Password, user.Password ?? string.Empty))
            {
                return Error("用户名或密码错误");
            }

            var loginUser = new LoginUser
            {
                UserId = user.UserId,
                UserName = user.UserName,
                NickName = user.NickName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
                DeptId = user.DeptId,
                LoginTime = DateTime.Now,
                RoleKeys = new List<string> { "admin" }
            };

            var token = _jwtService.GenerateToken(loginUser);
            loginUser.Token = token;

            // Cache login user
            await _redisService.SetStringAsync($"login:user:{user.UserId}", token, TimeSpan.FromHours(24));

            return Success(new { token, user = loginUser });
        }

        [HttpPost("logout")]
        public async Task<AjaxResult> Logout()
        {
            return Success("退出成功");
        }

        [HttpGet("info")]
        public async Task<AjaxResult> GetInfo()
        {
            return Success(new
            {
                roles = new[] { "admin" },
                permissions = new[] { "*:*:*" },
                user = new { }
            });
        }
    }

    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Code { get; set; }
        public string? Uuid { get; set; }
    }
}