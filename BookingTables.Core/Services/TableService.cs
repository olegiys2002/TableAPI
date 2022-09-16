using AutoMapper;
using Core.DTOs;
using Core.IServices;
using FireSharp.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Models.Models;

namespace Core.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFirebaseClient _client;

        private readonly IMemoryCache _memoryCache;
        public TableService(IUnitOfWork unitOfWork,IMapper mapper,IFirebaseClient client,IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _client = client;
            _memoryCache = memoryCache;
        }
        
        public async Task<List<TableDTO>> CreateCollectionOfTablesAsync(IEnumerable<TableFormDTO> tableFormDTOs)
        {
            var tables = _mapper.Map<List<Table>>(tableFormDTOs);
            await _unitOfWork.TableRepository.AddTablesAsync(tables);
        
            var tablesDTO = _mapper.Map<List<TableDTO>>(tables);

            return tablesDTO;
        }

        public async Task<TableDTO> CreateTableAsync(TableFormDTO tableForCreationDTO)
        {
            var table = _mapper.Map<Table>(tableForCreationDTO);

            _unitOfWork.TableRepository.Create(table);
            await _unitOfWork.SaveChangesAsync();

            var tableDTO = _mapper.Map<TableDTO>(table);
            //var setter = await _client.SetAsync<TableDTO>($"TableList/+{tableDTO.Id}",tableDTO);
            return tableDTO;
        }

        public async Task<int?> DeleteTableAsync(int id)
        {
          var table = await _unitOfWork.TableRepository.GetTableAsync(id);

          if (table == null)
          {
              return null;
          }

          _unitOfWork.TableRepository.Delete(table);
          await _unitOfWork.SaveChangesAsync();

          return table.Id;
        }

        public async Task<TableDTO> GetTableByIdAsync(int id)
        {
            var table = await _unitOfWork.TableRepository.GetTableAsync(id);
            //var result = await _client.GetAsync($"TableList/+{id}");
            //var testTable = result.ResultAs<Table>();
            if (table == null)
            {
                return null;
            }
            var tableDTO = _mapper.Map<TableDTO>(table);
          
            return tableDTO;
        }

        public async Task<List<TableDTO>> GetTablesAsync()
        {
            var tables = await _unitOfWork.TableRepository.FindAllAsync(false);
            var tablesDTOs = _mapper.Map<List<TableDTO>>(tables);

            return tablesDTOs;
        }

        public async Task<List<TableDTO>> GetTablesByIdAsync(IEnumerable<int> ids)
        {
            var tables = await _unitOfWork.TableRepository.GetTablesByIdsAsync(ids,false);

            if (tables.Count == 0)
            {
                return null;
            }

            var tablesDTOs = _mapper.Map<List<TableDTO>>(tables);
            return tablesDTOs;
        }

        public async Task<TableDTO> UpdateTableAsync(int id ,TableFormDTO tableForUpdateDTO)
        {
            var table = await _unitOfWork.TableRepository.GetTableAsync(id);

            if (table == null)
            {
                return null;
            }

            table.CountOfSeats = tableForUpdateDTO.CountOfSeats;
            table.Number = tableForUpdateDTO.Number;
        
            await _unitOfWork.SaveChangesAsync();

            var tableDTO = _mapper.Map<TableDTO>(table);
            return tableDTO;
        }
    }
}
