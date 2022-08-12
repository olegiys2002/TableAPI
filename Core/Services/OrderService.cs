using Core.DTOs;
using Core.IServices;
using Core.Models;
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
        public async Task<OrderDTO> CreateOrder(OrderForCreationDTO orderForCreationDTO)
        {
            Order order = _mapper.ToDomainModel(orderForCreationDTO);
            Table table = await _unitOfWork.TableRepository.GetTable(orderForCreationDTO.TableId);
            
            if (table == null || order.CountOfPeople>table.CountOfSeats)
            {
                return null;
            }
            order.Table = table;
            order.CreatedAt = DateTime.Now;

            _unitOfWork.OrderRepository.Create(order);
            await _unitOfWork.SaveChangesAsync();
            OrderDTO orderDTO = _mapper.ToDTO(order);
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
            OrderDTO orderDTO = _mapper.ToDTO(order);
            return orderDTO;
        }

        public List<OrderDTO> GetOrders()
        {
            List<Order> orders = _unitOfWork.OrderRepository.FindAll(false).ToList();
            List<OrderDTO> orderDTOs = _mapper.ToListDTO(orders);
            return orderDTOs;
        }

        public async Task<bool> UpdateOrder(int id, OrderForUpdatingDTO orderForUpdatingDTO)
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
            order.UpdatedAt = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();
            return true;

        }
    }
}
