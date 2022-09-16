﻿using BookingTables.API.Controllers;
using BookingTablesAPI.Filters;
using Core.DTOs;
using Core.IServices;
using Core.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace BookingTablesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : AuthenticationGuardController
    {
        private readonly IOrderService _orderService;
       
        public OrderController(IOrderService orderService,IRabbitMqService rabbitMqService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}",Name ="OrderById")]
        [ValidationFilter]
        public async Task<IActionResult> GetOrder(int id)
        {
           var orderDTO = await  _orderService.GetOrderAsync(id);
            return orderDTO == null ? NotFound() : Ok(orderDTO);
        }
  
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderRequestFeatures orderRequest)
        {
            var orderDTOs = await _orderService.GetOrdersAsync(orderRequest);
            return orderDTOs == null ? NotFound() : Ok(orderDTOs);
        }

        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> CreateOrder(OrderFormDTO orderForCreationDTO)
        {
            var orderDTO = await _orderService.CreateOrderAsync(orderForCreationDTO);
            return orderDTO == null ? BadRequest() : CreatedAtRoute("OrderById", new { orderDTO.Id }, orderDTO);
        }

        [HttpPut("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> UpdateOrder(int id, OrderFormDTO orderForUpdatingDTO)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(id, orderForUpdatingDTO);
            return updatedOrder != null ? Ok(updatedOrder) : NotFound();
        }

        [HttpDelete("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderId = await _orderService.DeleteOrderAsync(id);
            return orderId != null ? Ok(orderId) : NotFound();
        }

    }
}