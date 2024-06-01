using System.ComponentModel.DataAnnotations;
using Domain.Interfaces.Services;

namespace RazorPagesApp.ValidationAttributes;

public class UniqueUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var userService = validationContext.GetService<IUserService>();
        var existingUser = userService.GetUserByUsernameAsync(value.ToString()).Result;

        if (existingUser != null)
        {
            return new ValidationResult("Username already exists.");
        }

        return ValidationResult.Success;
    }
}