namespace Ordering.Domain.Models;

public class Product : Entity<Guid>
{
    protected Product() { }

    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public static Product Create(Guid id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

        return new Product
        {
            Id = id,
            Name = name,
            Price = price,
        };
    }
}
