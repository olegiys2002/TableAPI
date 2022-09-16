using Microsoft.EntityFrameworkCore;
using Models.Models;


namespace Infrastructure.Repositories
{
    public class EntityRepository<T> : RepositoryBase<T> where T : Entity
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

        public override Task<List<T>> FindAllAsync(bool trackChanges)
        {
            return !trackChanges ? dataSet.AsNoTracking().ToListAsync() : dataSet.ToListAsync();
        }
    }
}
