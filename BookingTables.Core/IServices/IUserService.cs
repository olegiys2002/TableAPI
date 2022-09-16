using Core.DTOs;


namespace Core.IServices
{
    public interface IUserService
    {
        Task<int?> DeleteUserAsync(int id);
        Task<UserDTO> UpdateUserAsync(int id , UserFormDTO userForUpdatingDTO);
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(UserFormDTO userForCreationDTO);
        Task<AvatarDTO> GetUserAvatarAsync(int id);
    }
}
