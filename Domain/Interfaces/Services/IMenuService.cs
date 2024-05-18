using Domain.Entities;

namespace Domain.Interfaces.Services

{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItem>> GetMenuItemsAsync();
        Task<MenuItem> GetMenuItemAsync(int id);
        Task CreateMenuItemAsync(MenuItem item);
        Task UpdateMenuItemAsync(MenuItem item);
        Task DeleteMenuItemAsync(int id);
    }
}