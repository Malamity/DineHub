using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly ApplicationDbContext _context;

    public MenuItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MenuItem item)
    {
        await _context.MenuItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.MenuItems.FindAsync(id);
        if (item != null)
        {
            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync() => await _context.MenuItems.ToListAsync();

    public async Task<MenuItem> GetByIdAsync(int id) => (await _context.MenuItems.FindAsync(id))!;

    public Task UpdateAsync(MenuItem item)
    {
        _context.MenuItems.Update(item);
        return _context.SaveChangesAsync();
    }
}