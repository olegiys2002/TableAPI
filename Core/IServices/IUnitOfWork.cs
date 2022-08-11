using Core.IServices.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IUnitOfWork
    {
        ITableRepository  TableRepository { get; }
        IUserRepository UserRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task SaveChangesAsync();
    }
}
