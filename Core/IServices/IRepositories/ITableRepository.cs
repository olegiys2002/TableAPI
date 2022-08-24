using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices.IRepositories
{
    public interface ITableRepository : IEntityRepository<Table>
    {
        Task<Table> GetTable(int id);
        IQueryable<Table> GetTablesByIds(IEnumerable<int> ids,bool trackChanges);
    }
}
