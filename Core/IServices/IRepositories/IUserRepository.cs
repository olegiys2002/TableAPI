using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices.IRepositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
       Task<User> GetUser(int id);
    }
}
