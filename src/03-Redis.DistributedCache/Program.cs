using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using _03_Redis.DistributedCache.Data;
using _03_Redis.DistributedCache.Model;
using Microsoft.Extensions.Caching.Distributed;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "Mousa:";
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseHttpsRedirection();
var jsonOption = new JsonSerializerOptions
{
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};
var productGroup = app.MapGroup("/products");
productGroup.MapGet("/", async (IDistributedCache cache) =>
{
    var sw = new Stopwatch();
    sw.Start();
    var cachedJson =await cache.GetStringAsync("products");
    if (!string.IsNullOrEmpty(cachedJson))
    {
        var result = JsonSerializer.Deserialize<List<Product>>(cachedJson, jsonOption);
        if (result is { Count: > 0 }) return Results.Ok(new { Source = "Cache (Hit)", ElapsedMs = sw.ElapsedMilliseconds, Data = result });
    }
    var products = FakeDatabase.GetSampleProducts();
    if (products.Count == 0) return Results.NotFound();
    var json = JsonSerializer.Serialize(products,jsonOption);
    await cache.SetStringAsync("products",json,new DistributedCacheEntryOptions
    {
        SlidingExpiration = TimeSpan.FromSeconds(10)
    });
    return Results.Ok(new { Source = "Database (Miss)", ElapsedMs = (long)sw.Elapsed.TotalMilliseconds, Data = products });
});
productGroup.MapGet("/{id:int}", async (int id,IDistributedCache cache) =>
{
    var sw = new Stopwatch();
    sw.Start();
    
    var key = $"product:{id}";
    var cachedJson = await cache.GetStringAsync(key);
    if (!string.IsNullOrEmpty(cachedJson))
    {
        var result = JsonSerializer.Deserialize<Product>(cachedJson, jsonOption);
        if (result is not null ) return Results.Ok(new{Source= "Cache (Hit)", ElapsedMs = sw.ElapsedMilliseconds, Data =result});
    }
    var product = FakeDatabase.GetById(id);
    if (product is null) return Results.NotFound();
    var json = JsonSerializer.Serialize(product, jsonOption);
    await cache.SetStringAsync(key, json, new DistributedCacheEntryOptions
    {
        SlidingExpiration = TimeSpan.FromSeconds(10)
    });
    return Results.Ok(new { Source = "Database (Miss)", ElapsedMs = (long)sw.Elapsed.TotalMilliseconds, Data = product });

});
productGroup.MapPatch("/{id:int}", async (int id, IDistributedCache cache) =>
{
    
    var result = FakeDatabase.ChangeTitle(id,"New Title");
    if (result is null) return Results.NotFound();
    await cache.RemoveAsync($"product:{id}");
    await cache.RemoveAsync($"products");
    return Results.Ok(result);
});
app.Run();