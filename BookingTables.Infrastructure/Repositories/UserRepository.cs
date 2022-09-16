using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Infrastructure.Repositories
{
    public class UserRepository : EntityRepository<User>,IUserRepository
    {
     
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
   
        }

        public  Task<User> GetUserAsync(int id)
        {
           return _applicationContext.Users.FirstOrDefaultAsync(user=>user.Id == id);
        }
        public Task<User> GetValidateUserAsync(string email , string passwordHash)
        {
            return _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == email && user.PasswordHash == passwordHash);
        }
        public async Task<Avatar> GetUserAvatarAsync(int id)
        {
            var avatar = await _applicationContext.Users.Include(user=>user.Avatar).FirstOrDefaultAsync(user=>user.Id == id );
            return avatar.Avatar;
        }
    }
}
