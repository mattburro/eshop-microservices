namespace Catalog.API.Features.Products.CreateProduct;

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create product entity from command
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        // Save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // Return result
        return new CreateProductResult(product.Id);
    }
}

public record class CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(c => c.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(c => c.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public record class CreateProductResult(Guid Id);
