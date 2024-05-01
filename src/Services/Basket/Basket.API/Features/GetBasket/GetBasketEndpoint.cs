namespace Basket.API.Features.GetBasket;

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string username, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetBasketQuery(username));

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
            .WithName("GetBasket")
            .WithSummary("Gets the basket associated with a username")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

internal record GetBasketResponse(ShoppingCart Cart);
