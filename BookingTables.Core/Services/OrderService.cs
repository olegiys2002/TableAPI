using AutoMapper;
using Core.DTOs;
using Core.IServices;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Models.Models;
using BookingTables.Shared.RequestModels;
using BookingTables.Shared.EventModels;
using IdentityModel;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderService> _logger;


        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IPublishEndpoint publishEndpoint,ILogger<OrderService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderFormDTO orderForCreationDTO)
        {
            var order = _mapper.Map<Order>(orderForCreationDTO);
            
            var tables = await _unitOfWork.TableRepository.GetTablesByIdsAsync(orderForCreationDTO.TablesId, true);
            var isOrderValid = IsOrderForCreationValid(orderForCreationDTO, tables);
          
            if (!isOrderValid)
            {
                return null;
            }

            var isDateBusy = await _unitOfWork.OrderRepository.IsDateAlreadyExsists( orderForCreationDTO.TablesId , orderForCreationDTO.StartOfReservation, orderForCreationDTO.EndOfReservation);
            
            if (isDateBusy)
            {
                return null;
            }

            var countOfSeats = 0;
            decimal costOfOrder = 0;
            var tablesNumber = new List<int>();

            tables.ForEach(table => { countOfSeats += table.CountOfSeats; tablesNumber.Add(table.Number); });
            tables.ForEach(table => { costOfOrder += table.Cost;});

            if (countOfSeats < orderForCreationDTO.CountOfPeople)
            {
                return null;
            }

            var userName = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == JwtClaimTypes.Name).Value;
            var userId = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == JwtClaimTypes.Subject).Value;

            order.Table = tables;
            order.CostOfOrder = costOfOrder;
            order.UserName = userName;
            order.UserId = userId;

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

            var orders = await _unitOfWork.OrderRepository.FindAllAsync(false, orderRequest);

            if (orders.Count == 0)
            {
                return null;
            }

            var orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
            return orderDTOs;
        }

        public async Task<List<OrderDTO>> GetUserOrders(OrderRequest orderRequest)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == JwtClaimTypes.Subject).Value;
            var orders = await _unitOfWork.OrderRepository.FindAllUserOrdersAsync(userId, orderRequest, false);

            if (orders.Count == 0)
            {
                return null;
            }

            var ordersDTO = _mapper.Map<List<OrderDTO>>(orders);
            return ordersDTO;
        }

        public async Task<OrderDTO> PartiallyUpdateOrder(int id, JsonPatchDocument<OrderFormDTO> patchDocument)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderAsync(id);

            if (order == null)
            {
                return null;
            }
            
            var orderToPatch = _mapper.Map<OrderFormDTO>(order);
            patchDocument.ApplyTo(orderToPatch);

            var tables = new List<int>();
            //var tablesIds = order.Table.ForEach(table => tables.Add(table.Id));

            _mapper.Map(orderToPatch,order);

            var orderDTO = _mapper.Map<OrderDTO>(orderToPatch);

            await _unitOfWork.SaveChangesAsync();

            return orderDTO;
        }

        public async Task<OrderDTO> UpdateOrderAsync(int id, OrderFormDTO orderForUpdatingDTO)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderAsync(id);
            var tables = await _unitOfWork.TableRepository.GetTablesByIdsAsync(orderForUpdatingDTO.TablesId, true);

            var isOrderValid = IsOrderForCreationValid(orderForUpdatingDTO, tables);

            if (!isOrderValid)
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
            order.StartOfReservation = orderForUpdatingDTO.StartOfReservation;
            order.EndOfReservation = orderForUpdatingDTO.EndOfReservation;
            order.Table = tables;

            await _unitOfWork.SaveChangesAsync();

            var orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;

        }
    
        private bool IsOrderForCreationValid(OrderFormDTO orderForCreationDTO,List<Table> tables) 
        {
            if (tables.Count == 0)
            {
                return false;
            }

            if (orderForCreationDTO.StartOfReservation >= orderForCreationDTO.EndOfReservation)
            {
                return false;
            }

            TimeSpan difference = orderForCreationDTO.EndOfReservation - orderForCreationDTO.StartOfReservation;

            _logger.LogInformation($"difference between orders reservations is {difference} hours");

            if (difference.Hours > 20)
            {
                return false;
            }

            return true;
        }


    }
}
