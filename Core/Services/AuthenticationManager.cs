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
using HashPassword;

namespace Core.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public async Task<User> ValidateUser(UserForAuthenticationDTO userForAuthentication)
        {
           
            string passwordHash = Hash.HashPassword(userForAuthentication.Password); ;
            var user = await _unitOfWork.UserRepository.IsUserExists(userForAuthentication.Email, passwordHash);
            return  user == null ? null : user;
      
        }
      
    }
}
