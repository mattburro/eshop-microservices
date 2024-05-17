using MassTransit;
using Shared.Messaging.Events;

namespace Basket.API.Features.CheckoutBasket;

public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasketAsync(command.BasketCheckoutDto.Username, cancellationToken);

        if (basket is null)
        {
            return new CheckoutBasketResult(false);
        }

        var checkoutEvent = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        checkoutEvent.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(checkoutEvent, cancellationToken);

        await repository.DeleteBasketAsync(basket.Username);

        return new CheckoutBasketResult(true);
    }
}

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(c => c.BasketCheckoutDto).NotNull().WithMessage("CheckoutBasketDto can't be null");
        RuleFor(c => c.BasketCheckoutDto.Username).NotEmpty().WithMessage("Username id required");
    }
}