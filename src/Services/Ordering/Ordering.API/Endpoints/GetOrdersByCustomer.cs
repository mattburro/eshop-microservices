using Ordering.Application.Features.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, IMediator mediator) =>
        {
            var query = new GetOrdersByCustomerQuery(customerId);

            var result = await mediator.Send(query);

            var response = result.Adapt<GetOrdersByCustomerResponse>();

            return Results.Ok(response);
        })
            .WithName("GetOrdersByCustomer")
            .WithSummary("Gets all the orders belonging to the given customer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
