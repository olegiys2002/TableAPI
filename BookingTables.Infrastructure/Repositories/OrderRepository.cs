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

        public Task<bool> IsDateAlreadyExsists(List<int> tables, DateTime startOfReservationDate, DateTime endOfReservationDate)
        {
            var isDateExists = _applicationContext.Orders.Include(o => o.Table)
                                                         .Where(o => o.Table.Any(table => tables.Contains(table.Id)))
                                                         .Where(o => startOfReservationDate < o.EndOfReservation && endOfReservationDate > o.StartOfReservation)
                                                         .AnyAsync();
            return isDateExists;
        }
     


        public Task<List<Order>> FindAllUserOrdersAsync(string userId,OrderRequest orderRequest,bool trackChanges)
        {
            return !trackChanges ? _applicationContext.Orders
                                             .Include(order => order.Table)
                                             .Filter(order => order.UserId == userId)
                                             .SortItems(orderRequest.SortModel)
                                             .GetPage(orderRequest.PageNumber, orderRequest.PageSize)
                                             .ToListAsync():
                                   _applicationContext.Orders
                                             .Include(order => order.Table)
                                             .Filter(order => order.UserId == userId)
                                             .SortItems(orderRequest.SortModel)
                                             .GetPage(orderRequest.PageNumber, orderRequest.PageSize)
                                             .AsNoTracking()
                                             .ToListAsync();
        }
        public Task<List<Order>> FindAllUserOrdersAsync(string userId)
        {
            return _applicationContext.Orders.Filter(order => order.UserId == userId)
                                             .ToListAsync();
        }
        public Task<Order> GetOrderAsync(int id)
        {
           return _applicationContext.Orders.Include(order => order.Table).FirstOrDefaultAsync(order => order.Id == id);
        } 
        public override Task<List<Order>> FindAllAsync(bool trackChanges,OrderRequest requestFeatures)
        {
            return !trackChanges ? _applicationContext.Orders.Include(order => order.Table)
                                                             .SortItems(requestFeatures.SortModel)
                                                             .GetPage(requestFeatures.PageNumber, requestFeatures.PageSize)
                                                             .ToListAsync()

                                  :_applicationContext.Orders.Include(order => order.Table)
                                                             .GetPage(requestFeatures.PageNumber, requestFeatures.PageSize)
                                                             .SortItems(requestFeatures.SortModel)
                                                             .AsNoTracking()
                                                             .ToListAsync();
        }
    }
}
