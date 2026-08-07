using System.Text.Json;
using _07_AsyncPerformance.Demo.Api.Models;

namespace _07_AsyncPerformance.Demo.Api.Services;

public interface IDataService
{
    Task<List<Order>> GetOrdersAsync();
    ValueTask<List<Order>> GetOrdersValueTaskAsync();
}
public class DataService : IDataService
{
    private readonly string _jsonFileUrl = Path.Combine(AppContext.BaseDirectory, "Data", "orders.json");
    private List<Order>? _cachedOrders;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public async Task<List<Order>> GetOrdersAsync()
    {
        var content = await File.ReadAllTextAsync(_jsonFileUrl);
        var orders = JsonSerializer.Deserialize<List<Order>>(content,_jsonOptions);
        return orders ?? [];
    }

    public ValueTask<List<Order>> GetOrdersValueTaskAsync()
    {
        return _cachedOrders is not null
            ? new ValueTask<List<Order>>(_cachedOrders)
            : new ValueTask<List<Order>>(LoadAndCacheAsync());
    }

    private async Task<List<Order>> LoadAndCacheAsync()
    {
        await Task.Delay(2000);
        var content = await File.ReadAllTextAsync(_jsonFileUrl);
        _cachedOrders = JsonSerializer.Deserialize<List<Order>>(content, _jsonOptions);
        return _cachedOrders ?? [];
    }
}