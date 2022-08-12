using Core.DTOs;
using Core.IServices;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TableService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TableDTO> CreateTable(TableForCreationDTO tableForCreationDTO)
        {
            Table table = _mapper.ToDomainModel(tableForCreationDTO);
            table.CreatedAt = DateTime.Now;
            table.UpdatedAt = null;

            _unitOfWork.TableRepository.Create(table);
            await _unitOfWork.SaveChangesAsync();

            TableDTO tableDTO = _mapper.ToDTO(table);
            return tableDTO;
        }

        public async Task<bool> DeleteTable(int id)
        {
          Table table = await _unitOfWork.TableRepository.GetTable(id);

          if (table == null)
          {
            return false;
          }

          _unitOfWork.TableRepository.Delete(table);
          await _unitOfWork.SaveChangesAsync();

          return true;
        }

        public async Task<TableDTO> GetTableById(int id)
        {
            Table table = await _unitOfWork.TableRepository.GetTable(id);
            if (table == null)
            {
                return null;
            }
            TableDTO tableDTO = _mapper.ToDTO(table);

            return tableDTO;
        }

        public List<TableDTO> GetTables()
        {
            List<Table> tables = _unitOfWork.TableRepository.FindAll(false).ToList();
            List<TableDTO> tablesDTOs = _mapper.ToListDTO(tables);

            return tablesDTOs;
        }

        public async Task<bool> UpdateTable(int id ,TableForUpdatingDTO tableForUpdateDTO)
        {
            Table table = await _unitOfWork.TableRepository.GetTable(id);

            if (table == null)
            {
                return false;
            }

            table.CountOfSeats = tableForUpdateDTO.CountOfSeats;
            table.Number = tableForUpdateDTO.Number;
            table.UpdatedAt = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
