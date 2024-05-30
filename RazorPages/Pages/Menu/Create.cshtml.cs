using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Menu
{
    public class CreateModel : PageModel
    {
        private readonly IMenuService _menuService;

        public CreateModel(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [BindProperty]
        public MenuItemDto MenuItem { get; set; }

        public IActionResult OnGet()
        {
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
                Name = MenuItem.Name,
                Description = MenuItem.Description,
                Price = MenuItem.Price,
                Category = MenuItem.Category
            };

            await _menuService.CreateMenuItemAsync(menuItem);

            return RedirectToPage("./Index");
        }
    }
}