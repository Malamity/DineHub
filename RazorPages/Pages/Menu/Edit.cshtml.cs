using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Menu
{
    public class EditModel : PageModel
    {
        private readonly IMenuService _menuService;

        public EditModel(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [BindProperty]
        public MenuItemDto MenuItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var menuItem = await _menuService.GetMenuItemAsync(id);

            if (menuItem == null)
            {
                return NotFound();
            }

            MenuItem = new MenuItemDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                Category = menuItem.Category
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var menuItem = new MenuItem
            {
                Id = MenuItem.Id,
                Name = MenuItem.Name,
                Description = MenuItem.Description,
                Price = MenuItem.Price,
                Category = MenuItem.Category
            };

            await _menuService.UpdateMenuItemAsync(menuItem);

            return RedirectToPage("./Index");
        }
    }
}