using AutoMapper;
using Core.DTOs;
using Core.IServices;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Models.Models;
using BookingTables.Shared.RequestModels;
using BookingTables.Shared.EventModels;
using Microsoft.AspNetCore.Authorization;
using IdentityModel;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPublishEndpoint _publishEndpoint;
     
        public OrderService(IMapper mapper,IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor,IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _publishEndpoint = publishEndpoint;   
        }
        [Authorize]
        public async Task<OrderDTO> CreateOrderAsync(OrderFormDTO orderForCreationDTO)
        {
            var order = _mapper.Map<Order>(orderForCreationDTO);
            var tables = await _unitOfWork.TableRepository.GetTablesByIdsAsync(orderForCreationDTO.TablesId,true);
      
            if (tables.Count == 0)
            {
                return null;
            }

            var countOfSeats = 0;
            var tablesNumber = new List<int>();

            tables.ForEach(table => { countOfSeats += table.CountOfSeats; tablesNumber.Add(table.Number);});

            if (countOfSeats < orderForCreationDTO.CountOfPeople)
            {
                return null;
            }

            order.Table = tables;

            _unitOfWork.OrderRepository.Create(order);
            await _unitOfWork.SaveChangesAsync();

            var email = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == JwtClaimTypes.Email).Value;

           
            await _publishEndpoint.Publish(new Notification { Email = email, Tables = tablesNumber });
           
            var orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;


        }

        public async Task<int?> DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderAsync(id);

            if (order == null)
            {
                return null;
            }

            _unitOfWork.OrderRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();
            return order.Id;
        }

        public async Task<OrderDTO> GetOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderAsync(id);

            if (order == null)
            {
                return null;
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;
        }

        public async Task<List<OrderDTO>> GetOrdersAsync(OrderRequest orderRequest)
        {
            var orders = await _unitOfWork.OrderRepository.FindAllAsync(false,orderRequest);

            if (orders.Count == 0)
            {
                return null;
            }

            var orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
            return orderDTOs;
        }

        public async Task<OrderDTO> UpdateOrderAsync(int id, OrderFormDTO orderForUpdatingDTO)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderAsync(id);
            var tables = await _unitOfWork.TableRepository.GetTablesByIdsAsync(orderForUpdatingDTO.TablesId, true);

            if (tables.Count == 0)
            {
                return null;
            }

            var countOfSeats = 0;

            tables.ForEach(table => countOfSeats += table.CountOfSeats);
      
            if (countOfSeats < orderForUpdatingDTO.CountOfPeople)
            {
                return null;
            }

            order.CountOfPeople = orderForUpdatingDTO.CountOfPeople;
            order.DateOfReservation = orderForUpdatingDTO.DateOfReservation;
            order.Table = tables;

            await _unitOfWork.SaveChangesAsync();

            var orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;

        }
    }
}
