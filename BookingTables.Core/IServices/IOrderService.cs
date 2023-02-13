using Core.DTOs;
using BookingTables.Shared.RequestModels;
using Microsoft.AspNetCore.JsonPatch;

namespace Core.IServices
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrderAsync(int id);
        Task<List<OrderDTO>> GetUserOrders(OrderRequest orderRequest);
        Task<List<OrderDTO>> GetOrdersAsync(OrderRequest orderRequest);
        Task<OrderDTO> CreateOrderAsync(OrderFormDTO orderForCreationDTO);
        Task<int?> DeleteOrderAsync(int id);
        Task<OrderDTO> UpdateOrderAsync(int id, OrderFormDTO orderForUpdatingDTO);
        Task<OrderDTO> PartiallyUpdateOrder(int id, JsonPatchDocument<OrderFormDTO> patchDocument);
    }
}
