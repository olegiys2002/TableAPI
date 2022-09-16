using Models.Models;


namespace Infrastructure.IRepositories
{
    public interface ITableRepository : IEntityRepository<Table>
    {
        Task<Table> GetTableAsync(int id);
        Task<List<Table>> GetTablesByIdsAsync(IEnumerable<int> ids,bool trackChanges);
        Task AddTablesAsync(List<Table> tables);
    }
}
