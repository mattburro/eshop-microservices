
using Ordering.Application.Features.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, IMediator mediator) =>
        {
            var command = new DeleteOrderCommand(id);

            var result = await mediator.Send(command);

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        })
            .WithName("DeleteOrder")
            .WithSummary("Deletes an order")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

public record DeleteOrderResponse(bool IsSuccess);
