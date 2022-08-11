using Core.IServices.IRepositories;
using Core.Models;
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

    }
}
