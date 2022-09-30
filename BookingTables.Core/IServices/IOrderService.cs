using Core.DTOs;
using Shared.RequestModels;

namespace Core.IServices
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrderAsync(int id);
        Task<List<OrderDTO>> GetOrdersAsync(OrderRequest orderRequest);
        Task<OrderDTO> CreateOrderAsync(OrderFormDTO orderForCreationDTO);
        Task<int?> DeleteOrderAsync(int id);
        Task<OrderDTO> UpdateOrderAsync(int id, OrderFormDTO orderForUpdatingDTO);
    }
}
