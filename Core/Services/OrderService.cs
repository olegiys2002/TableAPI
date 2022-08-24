using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
       
        }
        public async Task<OrderDTO> CreateOrder(OrderFormDTO orderForCreationDTO)
        {
            var order = _mapper.Map<Order>(orderForCreationDTO);
            var countOfSeats = 0;

            var tables = await _unitOfWork.TableRepository.GetTablesByIds(orderForCreationDTO.TablesId,true).ToListAsync();
      
            if (tables.Count == 0)
            {
                return null;
            }

            foreach (var table in tables)
            {
                countOfSeats += table.CountOfSeats;
            
            }

            if (countOfSeats < orderForCreationDTO.CountOfPeople)
            {
                return null;
            }

            order.Table = tables;
            _unitOfWork.OrderRepository.Create(order);
            await _unitOfWork.SaveChangesAsync();
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

        public async Task<List<OrderDTO>> GetOrders()
        {
            var orders = await _unitOfWork.OrderRepository.FindAll(false).ToListAsync();
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
