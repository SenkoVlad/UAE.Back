using UAE.Application.Models.Order;
using UAE.Application.Services.Interfaces;

namespace UAE.Application.Services.Implementations;

internal sealed class OrderService : IOrderService
{
    public Task<OrderModel> CreateOrder(OrderModel orderModel)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderAsync(OrderModel order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOrderAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderModel> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}