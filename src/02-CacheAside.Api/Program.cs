using _02_CacheAside.Api.Data;
using Microsoft.Extensions.Caching.Memory;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();

var cache = builder.Services.BuildServiceProvider().GetRequiredService<IMemoryCache>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
var productGroup = app.MapGroup("/products");
productGroup.MapGet("/", () =>
{
    var products = cache.GetOrCreate($"Products", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
        return ProductSeeder.GetSampleProducts();
    });
    return products;
});
productGroup.MapGet("/{id:int}", (int id) =>
{
    var product = cache.GetOrCreate($"Product:{id}", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
        return ProductSeeder.GetSampleProducts().FirstOrDefault(p => p.Id == id);
    });
    return product;
});
productGroup.MapPatch("/{id:int}", (int id) =>
{
    var success = ProductSeeder.ChangeTitle(id, "Changed");
    if (!success) return Results.NotFound();
    cache.Remove($"Product:{id}");
    return Results.Ok($"Product {id} changed");
});

app.Run();