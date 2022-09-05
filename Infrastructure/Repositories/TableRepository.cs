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
    public class TableRepository : EntityRepository<Table> ,ITableRepository
    {
        
        public TableRepository(ApplicationContext applicationContext) : base (applicationContext)
        {
         
        }

        public async Task<Table> GetTable(int id)
        {
           return await _applicationContext.Tables.FirstOrDefaultAsync(table=>table.Id == id);
        }
        public IQueryable<Table> GetTablesByIds(IEnumerable<int> ids,bool trackChanges)
        {
           return trackChanges ? _applicationContext.Tables.Where(table => ids.Contains(table.Id)) : _applicationContext.Tables.Where(table => ids.Contains(table.Id)).AsNoTracking();   
        }
    }
}
