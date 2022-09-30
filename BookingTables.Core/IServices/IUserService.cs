using BookingTables.Infrastructure.Views;
using Core.DTOs;
using Shared.RequestModels;

namespace Core.IServices
{
    public interface IUserService
    {
        Task<int?> DeleteUserAsync(int id);
        Task<UserDTO> UpdateUserAsync(int id , UserFormDTO userForUpdatingDTO);
        Task<List<UserDTO>> GetUsersAsync(UserRequest userRequest);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(UserFormDTO userForCreationDTO);
        Task<AvatarDTO> GetUserAvatarAsync(int id);
        Task<List<UserAvatars>> GetUserIdWithAvatar();
    }
}
