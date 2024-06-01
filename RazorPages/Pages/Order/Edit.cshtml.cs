using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesApp.Pages.Order
{
    public class EditModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;

        public EditModel(IOrderService orderService, IMenuService menuService)
        {
            _orderService = orderService;
            _menuService = menuService;
        }

        [BindProperty]
        public OrderDto Order { get; set; }

        [BindProperty]
        public List<int> SelectedMenuItemIds { get; set; }

        public List<SelectListItem> MenuItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            Order = new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    MenuItemId = i.MenuItemId,
                    Price = i.MenuItem.Price,
                    Quantity = i.Quantity,
                    MenuItemName = i.MenuItem.Name
                }).ToList()
            };

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
                Id = Order.Id,
                UserId = Order.UserId,
                Items = orderItems
            };

            await _orderService.UpdateOrderAsync(order);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteItemAsync(int orderId, int orderItemId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var item = order.Items.FirstOrDefault(i => i.Id == orderItemId);
            if (item != null)
            {
                order.Items.Remove(item);
                await _orderService.UpdateOrderAsync(order);
            }

            return RedirectToPage("./Edit", new { id = orderId });
        }
    }
}
