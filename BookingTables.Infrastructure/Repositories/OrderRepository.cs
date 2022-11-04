using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using BookingTables.Shared.RepositoriesExtensions;
using BookingTables.Shared.RequestModels;

namespace Infrastructure.Repositories
{
    public class OrderRepository : EntityRepository<Order, OrderRequest>,IOrderRepository
    {
        public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
          
        }
        public Task<List<Order>> FindAllUserOrdersAsync(string userId)
        {
            return _applicationContext.Orders.Where(order=>order.UserId == userId).ToListAsync();
        }
        public Task<Order> GetOrderAsync(int id)
        {
           return _applicationContext.Orders.Include(order => order.Table).FirstOrDefaultAsync(order => order.Id == id);
        } 
        public override Task<List<Order>> FindAllAsync(bool trackChanges,OrderRequest requestFeatures)
        {
            return !trackChanges ? _applicationContext.Orders.Include(order => order.Table)
                                                             .GetPage(requestFeatures.PageNumber, requestFeatures.PageSize)
                                                             .AsNoTracking()
                                                             .ToListAsync()
                                  :_applicationContext.Orders.Include(order => order.Table)
                                                             .GetPage(requestFeatures.PageNumber, requestFeatures.PageSize)
                                                             .ToListAsync();
        }
    }
}
