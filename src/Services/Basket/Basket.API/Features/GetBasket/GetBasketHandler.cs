namespace Basket.API.Features.GetBasket;

internal class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // TODO: Get basket from database

        return new GetBasketResult(new ShoppingCart("test"));
    }
}

internal record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

internal record GetBasketResult(ShoppingCart Cart);