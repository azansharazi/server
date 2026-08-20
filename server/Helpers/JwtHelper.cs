using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace server.Helpers
{
    public  class JwtHelper
    {
        private readonly JwtSettings _jwtsettings;
        public JwtHelper(IOptions<JwtSettings> jwtsettings)
        {
            _jwtsettings = jwtsettings.Value;
        }
        public  string GenerateToken(User user, List<string> roles)
        {
            
         var Key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(_jwtsettings.SecretKey));
            var credenrials = new SigningCredentials (Key,SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            foreach ( string role in roles) { 
                claims.Add(new Claim(ClaimTypes.Role, role));
                
            }
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials : credenrials,
                expires:DateTime.UtcNow.AddMinutes(_jwtsettings.ExpiryMinutes),
                issuer:_jwtsettings.Issuer,
                audience:_jwtsettings.Audience

                
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
