using BookingTablesAPI.Filters;
using Core.Commands;
using Core.DTOs;
using Core.IServices;
using Core.Models.Cache;
using Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BookingTablesAPI.Controllers
{
    [Route("api/tables")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITableService _tableService;
        public TableController(ITableService tableService,IMediator mediator)
        {
            _tableService = tableService;
            _mediator = mediator;
       
        }

        [HttpGet("{id}",Name ="tableById")]
        [ValidationFilter]
        public async Task<IActionResult> GetTable(int id)
        {
            TableDTO tableDTO = await _mediator.Send(new GetTableQuery(id));

            
            //TableDTO tableDTO = await _tableService.GetTableById(id);

            return tableDTO == null ? NotFound() : Ok(tableDTO);
        }
        [HttpGet]
        public async Task<IActionResult> GetTables()
        {
            var tablesDTOs = await _mediator.Send(new GetTablesQuery());
            //List<TableDTO> tableDTOs = await _tableService.GetTables();
            return tablesDTOs == null ? NotFound() : Ok(tablesDTOs); 
        }
        [HttpPost("collection")]
        [ValidationFilter]
        public async Task<IActionResult> CreateCollectionOfTables(IEnumerable<TableFormDTO> tableFormDTOs)
        {
           var tables = await _tableService.CreateCollectionOfTables(tableFormDTOs);
           var ids = string.Join(",", tables.Select(table => table.Id));
           return tables == null ? BadRequest() : CreatedAtRoute("Collection", new { ids }, tables);
        }

        [HttpGet("collection/({ids})",Name ="Collection")]
        public async Task<IActionResult> GetCollectionOfTables(IEnumerable<int> ids)
        {
           var tablesDTOs =  await _tableService.GetTablesById(ids);
           return Ok(tablesDTOs);
        }

        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> CreateTable(TableFormDTO tableForCreationDTO)
        {
            TableDTO tableDTO = await _mediator.Send(new AddTableCommand(tableForCreationDTO));
            //TableDTO tableDTO = await _tableService.CreateTable(tableForCreationDTO);

            return CreatedAtRoute("tableById",new { tableDTO.Id },tableDTO);
        }

        [HttpPut("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> UpdateTable(int id, TableFormDTO tableForUpdatingDTO)
        {
            bool isSuccess = await _mediator.Send(new UpdateTableCommand(tableForUpdatingDTO,id));
            //bool isSuccess = await _tableService.UpdateTable(id, tableForUpdatingDTO);
            return isSuccess ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> DeleteTable(int id)
        {
            bool isSuccess = await _tableService.DeleteTable(id);
            return isSuccess ? NoContent() : NotFound();
        }
    }
}
