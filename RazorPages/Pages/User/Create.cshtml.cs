using Application.DTOs;
using Domain.Enums;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserDto User { get; set; }

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

            var user = new Domain.Entities.User
            {
                Username = User.Username,
                Role = Enum.Parse<Role>(User.Role)
            };

            await _userService.CreateUserAsync(user);

            return RedirectToPage("./Index");
        }
    }
}