namespace Catalog.API.Features.Products.GetProductById;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductByIdQuery(id));

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
            .WithName("GetProductById")
            .WithSummary("Gets a product by its Id")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record GetProductByIdResponse(Product Product);
