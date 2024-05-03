namespace Basket.API.Features.GetBasket;

internal class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasketAsync(query.Username, cancellationToken);

        return new GetBasketResult(basket);
    }
}

internal record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

internal record GetBasketResult(ShoppingCart Cart);