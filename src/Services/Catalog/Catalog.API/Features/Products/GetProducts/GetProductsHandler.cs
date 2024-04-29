﻿using Marten.Pagination;

namespace Catalog.API.Features.Products.GetProducts;

internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);

        return new GetProductsResult(products);
    }
}

public record GetProductsQuery(int PageNumber, int PageSize) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);
