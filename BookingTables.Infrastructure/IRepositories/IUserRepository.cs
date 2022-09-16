using Models.Models;

namespace Infrastructure.IRepositories
{
    public interface IUserRepository : IEntityRepository<User>
    {
       Task<User> GetUserAsync(int id);
       Task<User> GetValidateUserAsync(string email, string passwordHash);
       Task<Avatar> GetUserAvatarAsync(int id);
    }
}
