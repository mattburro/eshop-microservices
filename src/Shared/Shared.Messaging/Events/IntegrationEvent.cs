namespace Shared.Messaging.Events;

public abstract record IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTime Timestamp => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}
