namespace Basket.API.Features.StoreBasket;

internal class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // TODO: Store basket in database using Marten upsert
        // TODO: Update cache

        return new StoreBasketResult("user");
    }
}

internal record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

internal class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(c => c.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(c => c.Cart.Username).NotEmpty().WithMessage("Username is required");
    }
}

internal record StoreBasketResult(string Username);
