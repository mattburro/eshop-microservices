namespace Ordering.Domain.Models;

public class Customer : Entity<Guid>
{
    protected Customer() { }

    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public static Customer Create(Guid id, string name, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

        return new Customer
        {
            Id = id,
            Name = name,
            Email = email
        };
    }
}
