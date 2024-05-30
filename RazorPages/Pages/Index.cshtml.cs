using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Domain.Interfaces.Services;
using Infrastructure.Services;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _secretKey;
    private readonly int _expiryMinutes;
    private readonly IUserService _userService;
    private readonly AuthenticationService _authService;

    public IndexModel(
        ILogger<IndexModel> logger, 
        IHttpContextAccessor httpContextAccessor, 
        IConfiguration configuration,
        IUserService userService,
        AuthenticationService authService)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _secretKey = configuration.GetValue<string>("JwtSettings:SecretKey");
        _expiryMinutes = configuration.GetValue<int>("JwtSettings:ExpiryMinutes");
        _userService = userService;
        _authService = authService;
    }

    [BindProperty]
    public string Username { get; set; }
    [BindProperty]
    public string Password { get; set; }
    public bool IsAuthenticated { get; private set; }

    public void OnGet()
    {
        var token = JwtHelper.GetJwtTokenFromCookies(_httpContextAccessor.HttpContext);
        if (JwtHelper.ValidateJwtToken(token, out var jwtSecurityToken, _secretKey))
        {
            IsAuthenticated = true;
        }
        else
        {
            IsAuthenticated = false;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userService.GetUserByUsernameAsync(Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        var token = _authService.GenerateJwtToken(user);

        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(_expiryMinutes)
        });

        return RedirectToPage("./Index");
    }

    public IActionResult OnPostLogout()
    {
        Response.Cookies.Delete("jwt");
        return RedirectToPage("./Index");
    }
}
