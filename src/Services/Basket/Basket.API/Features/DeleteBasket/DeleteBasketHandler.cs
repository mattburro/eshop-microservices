
namespace Basket.API.Features.DeleteBasket;

public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        // TODO: Delete basket from database and cache

        return new DeleteBasketResult(true);
    }
}

internal record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;

internal class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required");
    }
}

internal record DeleteBasketResult(bool IsSuccess);