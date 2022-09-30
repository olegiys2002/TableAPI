using Models.Models;
using Shared.RequestModels;

namespace Infrastructure.IRepositories
{
    public interface IOrderRepository : IEntityRepository<Order,OrderRequest>
    {
        Task<Order> GetOrderAsync(int id);
    }
}
