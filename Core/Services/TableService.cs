using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<TableDTO> CreateTable(TableFormDTO tableForCreationDTO)
        {
            Table table = _mapper.Map<Table>(tableForCreationDTO);

            _unitOfWork.TableRepository.Create(table);
            await _unitOfWork.SaveChangesAsync();

            TableDTO tableDTO = _mapper.Map<TableDTO>(table);
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
            TableDTO tableDTO = _mapper.Map<TableDTO>(table);

            return tableDTO;
        }

        public async Task<List<TableDTO>> GetTables()
        {
            List<Table> tables = await _unitOfWork.TableRepository.FindAll(false).ToListAsync();
            List<TableDTO> tablesDTOs = _mapper.Map<List<TableDTO>>(tables);
            return tablesDTOs;
        }

        public async Task<bool> UpdateTable(int id ,TableFormDTO tableForUpdateDTO)
        {
            Table table = await _unitOfWork.TableRepository.GetTable(id);

            if (table == null)
            {
                return false;
            }

            table.CountOfSeats = tableForUpdateDTO.CountOfSeats;
            table.Number = tableForUpdateDTO.Number;
        

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
