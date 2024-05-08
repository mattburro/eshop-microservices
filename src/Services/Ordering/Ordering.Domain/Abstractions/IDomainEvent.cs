using MediatR;

namespace Ordering.Domain.Abstractions;

public class IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime Timestamp => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
