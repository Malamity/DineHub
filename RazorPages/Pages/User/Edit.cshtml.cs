using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.DTOs;
using Domain.Interfaces.Services;

namespace RazorPagesApp.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserDto User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.ToString()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userService.GetUserByIdAsync(User.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.Username = User.Username;
            if (!string.IsNullOrEmpty(User.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(User.Password);
            }
            user.Role = Enum.Parse<Domain.Enums.Role>(User.Role);

            await _userService.UpdateUserAsync(user);

            return RedirectToPage("./Index");
        }
    }
}