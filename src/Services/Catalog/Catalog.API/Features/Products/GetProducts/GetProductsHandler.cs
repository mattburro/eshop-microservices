﻿using Catalog.API.Models;
using Shared.CQRS;

namespace Catalog.API.Features.Products.GetProducts;

internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}

public record GetProductsQuery : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);
