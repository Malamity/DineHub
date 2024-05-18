using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class MenuItemValidator : AbstractValidator<MenuItemDto>
{
    public MenuItemValidator()
    {
        RuleFor(x => x.Name)!.NotEmpty()!.WithMessage("Name is required.");
        RuleFor(x => x.Price)!.GreaterThan(0)!.WithMessage("Price must be greater than 0.");
    }
}