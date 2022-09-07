using Core.IServices.IRepositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class UserRepository : EntityRepository<User>,IUserRepository
    {
     
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
   
        }

        public async Task<User> GetUser(int id)
        {
           return await _applicationContext.Users.FirstOrDefaultAsync(user=>user.Id == id);
        }
        public async Task<User> IsUserExists(string email , string passwordHash)
        {
            return await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == email && user.PasswordHash == passwordHash);
        }
        public async Task<Avatar> GetAvatarAsync(int id)
        {
            var avatar = await _applicationContext.Users.Include(user=>user.Avatar).FirstOrDefaultAsync(user=>user.Id == id );
            return avatar.Avatar;
        }
    }
}
