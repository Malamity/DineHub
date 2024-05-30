using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Order
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService _orderService;

        public CreateModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public OrderDto Order { get; set; }

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

            var order = new Domain.Entities.Order
            {
                UserId = Order.UserId,
                Items = Order.Items.Select(i => new OrderItem
                {
                    MenuItemId = i.MenuItemId,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _orderService.CreateOrderAsync(order);

            return RedirectToPage("./Index");
        }
    }
}