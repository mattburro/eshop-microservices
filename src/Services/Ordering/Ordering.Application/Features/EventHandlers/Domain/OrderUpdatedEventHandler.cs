﻿namespace Ordering.Application.Features.EventHandlers.Domain;

public class OrderUpdatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Domain Event handled: {notification.GetType().Name}");

        return Task.CompletedTask;
    }
}
