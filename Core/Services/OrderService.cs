using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Order order = _mapper.Map<Order>(orderForCreationDTO);
            Table table = await _unitOfWork.TableRepository.GetTable(orderForCreationDTO.TableId);
            
            if (table == null || order.CountOfPeople>table.CountOfSeats)
            {
                return null;
            }
            order.Table = table;
     

            _unitOfWork.OrderRepository.Create(order);
            await _unitOfWork.SaveChangesAsync();
            OrderDTO orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;
            
        }

        public async Task<bool> DeleteOrder(int id)
        {
            Order order = await _unitOfWork.OrderRepository.GetOrder(id);
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
            Order order = await _unitOfWork.OrderRepository.GetOrder(id);
            if (order == null)
            {
                return null;
            }
            OrderDTO orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;
        }

        public async Task<List<OrderDTO>> GetOrders()
        {
            List<Order> orders = await _unitOfWork.OrderRepository.FindAll(false).ToListAsync();
            List<OrderDTO> orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
            return orderDTOs;
        }

        public async Task<bool> UpdateOrder(int id, OrderFormDTO orderForUpdatingDTO)
        {
            Order order = await _unitOfWork.OrderRepository.GetOrder(id);
            Table table = await _unitOfWork.TableRepository.GetTable(orderForUpdatingDTO.TableId);

            if (order == null || table == null|| table.CountOfSeats < order.CountOfPeople)
            {
                return false;
            }

            order.CountOfPeople = orderForUpdatingDTO.CountOfPeople;
            order.DateOfReservation = orderForUpdatingDTO.DateOfReservation;
            order.Table = table;
          

            await _unitOfWork.SaveChangesAsync();
            return true;

        }
    }
}
