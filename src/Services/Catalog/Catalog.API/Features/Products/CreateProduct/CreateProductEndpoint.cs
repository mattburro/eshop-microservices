using Shared.CQRS;

namespace Catalog.API.Features.Products.CreateProduct;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await mediator.Send(command);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
            .WithName("CreateProduct")
            .WithSummary("Creates a new product")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record class CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;

public record class CreateProductResponse(Guid Id);
