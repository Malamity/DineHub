using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Order
{
    public class EditModel : PageModel
    {
        private readonly IOrderService _orderService;

        public EditModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public OrderDto Order { get; set; }

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
                    Quantity = i.Quantity
                }).ToList()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var order = new Domain.Entities.Order
            {
                Id = Order.Id,
                UserId = Order.UserId,
                Items = Order.Items.Select(i => new OrderItem
                {
                    Id = i.Id,
                    MenuItemId = i.MenuItemId,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _orderService.UpdateOrderAsync(order);

            return RedirectToPage("./Index");
        }
    }
}