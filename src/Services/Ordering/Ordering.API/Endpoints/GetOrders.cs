using Ordering.Application.Features.Queries.GetOrders;
using Shared.Pagination;

namespace Ordering.API.Endpoints;

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, IMediator mediator) =>
        {
            var query = new GetOrdersQuery(request);

            var result = await mediator.Send(query);

            var response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);
        })
            .WithName("GetOrders")
            .WithSummary("Gets all orders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
