using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _mockRepo;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _mockRepo = new Mock<IOrderRepository>();
        _orderService = new OrderService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetOrdersAsync_ReturnsOrders()
    {
        // Arrange
        var orders = new List<Order>
        {
            new Order { Id = 1, UserId = 1 },
            new Order { Id = 2, UserId = 2 }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orders);

        // Act
        var result = await _orderService.GetOrdersAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetOrderAsync_ReturnsOrder()
    {
        // Arrange
        var order = new Order { Id = 1, UserId = 1 };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(order);

        // Act
        var result = await _orderService.GetOrderAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.UserId);
    }

    [Fact]
    public async Task GetOrderAsync_OrderNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Order)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _orderService.GetOrderAsync(1));
    }

    [Fact]
    public async Task CreateOrderAsync_CreatesOrder()
    {
        // Arrange
        var order = new Order { Id = 1, UserId = 1 };

        // Act
        await _orderService.CreateOrderAsync(order);

        // Assert
        _mockRepo.Verify(repo => repo.AddAsync(order), Times.Once);
    }

    [Fact]
    public async Task UpdateOrderAsync_UpdatesOrder()
    {
        // Arrange
        var order = new Order { Id = 1, UserId = 1 };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(order);

        // Act
        order.UserId = 2;
        await _orderService.UpdateOrderAsync(order);

        // Assert
        _mockRepo.Verify(repo => repo.UpdateAsync(order), Times.Once);
    }

    [Fact]
    public async Task UpdateOrderAsync_OrderNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var order = new Order { Id = 1, UserId = 1 };
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Order)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _orderService.UpdateOrderAsync(order));
    }

    [Fact]
    public async Task DeleteOrderAsync_DeletesOrder()
    {
        // Arrange
        var order = new Order { Id = 1, UserId = 1 };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(order);

        // Act
        await _orderService.DeleteOrderAsync(1);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}
