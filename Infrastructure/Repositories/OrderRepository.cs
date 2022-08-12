using Core.IServices.IRepositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>,IOrderRepository
    {
        private readonly ApplicationContext _applicationContext;
        public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Order> GetOrder(int id)
        {
           return await _applicationContext.Orders.Include(order => order.Table).FirstOrDefaultAsync(order => order.Id == id);
        }
        public new IQueryable<Order> FindAll(bool trackChanges)
        {
            return !trackChanges ? _applicationContext.Orders.Include(order => order.Table).AsNoTracking()
                                  :_applicationContext.Orders.Include(order => order.Table);
        }
    }
}
