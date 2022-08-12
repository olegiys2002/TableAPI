using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices.IRepositories
{
    public interface IRepositoryBase <T>
    {
 
        IQueryable<T> FindAll(bool trackChanges);
        void Delete(T entity);
        void Create(T entity);
        void Update(T entity);

    }
}
