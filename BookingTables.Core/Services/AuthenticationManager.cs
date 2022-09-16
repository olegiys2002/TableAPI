using Core.DTOs;
using Core.IServices;
using SharedAssembly;
using Models.Models;

namespace Core.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public async Task<User> ValidateUserAsync(UserForAuthenticationDTO userForAuthentication)
        {
           
            string passwordHash = Hash.HashPassword(userForAuthentication.Password); ;
            var user = await _unitOfWork.UserRepository.GetValidateUserAsync(userForAuthentication.Email, passwordHash);
            return  user == null ? null : user;
      
        }
      
    }
}
