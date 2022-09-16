using Models.Models;


namespace Infrastructure.IRepositories
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        Task<Order> GetOrderAsync(int id);
        Task<List<Order>> GetOrderPageAsync(int pageNumber, int pageSize);
    }
}
