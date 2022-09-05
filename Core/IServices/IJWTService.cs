using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IJWTService
    {
        JwtSecurityToken ParseJwtToken(string jwt);
        string GetEmailFromToken(JwtSecurityToken token);
    }
}
