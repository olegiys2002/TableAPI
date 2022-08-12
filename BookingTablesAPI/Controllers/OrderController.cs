using BookingTablesAPI.Filters;
using Core.DTOs;
using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingTablesAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}",Name ="OrderById")]
        [ValidationFilter]
        public async Task<IActionResult> GetOrder(int id)
        {
           OrderDTO orderDTO = await  _orderService.GetOrder(id);
            return orderDTO == null ? NotFound() : Ok(orderDTO);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            List<OrderDTO> orderDTOs = await _orderService.GetOrders();
            return Ok(orderDTOs);
        }
        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> CreateOrder(OrderFormDTO orderForCreationDTO)
        {
            OrderDTO orderDTO = await _orderService.CreateOrder(orderForCreationDTO);
            if (orderDTO == null)
            {
                return BadRequest();
            }
            return CreatedAtRoute("OrderById", new { orderDTO.Id }, orderDTO);
        }
        [HttpPut("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> UpdateOrder(int id, OrderFormDTO orderForUpdatingDTO)
        {
            bool isSuccess = await _orderService.UpdateOrder(id, orderForUpdatingDTO);
            return isSuccess ? NoContent() : NotFound();
        }
        [HttpDelete("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool isSuccess = await _orderService.DeleteOrder(id);
            return isSuccess ? NoContent() : NotFound();
        }

    }
}
