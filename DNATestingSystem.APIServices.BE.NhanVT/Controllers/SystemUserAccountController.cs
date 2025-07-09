using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{

    public class SystemUserAccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ISystemUserAccountService _userAccountsService;

        public SystemUserAccountController(IConfiguration config, ISystemUserAccountService userAccountsService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] MyLoginRequest request)
        {
            var user = _userAccountsService.GetUserAccount(request.userName, request.password);

            if (user == null || user.Result == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user.Result);

            return Ok(token);
        }

        private string GenerateJSONWebToken(SystemUserAccount systemUserAccount)
        {
            var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var issuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured.");
            var audience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured.");
            var token = new JwtSecurityToken(issuer
                    , audience
                    , new Claim[]
                    {
                new(ClaimTypes.Name, systemUserAccount.UserName),
                new(ClaimTypes.NameIdentifier, systemUserAccount.UserAccountId.ToString()),
                new("UserId", systemUserAccount.UserAccountId.ToString()),
                //new(ClaimTypes.Email, systemUserAccount.Email),
                new(ClaimTypes.Role, systemUserAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record MyLoginRequest(string userName, string password);
    }
}
