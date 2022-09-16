using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;


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

        public virtual Task<List<T>> FindAllAsync(bool trackChanges)
        {
            return !trackChanges ? dataSet.AsNoTracking().ToListAsync() :  dataSet.ToListAsync();
        }
        public virtual void Update(T entity)
        {
            dataSet.Update(entity);
        }
    }
}
