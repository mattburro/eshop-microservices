using Ordering.Application.Features.Queries.GetOrderByName;

namespace Ordering.Application.Features.Queries.GetOrdersByName;

public class GetOrdersByNameQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.ToLower().Contains(query.Name.ToLower()))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders.ToDto());
    }
}
