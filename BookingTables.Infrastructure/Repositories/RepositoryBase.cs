using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using BookingTables.Shared.RepositoriesExtensions;
using BookingTables.Shared.RequestModels;

namespace Infrastructure
{
    public abstract class RepositoryBase<T,K> : IRepositoryBase<T,K> where T : class
                                                                     where K : RequestFeatures   
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

        public virtual void Delete(T entity)
        {
            dataSet.Remove(entity);
        }

        public virtual Task<List<T>> FindAllAsync(bool trackChanges,K requestFeatures)
        {
            return !trackChanges ? dataSet.GetPage(requestFeatures.PageNumber,requestFeatures.PageSize).AsNoTracking().ToListAsync():  
                                   dataSet.GetPage(requestFeatures.PageNumber, requestFeatures.PageSize).ToListAsync();
        }
        public virtual void Update(T entity)
        {
            dataSet.Update(entity);
        }
    }
}
