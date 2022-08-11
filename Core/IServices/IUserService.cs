using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IUserService
    {
        Task<bool> DeleteUser(int id);
        Task<bool> UpdateUser(int id , UserForUpdatingDTO userForUpdatingDTO);
        List<UserDTO> GetUsers();
        Task<UserDTO> GetUserById(int id);
        Task<UserDTO> CreateUser(UserForCreationDTO userForCreationDTO);
    }
}
