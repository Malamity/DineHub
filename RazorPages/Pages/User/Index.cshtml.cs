using Application.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<UserDto> Users { get; set; }

        public async Task OnGetAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            Users = users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role.ToString()
            }).ToList();
        }
    }
}