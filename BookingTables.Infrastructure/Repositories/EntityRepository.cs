using Microsoft.EntityFrameworkCore;
using Models.Models;
using BookingTables.Shared.RepositoriesExtensions;
using BookingTables.Shared.RequestModels;

namespace Infrastructure.Repositories
{
    public class EntityRepository<T,K> : RepositoryBase<T,K> where T : Entity
                                                             where K : RequestFeatures
    {
        public EntityRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
           
        }

        public override void Create(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            dataSet.Add(entity);

        }
        
        public override void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            dataSet.Update(entity);
        }

        public override Task<List<T>> FindAllAsync(bool trackChanges,K requestFeatures)
        {
            return !trackChanges ? dataSet.AsNoTracking().GetPage(requestFeatures.PageNumber, requestFeatures.PageSize).ToListAsync() :
                                   dataSet.GetPage(requestFeatures.PageNumber, requestFeatures.PageSize).ToListAsync();
        }
      
    }
}
