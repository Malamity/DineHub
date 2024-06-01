using Application.DTOs;
using Application.Services;
using Domain.Interfaces.Services;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Menu
{
    public class IndexModel : PageModel
    {
        private readonly IMenuService _menuService;

        public IndexModel(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public IList<MenuItemDto> MenuItems { get; set; }

        public async Task OnGetAsync()
        {
            var items = await _menuService.GetMenuItemsAsync();
            MenuItems = items.Select(m => new MenuItemDto 
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Category = m.Category
            }).ToList();
        }
    }
}