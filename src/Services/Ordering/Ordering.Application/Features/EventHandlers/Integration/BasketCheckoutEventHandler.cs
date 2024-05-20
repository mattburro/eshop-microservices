﻿using MassTransit;
using Ordering.Application.Features.Commands.CreateOrder;
using Shared.Messaging.Events;

namespace Ordering.Application.Features.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ILogger<BasketCheckoutEventHandler> logger, IMediator mediator) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation($"Integration Event handled: {context.Message.GetType().Name}");

        var command = MapToCreateOrderCommand(context.Message);
        await mediator.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.Username,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: Ordering.Domain.Enums.OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
            ]);

        return new CreateOrderCommand(orderDto);
    }
}
