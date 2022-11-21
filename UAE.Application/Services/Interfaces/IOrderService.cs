using UAE.Application.Models.Order;

namespace UAE.Application.Services.Interfaces;

public interface IOrderService
{
    Task<OrderModel> CreateOrder(OrderModel orderModel);
    
    Task UpdateOrderAsync(OrderModel order);
    
    Task DeleteOrderAsync(int id);
    
    Task<OrderModel> GetOrderByIdAsync(int id);
}