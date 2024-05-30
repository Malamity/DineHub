using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync() => await _orderRepository.GetAllAsync();

    public async Task CreateOrderAsync(Order order) => await _orderRepository.AddAsync(order);

    public async Task<Order> GetOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new KeyNotFoundException($"Order with ID {id} not found.");
        return order;
    }

    public async Task UpdateOrderAsync(Order order)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(order.Id);
        if (existingOrder == null)
            throw new KeyNotFoundException($"Order with ID {order.Id} not found.");

        existingOrder.Items = order.Items;

        await _orderRepository.UpdateAsync(existingOrder);
    }
    
    public async Task DeleteOrderAsync(int id) => await _orderRepository.DeleteAsync(id);
}