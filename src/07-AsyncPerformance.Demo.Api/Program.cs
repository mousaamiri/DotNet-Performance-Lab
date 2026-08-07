using _07_AsyncPerformance.Demo.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IDataService, DataService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
var ordersGroup = app.MapGroup("/orders");
ordersGroup.MapGet("/sync-over-async", (IDataService dataService) =>
{
    // Anti-pattern: blocking on async code can lead to deadlocks and performance issues
    var result = dataService.GetOrdersAsync().Result;
    return result;
});
ordersGroup.MapGet("/proper-async", async (IDataService dataService) =>
{
    var result = await dataService.GetOrdersAsync();
    return result;
});
ordersGroup.MapGet("/valuetask-demo", async (IDataService dataService) =>
{
    
    var result = await dataService.GetOrdersValueTaskAsync();
    return result;
});
app.Run();