
namespace Catalog.API.Features.Products.GetProductsByCategory;

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductsByCategoryQuery(category));

            var response = result.Adapt<GetProductsByCategoryResponse>();

            return Results.Ok(response);
        })
            .WithName("GetProductByCategory")
            .WithSummary("Gets the products that belong to a category")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
