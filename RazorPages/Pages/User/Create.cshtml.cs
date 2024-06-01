using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.DTOs;
using Domain.Interfaces.Services;

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

            // Check for existing username
            var existingUser = await _userService.GetUserByUsernameAsync(User.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("User.Username", "Username already exists.");
                return Page();
            }

            var user = new Domain.Entities.User
            {
                Username = User.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(User.Password),
                Role = Enum.Parse<Domain.Enums.Role>(User.Role)
            };

            await _userService.CreateUserAsync(user);

            return RedirectToPage("./Index");
        }
    }
}