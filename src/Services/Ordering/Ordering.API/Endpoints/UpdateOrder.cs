using Ordering.Application.Features.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders/{id}", async (UpdateOrderRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<CreateOrderCommand>();

            var result = await mediator.Send(command);

            var response = result.Adapt<UpdateOrderResponse>();

            return Results.Ok(response);
        })
            .WithName("UpdateOrder")
            .WithSummary("Updates an order")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);
