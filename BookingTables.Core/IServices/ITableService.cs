using Core.DTOs;

namespace Core.IServices
{
    public interface ITableService
    {
        Task<List<TableDTO>> GetTablesAsync();
        Task<TableDTO> CreateTableAsync(TableFormDTO tableForCreationDTO);
        Task<TableDTO> UpdateTableAsync(int id ,TableFormDTO tableForUpdateDTO);
        Task<int?> DeleteTableAsync(int id);
        Task<TableDTO> GetTableByIdAsync(int id);
        Task<List<TableDTO>> CreateCollectionOfTablesAsync(IEnumerable<TableFormDTO> tableFormDTOs);
        Task<List<TableDTO>> GetTablesByIdAsync(IEnumerable<int> ids);

        

    }
}
