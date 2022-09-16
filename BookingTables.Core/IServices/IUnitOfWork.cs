using Infrastructure.IRepositories;

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
