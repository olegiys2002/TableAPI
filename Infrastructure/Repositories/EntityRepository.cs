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
    public class EntityRepository<T> : RepositoryBase<T> where T : Entity
    {
        public EntityRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
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

        public override IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? dataSet.AsNoTracking() : dataSet;
        }
    }
}
