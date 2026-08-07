using _06_Compression.Demo.Api.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseResponseCompression();
app.UseHttpsRedirection();

var ordersGroup = app.MapGroup("/orders");
ordersGroup.MapGet("/", (IDataService dataService) => dataService.GetOrdersAsync());


app.Run();