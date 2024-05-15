using Ordering.Application.Features.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<CreateOrderCommand>();

            var result = await mediator.Send(command);

            var response = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/orders/{response.Id}", response);
        })
            .WithName("CreateOrder")
            .WithSummary("Creates an order")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(Guid Id);
