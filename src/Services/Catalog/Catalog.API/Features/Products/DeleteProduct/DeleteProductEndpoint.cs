
namespace Catalog.API.Features.Products.DeleteProduct;

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, IMediator mediator) =>
        {
            var command = new DeleteProductCommand(id);

            await mediator.Send(command);

            return Results.Ok();
        })
            .WithName("DeleteProduct")
            .WithSummary("Deletes a product")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record DeleteProductResponse(bool Success);
