using Core.IServices;
using Core.IServices.IRepositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        private ITableRepository _tableRepository;
        private IUserRepository _userRepository;
        private IOrderRepository _orderRepository;
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
        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_applicationContext);
                return _userRepository;
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
