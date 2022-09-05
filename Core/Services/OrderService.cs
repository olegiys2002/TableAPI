using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using Core.Models.PaginationModels;
using Core.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IJWTService _jwtService;
        public OrderService(IMapper mapper,IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor,IRabbitMqService rabbitMqService,IJWTService jwtService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _rabbitMqService = rabbitMqService;
            _jwtService = jwtService;
         
       
        }
        public async Task<OrderDTO> CreateOrder(OrderFormDTO orderForCreationDTO)
        {
            var order = _mapper.Map<Order>(orderForCreationDTO);
            var countOfSeats = 0;
            var tablesNumber = new List<int>();

            var tables = await _unitOfWork.TableRepository.GetTablesByIds(orderForCreationDTO.TablesId,true).ToListAsync();
      
            if (tables.Count == 0)
            {
                return null;
            }

            foreach (var table in tables)
            {
                countOfSeats += table.CountOfSeats;
                tablesNumber.Add(table.Number);
                
            
            }

            if (countOfSeats < orderForCreationDTO.CountOfPeople)
            {
                return null;
            }
            order.Table = tables;
            _unitOfWork.OrderRepository.Create(order);
            await _unitOfWork.SaveChangesAsync();

            var tokenString = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", String.Empty);
            var token = _jwtService.ParseJwtToken(tokenString);
            var email = _jwtService.GetEmailFromToken(token);
            
            _rabbitMqService.SendMessage(new {Email = email,Tables = tablesNumber});
           

            var orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;


        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(id);
            if (order == null)
            {
                return false;
            }
            _unitOfWork.OrderRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<OrderDTO> GetOrder(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(id);
            if (order == null)
            {
                return null;
            }
            var orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;
        }

        public async Task<List<OrderDTO>> GetOrders(OrderRequestFeatures orderRequest)
        {
            var ordersQuary = _unitOfWork.OrderRepository.FindAll(false);
            var orders = await PagedList<Order>.ToPagedList(ordersQuary, orderRequest.PageNumber, orderRequest.PageSize);
            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(orders.Metadata));
            if (orders == null)
            {
                return null;
            }
            var orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
            return orderDTOs;
        }

        public async Task<bool> UpdateOrder(int id, OrderFormDTO orderForUpdatingDTO)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(id);
            var countOfSeats = 0;

            var tables = await _unitOfWork.TableRepository.GetTablesByIds(orderForUpdatingDTO.TablesId, true).ToListAsync();

            if (tables.Count == 0)
            {
                return false;
            }

            foreach (var table in tables)
            {
                countOfSeats += table.CountOfSeats;

            }

            if (countOfSeats < orderForUpdatingDTO.CountOfPeople)
            {
                return false;
            }

            order.CountOfPeople = orderForUpdatingDTO.CountOfPeople;
            order.DateOfReservation = orderForUpdatingDTO.DateOfReservation;
            order.Table = tables;
          

            await _unitOfWork.SaveChangesAsync();
            return true;

        }
    }
}
