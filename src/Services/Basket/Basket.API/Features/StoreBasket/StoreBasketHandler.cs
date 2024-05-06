using Discount.Grpc;

namespace Basket.API.Features.StoreBasket;

internal class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscounts(command.Cart, cancellationToken);

        var basket = await repository.StoreBasketAsync(command.Cart, cancellationToken);

        return new StoreBasketResult(basket.Username);
    }

    private async Task DeductDiscounts(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
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
