using Shared.Pagination;

namespace Ordering.Application.Features.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetsOrdersResult>;

public record GetsOrdersResult(PaginatedResult<OrderDto> Orders);