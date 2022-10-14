using BookingTables.Infrastructure.Views;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Shared.RequestModels;

namespace Infrastructure.Repositories
{
    public class UserRepository : EntityRepository<User,UserRequest>,IUserRepository
    {
     
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
   
        }

        public override Task<List<User>> FindAllAsync(bool trackChanges, UserRequest requestFeatures)
        {
            return base.FindAllAsync(trackChanges, requestFeatures);
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
        public Task<List<UserAvatarsDTO>> GetAvatarsWihtUserId()
        {
            return _applicationContext.UserAvatars.ToListAsync();
        }
    }
}
