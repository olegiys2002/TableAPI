using Models.Models;
using BookingTables.Shared.RequestModels;

namespace Infrastructure.IRepositories
{
    public interface IOrderRepository : IEntityRepository<Order,OrderRequest>
    {
        Task<Order> GetOrderAsync(int id);
        Task<List<Order>> FindAllUserOrdersAsync(string userId);
    }
}
