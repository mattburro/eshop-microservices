
namespace Basket.API.Features.DeleteBasket;

internal class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var isSuccess = await repository.DeleteBasketAsync(command.Username, cancellationToken);

        return new DeleteBasketResult(isSuccess);
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