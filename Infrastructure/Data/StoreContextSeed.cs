using System.Text.Json;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    private const string PathTemplate = "../Infrastructure/Data/SeedData/{0}.json";

    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            await SeedDataAsync(context, context.ProductTypes, "types");
            await SeedDataAsync(context, context.ProductBrands, "brands");
            await SeedDataAsync(context, context.Products, "products");
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex, "An error occured during context seeding");
        }
    }

    private static async Task SeedDataAsync<TData>(StoreContext context, DbSet<TData> table, string file) where TData : class
    {
        if (table.Any())
            return;

        var text = await File.ReadAllTextAsync(string.Format(PathTemplate, file));
        var items = JsonSerializer.Deserialize<IEnumerable<TData>>(text);

        if(items == null)
            return;

        await context.AddRangeAsync(items);
        await context.SaveChangesAsync();
    }
}