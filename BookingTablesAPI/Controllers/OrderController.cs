using BookingTablesAPI.Filters;
using Core.DTOs;
using Core.IServices;
using Core.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace BookingTablesAPI.Controllers
{
    [Route("api/orders")]
    //[Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IRabbitMqService _rabbitMqService;
        public OrderController(IOrderService orderService,IRabbitMqService rabbitMqService)
        {
            _orderService = orderService;
            _rabbitMqService = rabbitMqService;
        }

        [HttpGet("{id}",Name ="OrderById")]
        [ValidationFilter]
        public async Task<IActionResult> GetOrder(int id)
        {
           OrderDTO orderDTO = await  _orderService.GetOrder(id);
            return orderDTO == null ? NotFound() : Ok(orderDTO);
        }
        //[Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderRequestFeatures orderRequest)
        {
            List<OrderDTO> orderDTOs = await _orderService.GetOrders(orderRequest);
            return orderDTOs == null ? NotFound() : Ok(orderDTOs);
        }
        [Authorize]
        [HttpPost]
        [ValidationFilter]
     
        public async Task<IActionResult> CreateOrder(OrderFormDTO orderForCreationDTO)
        {
            OrderDTO orderDTO = await _orderService.CreateOrder(orderForCreationDTO);
            return orderDTO == null ? BadRequest() : CreatedAtRoute("OrderById", new { orderDTO.Id }, orderDTO);
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
