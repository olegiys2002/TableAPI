using Core.DTOs;
using Models.Models;

namespace Core.IServices
{
    public interface IAuthenticationManager
    {
        Task<User> ValidateUserAsync(UserForAuthenticationDTO user);
    }
}
