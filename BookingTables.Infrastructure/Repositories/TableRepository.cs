using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.Models;


namespace Infrastructure.Repositories
{
    public class TableRepository : EntityRepository<Table> ,ITableRepository
    {
        
        public TableRepository(ApplicationContext applicationContext) : base (applicationContext)
        {
         
        }

        public Task AddTablesAsync(List<Table> tables)
        {
           return _applicationContext.Tables.AddRangeAsync(tables);
        }

        public Task<Table> GetTableAsync(int id)
        {
           return _applicationContext.Tables.FirstOrDefaultAsync(table=>table.Id == id);
        }
        public Task<List<Table>> GetTablesByIdsAsync(IEnumerable<int> ids,bool trackChanges)
        {
           return trackChanges ? _applicationContext.Tables.Where(table => ids.Contains(table.Id)).ToListAsync()
                               : _applicationContext.Tables.Where(table => ids.Contains(table.Id)).AsNoTracking().ToListAsync();   
        }
    }
}
