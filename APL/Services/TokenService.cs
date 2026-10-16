using APL.DTOs;
using APL.Interfaces;
using APL.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<AppSettings> _appsettings;
        private readonly IUserService _userService;

        public TokenService(IOptions<AppSettings> settings, IUserService userService)
        {   
            _appsettings = settings;
            _userService = userService;
        }

        public async Task<string> GenerateToken(UserDTO user)
        {
            var roles = await _userService.GetUserRoleAsync(user);
            var signInKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_appsettings.Value.JWTSecret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim("UserID", user.Id.ToString()),
                    new Claim("UserName", user.Name),
                    new Claim(ClaimTypes.Role, roles)
                ]),
                //Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    signInKey,
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
