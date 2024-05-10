using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();

        await SeedAsync(dbContext);
    }

    private static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        await SeedCustomersAsync(dbContext);
        await SeedProductsAsync(dbContext);
        await SeedOrdersAndItemsAsync(dbContext);
    }

    private static async Task SeedCustomersAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Customers.AnyAsync())
        {
            await dbContext.Customers.AddRangeAsync(InitialData.Customers);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedProductsAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Products.AnyAsync())
        { 
            await dbContext.Products.AddRangeAsync(InitialData.Products);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedOrdersAndItemsAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Orders.AnyAsync())
        {
            await dbContext.Orders.AddRangeAsync(InitialData.OrdersWithItems);
            await dbContext.SaveChangesAsync();
        }
    }

    private static class InitialData
    {
        private static Guid[] customerIds = [
            new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
            new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")
        ];
        private static Guid[] productIds = [
            new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
            new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
            new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"),
            new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")
        ];

        public static IEnumerable<Customer> Customers =>
        new List<Customer>
        {
            Customer.Create(customerIds[0], "mehmet", "mehmet@gmail.com"),
            Customer.Create(customerIds[1], "john", "john@gmail.com")
        };

        public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(productIds[0], "IPhone X", 500),
            Product.Create(productIds[1], "Samsung 10", 400),
            Product.Create(productIds[2], "Huawei Plus", 650),
            Product.Create(productIds[3], "Xiaomi Mi", 450)
        };

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Create("mehmet", "ozkaya", "mehmet@gmail.com", "Bahcelievler No:4", "Turkey", "Istanbul", "38050");
                var address2 = Address.Create("john", "doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

                var payment1 = Payment.Create("mehmet", "5555555555554444", "12/28", "355", 1);
                var payment2 = Payment.Create("john", "8885555555554444", "06/30", "222", 2);

                var order1 = Order.Create(
                    Guid.NewGuid(),
                    customerIds[0],
                    "ORD_1",
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment1);
                order1.AddItem(productIds[0], 2, 500);
                order1.AddItem(productIds[1], 1, 400);

                var order2 = Order.Create(
                    Guid.NewGuid(),
                    customerIds[1],
                    "ORD_2",
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment2);
                order2.AddItem(productIds[2], 1, 650);
                order2.AddItem(productIds[3], 2, 450);

                return new List<Order> { order1, order2 };
            }
        }
    }
}
