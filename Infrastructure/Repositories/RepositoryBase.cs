using Core.IServices.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationContext _applicationContext;
        public RepositoryBase(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public void Create(T entity)
        {
            _applicationContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _applicationContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? _applicationContext.Set<T>().AsNoTracking() : _applicationContext.Set<T>();
        }
        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
