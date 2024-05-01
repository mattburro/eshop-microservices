
namespace Basket.API.Features.StoreBasket;

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<StoreBasketCommand>();

            var result = await mediator.Send(command);

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.Username}", response);
        })
            .WithName("StoreBasket")
            .WithSummary("Creates or updates a user's basket")
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string Username);
