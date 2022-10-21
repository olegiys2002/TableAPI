using Core.IServices;
using Infrastructure;
using Infrastructure.IRepositories;
using Infrastructure.Repositories;

namespace Core.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        private ITableRepository ?_tableRepository;
        private IOrderRepository ?_orderRepository;
        public UnitOfWork(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public ITableRepository TableRepository
        {
            get
            {
               _tableRepository ??= new TableRepository(_applicationContext); 
                return _tableRepository;
            }
        }
        public IOrderRepository OrderRepository
        {
            get
            {
                _orderRepository ??= new OrderRepository(_applicationContext);
                return _orderRepository;
            }
        }
        public async Task SaveChangesAsync()
        {
          await  _applicationContext.SaveChangesAsync();
        }
    }
}
