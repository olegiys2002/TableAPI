using BookingTables.Infrastructure.Views;
using Models.Models;
using Shared.RequestModels;

namespace Infrastructure.IRepositories
{
    public interface IUserRepository : IEntityRepository<User,UserRequest>
    {
       Task<User> GetUserAsync(int id);
       Task<User> GetValidateUserAsync(string email, string passwordHash);
       Task<Avatar> GetUserAvatarAsync(int id);
       Task<List<UserAvatarsDTO>> GetAvatarsWihtUserId();
    }
}
