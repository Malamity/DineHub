using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesApp.Pages.Order
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;

        public CreateModel(IOrderService orderService, IMenuService menuService)
        {
            _orderService = orderService;
            _menuService = menuService;
        }

        [BindProperty]
        public OrderDto Order { get; set; }
        
        [BindProperty]
        public List<int> SelectedMenuItemIds { get; set; }

        public List<SelectListItem> MenuItems { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var menuItems = await _menuService.GetMenuItemsAsync();
            MenuItems = menuItems.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var menuItems = await _menuService.GetMenuItemsAsync();
                MenuItems = menuItems.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList();
                return Page();
            }

            var orderItems = SelectedMenuItemIds.Select(id => new OrderItem
            {
                MenuItemId = id,
                Quantity = 1 // Default to 1 for simplicity, you might want to make this configurable
            }).ToList();

            var order = new Domain.Entities.Order
            {
                UserId = Order.UserId,
                Items = orderItems
            };

            await _orderService.CreateOrderAsync(order);

            return RedirectToPage("./Index");
        }
    }
}
