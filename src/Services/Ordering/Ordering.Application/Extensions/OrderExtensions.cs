namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToDto(this IEnumerable<Order> orders)
    {
        return orders.Select(ToDto);
    }

    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto(
            Id: order.Id,
            CustomerId: order.CustomerId,
            OrderName: order.OrderName,
            ShippingAddress: new AddressDto(
                order.ShippingAddress.FirstName,
                order.ShippingAddress.LastName,
                order.ShippingAddress.EmailAddress,
                order.ShippingAddress.AddressLine,
                order.ShippingAddress.Country,
                order.ShippingAddress.State,
                order.ShippingAddress.ZipCode),
            BillingAddress: new AddressDto(
                order.BillingAddress.FirstName,
                order.BillingAddress.LastName,
                order.BillingAddress.EmailAddress,
                order.BillingAddress.AddressLine,
                order.BillingAddress.Country,
                order.BillingAddress.State,
                order.BillingAddress.ZipCode),
            Payment: new PaymentDto(
                order.Payment.CardName,
                order.Payment.CardNumber,
                order.Payment.Expiration,
                order.Payment.CVV,
                order.Payment.PaymentMethod),
            Status: order.Status,
            OrderItems: order.OrderItems.Select(oi => new OrderItemDto(
                oi.OrderId,
                oi.ProductId,
                oi.Quantity,
                oi.Price)).ToList());
    }
}
