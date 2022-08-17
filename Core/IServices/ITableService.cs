using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface ITableService
    {
        Task<List<TableDTO>> GetTables();
        Task<TableDTO> CreateTable(TableFormDTO tableForCreationDTO);
        Task<bool> UpdateTable(int id ,TableFormDTO tableForUpdateDTO);
        Task<bool> DeleteTable(int id);
        Task<TableDTO> GetTableById(int id);
        Task<List<TableDTO>> CreateCollectionOfTables(IEnumerable<TableFormDTO> tableFormDTOs);
        Task<List<TableDTO>> GetTablesById(IEnumerable<int> ids);

        

    }
}
