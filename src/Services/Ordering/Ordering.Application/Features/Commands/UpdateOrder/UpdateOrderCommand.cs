using FluentValidation;

namespace Ordering.Application.Features.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Order.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(c => c.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(c => c.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
    }
}