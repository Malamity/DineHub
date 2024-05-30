using Application.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IList<OrderDto> Orders { get; set; }

        public async Task OnGetAsync()
        {
            var orders = await _orderService.GetOrdersAsync();
            Orders = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    MenuItemId = i.MenuItemId,
                    Price = i.MenuItem.Price,
                    Quantity = i.Quantity
                }).ToList()
            }).ToList();
        }
    }
}