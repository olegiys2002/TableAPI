using Models.Models;
using BookingTables.Shared.RequestModels;

namespace Infrastructure.IRepositories
{
    public interface ITableRepository : IEntityRepository<Table,TableRequest>
    {
        Task<Table> GetTableAsync(int id);
        Task<List<Table>> GetTablesByIdsAsync(IEnumerable<int> ids,bool trackChanges);
        Task AddTablesAsync(List<Table> tables);
    }
}
