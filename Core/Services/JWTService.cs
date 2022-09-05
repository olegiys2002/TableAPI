using Core.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class JWTService : IJWTService
    {
        public string GetEmailFromToken(JwtSecurityToken token)
        {
            var email = token.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            return email;
        }
        public JwtSecurityToken ParseJwtToken(string jwt)
        {

            var jwtNormalize = jwt.Replace("\"", String.Empty);
            var token = new JwtSecurityToken(jwtNormalize);

            return token;
        }
    }
}
