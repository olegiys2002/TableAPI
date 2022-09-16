using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.Models;


namespace Infrastructure.Repositories
{
    public class OrderRepository : EntityRepository<Order>,IOrderRepository
    {
        public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
          
        }

        public Task<Order> GetOrderAsync(int id)
        {
           return _applicationContext.Orders.Include(order => order.Table).FirstOrDefaultAsync(order => order.Id == id);
        }
        public override Task<List<Order>> FindAllAsync(bool trackChanges)
        {
            return !trackChanges ? _applicationContext.Orders.Include(order => order.Table).AsNoTracking().ToListAsync()
                                  :_applicationContext.Orders.Include(order => order.Table).ToListAsync();
        }
        public Task<List<Order>> GetOrderPageAsync(int pageNumber,int pageSize)
        {
           return _applicationContext.Orders.Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
