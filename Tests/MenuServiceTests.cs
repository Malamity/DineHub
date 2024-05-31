using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class MenuServiceTests
{
    private readonly Mock<IMenuItemRepository> _mockRepo;
    private readonly MenuService _menuService;

    public MenuServiceTests()
    {
        _mockRepo = new Mock<IMenuItemRepository>();
        _menuService = new MenuService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetMenuItemsAsync_ReturnsMenuItems()
    {
        // Arrange
        var menuItems = new List<MenuItem>
        {
            new MenuItem { Id = 1, Name = "Pizza" },
            new MenuItem { Id = 2, Name = "Burger" }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(menuItems);

        // Act
        var result = await _menuService.GetMenuItemsAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Pizza", result.First().Name);
    }

    [Fact]
    public async Task GetMenuItemAsync_ReturnsMenuItem()
    {
        // Arrange
        var menuItem = new MenuItem { Id = 1, Name = "Pizza" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(menuItem);

        // Act
        var result = await _menuService.GetMenuItemAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Pizza", result.Name);
    }

    [Fact]
    public async Task GetMenuItemAsync_MenuItemNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((MenuItem)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _menuService.GetMenuItemAsync(1));
    }

    [Fact]
    public async Task CreateMenuItemAsync_CreatesMenuItem()
    {
        // Arrange
        var menuItem = new MenuItem { Id = 1, Name = "Pizza" };

        // Act
        await _menuService.CreateMenuItemAsync(menuItem);

        // Assert
        _mockRepo.Verify(repo => repo.AddAsync(menuItem), Times.Once);
    }

    [Fact]
    public async Task UpdateMenuItemAsync_UpdatesMenuItem()
    {
        // Arrange
        var menuItem = new MenuItem { Id = 1, Name = "Pizza" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(menuItem);

        // Act
        menuItem.Name = "Updated Pizza";
        await _menuService.UpdateMenuItemAsync(menuItem);

        // Assert
        _mockRepo.Verify(repo => repo.UpdateAsync(menuItem), Times.Once);
    }

    [Fact]
    public async Task UpdateMenuItemAsync_MenuItemNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var menuItem = new MenuItem { Id = 1, Name = "Pizza" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((MenuItem)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _menuService.UpdateMenuItemAsync(menuItem));
    }

    [Fact]
    public async Task DeleteMenuItemAsync_DeletesMenuItem()
    {
        // Arrange
        var menuItem = new MenuItem { Id = 1, Name = "Pizza" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(menuItem);

        // Act
        await _menuService.DeleteMenuItemAsync(1);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteMenuItemAsync_MenuItemNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((MenuItem)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _menuService.DeleteMenuItemAsync(1));
    }
}
