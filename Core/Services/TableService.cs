using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
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
        private readonly IFirebaseClient _client;
        public TableService(IUnitOfWork unitOfWork,IMapper mapper,IFirebaseClient client)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _client = client;
        }
        
        public async Task<List<TableDTO>> CreateCollectionOfTables(IEnumerable<TableFormDTO> tableFormDTOs)
        {
            var tables = new List<Table>();
            foreach (var tableForCreationDTO in tableFormDTOs)
            {
                var table = _mapper.Map<Table>(tableForCreationDTO);
                _unitOfWork.TableRepository.Create(table);
                tables.Add(table);
            }
            await _unitOfWork.SaveChangesAsync();
            var tablesDTO = _mapper.Map<List<TableDTO>>(tables);
            return tablesDTO;
        }

        public async Task<TableDTO> CreateTable(TableFormDTO tableForCreationDTO)
        {
            var table = _mapper.Map<Table>(tableForCreationDTO);

            _unitOfWork.TableRepository.Create(table);
            await _unitOfWork.SaveChangesAsync();

            var tableDTO = _mapper.Map<TableDTO>(table);
            //var setter = await _client.SetAsync<TableDTO>($"TableList/+{tableDTO.Id}",tableDTO);
            return tableDTO;
        }

        public async Task<bool> DeleteTable(int id)
        {
          var table = await _unitOfWork.TableRepository.GetTable(id);

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
            var table = await _unitOfWork.TableRepository.GetTable(id);
            //var result = await _client.GetAsync($"TableList/+{id}");
            //var testTable = result.ResultAs<Table>();
            if (table == null)
            {
                return null;
            }
            var tableDTO = _mapper.Map<TableDTO>(table);
          
            return tableDTO;
        }

        public async Task<List<TableDTO>> GetTables()
        {
            var tables = await _unitOfWork.TableRepository.FindAll(false).ToListAsync();
            var tablesDTOs = _mapper.Map<List<TableDTO>>(tables);
            return tablesDTOs;
        }

        public async Task<List<TableDTO>> GetTablesById(IEnumerable<int> ids)
        {
            var tables = await _unitOfWork.TableRepository.GetTablesByIds(ids,false).ToListAsync();
            if (tables == null)
            {
                return null;
            }
            var tablesDTOs = _mapper.Map<List<TableDTO>>(tables);
            return tablesDTOs;
        }

        public async Task<bool> UpdateTable(int id ,TableFormDTO tableForUpdateDTO)
        {
            var table = await _unitOfWork.TableRepository.GetTable(id);

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
