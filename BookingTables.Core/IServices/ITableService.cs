using Core.DTOs;
using Shared.RequestModels;

namespace Core.IServices
{
    public interface ITableService
    {
        Task<List<TableDTO>> GetTablesAsync(TableRequest tableRequest);
        Task<TableDTO> CreateTableAsync(TableFormDTO tableForCreationDTO);
        Task<TableDTO> UpdateTableAsync(int id ,TableFormDTO tableForUpdateDTO);
        Task<int?> DeleteTableAsync(int id);
        Task<TableDTO> GetTableByIdAsync(int id);
        Task<List<TableDTO>> CreateCollectionOfTablesAsync(IEnumerable<TableFormDTO> tableFormDTOs);
        Task<List<TableDTO>> GetTablesByIdAsync(IEnumerable<int> ids);

        

    }
}
