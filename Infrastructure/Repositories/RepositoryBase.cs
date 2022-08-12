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
        protected DbSet<T> dataSet;
        public RepositoryBase(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            dataSet = _applicationContext.Set<T>();
        }
        public virtual void Create(T entity)
        {
            dataSet.Add(entity);
        
        }

        public void Delete(T entity)
        {
            dataSet.Remove(entity);
        }

        public virtual IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? dataSet.AsNoTracking() : dataSet;
        }
        public virtual void Update(T entity)
        {
            dataSet.Update(entity);
        }
    }
}
