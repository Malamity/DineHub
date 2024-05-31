using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _userService = new UserService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllUsersAsync_ReturnsUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Username = "user1" },
            new User { Id = 2, Username = "user2" }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "user1" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("user1", result.Username);
    }

    [Fact]
    public async Task GetUserByIdAsync_UserNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.GetUserByIdAsync(1));
    }

    [Fact]
    public async Task GetUserByUsernameAsync_ReturnsUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "user1" };
        _mockRepo.Setup(repo => repo.GetByUsernameAsync("user1")).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByUsernameAsync("user1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("user1", result.Username);
    }

    [Fact]
    public async Task GetUserByUsernameAsync_UserNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.GetUserByUsernameAsync("user1"));
    }

    [Fact]
    public async Task CreateUserAsync_CreatesUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "user1" };

        // Act
        await _userService.CreateUserAsync(user);

        // Assert
        _mockRepo.Verify(repo => repo.AddAsync(user), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync_UpdatesUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "user1" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

        // Act
        user.Username = "updatedUser1";
        await _userService.UpdateUserAsync(user);

        // Assert
        _mockRepo.Verify(repo => repo.UpdateAsync(user), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync_UserNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var user = new User { Id = 1, Username = "user1" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.UpdateUserAsync(user));
    }

    [Fact]
    public async Task DeleteUserAsync_DeletesUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "user1" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

        // Act
        await _userService.DeleteUserAsync(1);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteUserAsync_UserNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.DeleteUserAsync(1));
    }
}
