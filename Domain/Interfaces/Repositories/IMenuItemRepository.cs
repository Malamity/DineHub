using Domain.Entities;

namespace Domain.Interfaces.Repositories

{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<MenuItem> GetByIdAsync(int id);
        Task AddAsync(MenuItem item);
        Task UpdateAsync(MenuItem item);
        Task DeleteAsync(int id);
    }
}