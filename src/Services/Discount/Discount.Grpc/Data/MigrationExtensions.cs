using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Grpc.Data
{
    public static class MigrationExtensions
    {
        public static IApplicationBuilder UseMigrations<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            dbContext.Database.MigrateAsync();

            return app;
        }
    }
}
