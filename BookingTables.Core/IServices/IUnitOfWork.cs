using Infrastructure.IRepositories;

namespace Core.IServices
{
    public interface IUnitOfWork
    {
        ITableRepository  TableRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task SaveChangesAsync();
    }
}
