using Core.DTOs;
using Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrder(int id);
        Task<List<OrderDTO>> GetOrders(OrderRequestFeatures orderRequest);
        Task<OrderDTO> CreateOrder(OrderFormDTO orderForCreationDTO);
        Task<bool> DeleteOrder(int id);
        Task<bool> UpdateOrder(int id, OrderFormDTO orderForUpdatingDTO);
    }
}
