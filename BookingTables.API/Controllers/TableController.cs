using Core.Commands;
using Core.DTOs;
using Core.IServices;
using Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookingTables.Shared.RequestModels;

namespace BookingTablesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITableService _tableService;
        private readonly ILogger<TableController> _logger;
        public TableController(ITableService tableService,IMediator mediator,ILogger<TableController> logger)
        {
            _tableService = tableService;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}",Name ="tableById")]
        public async Task<IActionResult> GetTable(int id)
        {
            var tableDTO = await _mediator.Send(new GetTableQuery(id));

            return tableDTO == null ? NotFound() : Ok(tableDTO);
        }

        [HttpPut]
        public async Task<IActionResult> GetTables(TableRequest? tableRequest)
        {
            _logger.LogInformation($"Get tables with {tableRequest.PageSize} page size");
            var tablesDTOs = await _mediator.Send(new GetTablesQuery(tableRequest));

            return tablesDTOs == null ? NotFound() : Ok(tablesDTOs); 
        }

        [HttpPost("collection")]     
        public async Task<IActionResult> CreateCollectionOfTables(IEnumerable<TableFormDTO> tableFormDTOs)
        {
           var tables = await _tableService.CreateCollectionOfTablesAsync(tableFormDTOs);
           var ids = string.Join(",", tables.Select(table => table.Id));

           return tables == null ? BadRequest() : CreatedAtRoute("Collection", new { ids }, tables);
        }

        [HttpPut("collection",Name ="Collection")]
        public async Task<IActionResult> GetCollectionOfTables(IEnumerable<int> ids)
        {
           var tablesDTOs =  await _tableService.GetTablesByIdAsync(ids);

           return Ok(tablesDTOs);
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateTable(TableFormDTO tableForCreationDTO)
        {
            var tableDTO = await _mediator.Send(new AddTableCommand(tableForCreationDTO));

            return CreatedAtRoute("tableById",new { tableDTO.Id },tableDTO);
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateTable(int id, TableFormDTO tableForUpdatingDTO)
        {
            var updatedTable = await _mediator.Send(new UpdateTableCommand(tableForUpdatingDTO,id));
      
            return updatedTable != null ? Ok(updatedTable) : NotFound();
        }

        [HttpDelete("{id}")]
      
        public async Task<IActionResult> DeleteTable(int id)
        {
            var tableId = await _tableService.DeleteTableAsync(id);
            return tableId != null ? Ok(tableId) : NotFound();
        }

    }
}
