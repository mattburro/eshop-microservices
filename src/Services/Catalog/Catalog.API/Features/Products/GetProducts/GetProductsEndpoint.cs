namespace Catalog.API.Features.Products.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request ,IMediator mediator) =>
        {
            var query = request.Adapt<GetProductsQuery>();

            var result = await mediator.Send(query);

            var response = result.Adapt<GetsProductsResponse>();

            return Results.Ok(response);
        })
            .WithName("GetProducts")
            .WithSummary("Gets all products")
            .Produces<GetsProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetsProductsResponse(IEnumerable<Product> Products);