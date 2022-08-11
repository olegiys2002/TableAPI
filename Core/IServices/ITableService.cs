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
        List<TableDTO> GetTables();
        Task<TableDTO> CreateTable(TableForCreationDTO tableForCreationDTO);
        Task<bool> UpdateTable(int id ,TableForUpdatingDTO tableForUpdateDTO);
        Task<bool> DeleteTable(int id);
        Task<TableDTO> GetTableById(int id);

        

    }
}
