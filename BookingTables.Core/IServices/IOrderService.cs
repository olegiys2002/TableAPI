using Core.DTOs;
using Core.Models.Request;

namespace Core.IServices
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrderAsync(int id);
        Task<List<OrderDTO>> GetOrdersAsync(OrderRequestFeatures orderRequest);
        Task<OrderDTO> CreateOrderAsync(OrderFormDTO orderForCreationDTO);
        Task<int?> DeleteOrderAsync(int id);
        Task<OrderDTO> UpdateOrderAsync(int id, OrderFormDTO orderForUpdatingDTO);
    }
}
