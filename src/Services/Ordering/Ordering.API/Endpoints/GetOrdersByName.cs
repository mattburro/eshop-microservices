using Ordering.Application.Features.Queries.GetOrderByName;

namespace Ordering.API.Endpoints;

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, IMediator mediator) =>
        {
            var query = new GetOrdersByNameQuery(orderName);

            var result = await mediator.Send(query);

            var response = result.Adapt<GetOrdersByNameResponse>();

            return Results.Ok(response);
        })
            .WithName("GetOrdersByName")
            .WithSummary("Get all orders containing the given name")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
