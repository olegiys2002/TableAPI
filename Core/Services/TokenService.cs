using Core.IServices;
using Core.Models;
using Core.Models.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettingsOptions _jwtSettings;
        public TokenService(IOptions<JwtSettingsOptions> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public string CreateToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            string key = _jwtSettings.SecretKey;
            var encodingKey = Encoding.UTF8.GetBytes(key);
            var secret = new SymmetricSecurityKey(encodingKey);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            string validIssuer = _jwtSettings.ValidIssuer;
            string validAudience = _jwtSettings.ValidAudience;
            string expires = _jwtSettings.Expires;
            var tokenOptions = new JwtSecurityToken
            (
                issuer: validIssuer,
                audience: validAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(expires)),
                signingCredentials: signingCredentials

            );
            return tokenOptions;
        }
    }
}
