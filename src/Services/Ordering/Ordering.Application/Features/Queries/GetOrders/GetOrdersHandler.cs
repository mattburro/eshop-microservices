namespace Ordering.Application.Features.Queries.GetOrders;

public class GetOrdersQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetsOrdersResult>
{
    public async Task<GetsOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var count = await dbContext.Orders.LongCountAsync(cancellationToken);
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new GetsOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, count, orders.ToDto()));
    }
}
