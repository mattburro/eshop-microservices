using FluentValidation;

namespace Ordering.Application.Features.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.Order.OrderName).NotEmpty().WithMessage("OrderName is required");
        RuleFor(c => c.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(c => c.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}
