using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using BookingTables.Shared.RepositoriesExtensions;
using BookingTables.Shared.RequestModels;

namespace Infrastructure.Repositories
{
    public class TableRepository : EntityRepository<Table,TableRequest> ,ITableRepository
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
           return _applicationContext.Tables.FirstOrDefaultAsync(table => table.Id == id);
        }
        public Task<List<Table>> GetTablesByIdsAsync(IEnumerable<int> ids,bool trackChanges)
        {
           return trackChanges ? _applicationContext.Tables.Where(table => ids.Contains(table.Id)).ToListAsync()
                               : _applicationContext.Tables.Where(table => ids.Contains(table.Id)).AsNoTracking().ToListAsync();   
        }
        public override Task<List<Table>> FindAllAsync(bool trackChanges, TableRequest requestFeatures)
        {
            return !trackChanges ? _applicationContext.Tables
                                   .Filter(table => table.CountOfSeats < requestFeatures.MaxCountOfSeats && table.CountOfSeats > requestFeatures.MinCountOfSeats)
                                   .SortItems(requestFeatures.SortModel)
                                   .GetPage(requestFeatures.PageNumber, requestFeatures.PageSize)
                                   .ToListAsync() :
                                    _applicationContext.Tables
                                   .Filter(table => table.CountOfSeats < requestFeatures.MaxCountOfSeats && table.CountOfSeats > requestFeatures.MinCountOfSeats)
                                   .SortItems(requestFeatures.SortModel)
                                   .GetPage(requestFeatures.PageNumber, requestFeatures.PageSize)
                                   .AsNoTracking()
                                   .ToListAsync();
        }
    }
}
