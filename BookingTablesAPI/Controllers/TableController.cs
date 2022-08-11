using Core.DTOs;
using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingTablesAPI.Controllers
{
    [Route("api/tables")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;
        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet("{id}",Name ="tableById")]
        public async Task<IActionResult> GetTable(int id)
        {
            TableDTO tableDTO = await _tableService.GetTableById(id);

            if (tableDTO == null)
            {
                return NotFound();
            }
            return Ok(tableDTO);
        }
        [HttpGet]
        public IActionResult GetTables()
        {
            List<TableDTO> tableDTOs = _tableService.GetTables();
            return Ok(tableDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable(TableForCreationDTO tableForCreationDTO)
        {
            TableDTO tableDTO = await _tableService.CreateTable(tableForCreationDTO);

            return CreatedAtRoute("tableById",new { tableDTO.Id },tableDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(int id, TableForUpdatingDTO tableForUpdatingDTO)
        {
            bool isSuccess = await _tableService.UpdateTable(id, tableForUpdatingDTO);
            return isSuccess ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            bool isSuccess = await _tableService.DeleteTable(id);
            return isSuccess ? NoContent() : NotFound();
        }
    }
}
