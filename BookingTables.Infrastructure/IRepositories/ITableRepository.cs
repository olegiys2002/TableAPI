using Models.Models;
using Shared.RequestModels;

namespace Infrastructure.IRepositories
{
    public interface ITableRepository : IEntityRepository<Table,TableRequest>
    {
        Task<Table> GetTableAsync(int id);
        Task<List<Table>> GetTablesByIdsAsync(IEnumerable<int> ids,bool trackChanges);
        Task AddTablesAsync(List<Table> tables);
    }
}
