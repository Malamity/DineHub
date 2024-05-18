using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;

public class MenuService : IMenuService
{
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuService(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync() => await _menuItemRepository.GetAllAsync();

    public async Task<MenuItem> GetMenuItemAsync(int id)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);
        if (menuItem == null)
            throw new KeyNotFoundException($"Menu item with ID {id} not found.");
        return menuItem;
    }

    public async Task CreateMenuItemAsync(MenuItem item) => await _menuItemRepository.AddAsync(item);

    public async Task UpdateMenuItemAsync(MenuItem item)
    {
        var existingItem = await _menuItemRepository.GetByIdAsync(item.Id);
        if (existingItem == null)
            throw new KeyNotFoundException($"Menu item with ID {item.Id} not found.");

        existingItem.Name = item.Name;
        existingItem.Description = item.Description;
        existingItem.Price = item.Price;
        existingItem.Category = item.Category;

        await _menuItemRepository.UpdateAsync(existingItem);
    }

    public async Task DeleteMenuItemAsync(int id)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);
        if (menuItem == null)
            throw new KeyNotFoundException($"Menu item with ID {id} not found.");
            
        await _menuItemRepository.DeleteAsync(id);
    }
}