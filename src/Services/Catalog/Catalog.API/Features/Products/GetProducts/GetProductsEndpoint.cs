using Catalog.API.Models;

namespace Catalog.API.Features.Products.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductsQuery());

            var response = result.Adapt<GetsProductsResponse>();

            return Results.Ok(response);
        })
            .WithName("GetProducts")
            .WithSummary("Gets all products")
            .Produces<GetsProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record GetsProductsResponse(IEnumerable<Product> Products);