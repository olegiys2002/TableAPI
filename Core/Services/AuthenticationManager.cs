using Core.DTOs;
using Core.IServices;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Core.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private User _user;
        public AuthenticationManager(IUnitOfWork unitOfWork,IConfiguration  configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            
        }
        public async Task<bool> ValidateUser(UserForAuthenticationDTO userForAuthentication)
        {
            string passwordHash = HashPassword(userForAuthentication.Password);
            _user = await _unitOfWork.UserRepository.IsUserExists(userForAuthentication.Email, passwordHash);
            if (_user == null)
            {
                return false;
            }
         
            return true;
         
        }
        private string HashPassword(string password)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedBytes = SHA256.HashData(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
        public string CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            string key = _configuration.GetSection("SecretKey").Value;
            var encodingKey = Encoding.UTF8.GetBytes(key);
            var secret = new SymmetricSecurityKey(encodingKey);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private List<Claim> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,_user.Name),
                new Claim(ClaimTypes.Email,_user.Email),
                new Claim(ClaimTypes.Role,_user.Role)
            };
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            string validIssuer = _configuration.GetSection("validIssuer").Value;
            string validAudience = _configuration.GetSection("validAudience").Value;
            string expires = _configuration.GetSection("expires").Value;
            var tokenOptions = new JwtSecurityToken
            (
                issuer:validIssuer,
                audience:validAudience,
                claims:claims,
                expires:DateTime.Now.AddMinutes(Convert.ToDouble(expires)),
                signingCredentials:signingCredentials

            );
            return tokenOptions;
        }
    }
}
