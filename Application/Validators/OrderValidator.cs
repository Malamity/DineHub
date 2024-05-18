using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class OrderValidator : AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(x => x.Items)!.NotEmpty()!.WithMessage("Order must have at least one item.");
    }
}