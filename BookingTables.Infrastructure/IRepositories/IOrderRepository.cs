using Models.Models;
using BookingTables.Shared.RequestModels;

namespace Infrastructure.IRepositories
{
    public interface IOrderRepository : IEntityRepository<Order,OrderRequest>
    {
        Task<Order> GetOrderAsync(int id);
        Task<List<Order>> FindAllUserOrdersAsync(string userId,OrderRequest orderRequest,bool trackChanges);
        Task<List<Order>> FindAllUserOrdersAsync(string userId);
        Task<bool> IsDateAlreadyExsists(List<int> tables, DateTime startOfReservationDate, DateTime endOfReservationDate);
    }
}
