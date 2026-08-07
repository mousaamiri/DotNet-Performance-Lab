using System.Diagnostics;
using _05_ResponseCaching.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddOutputCache();
// to use Redis as the output cache store
builder.Services.AddStackExchangeRedisOutputCache(op =>
{
    op.Configuration = "localhost:6379";
    op.InstanceName = "ResponseCachingDemo_Api:";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
//app.UseResponseCaching(); -> for response caching middleware, but we are using Output Caching middleware instead
app.UseOutputCache();

var ordersGroup = app.MapGroup("/orders");
ordersGroup.MapGet("/", (IDataService dataService) => dataService.GetOrdersAsync());

ordersGroup.MapGet("/{id:int}", async (HttpContext context,IDataService dataService, int id) =>
{
    /*  Set cache control headers for the response  */
    //context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
    //{
    //    Public = true,
    //    MaxAge = TimeSpan.FromSeconds(10)
    //};
    Console.WriteLine("Handler called!");

    var sw = new Stopwatch();
    sw.Start();
    var product = await dataService.GetOrderAsync(id);
    sw.Stop();
    return Results.Ok(new { QueryTime = sw.ElapsedMilliseconds, Result = product });
})
.CacheOutput(op =>
{
    op.Expire(TimeSpan.FromSeconds(10));
    
});
app.Run();
