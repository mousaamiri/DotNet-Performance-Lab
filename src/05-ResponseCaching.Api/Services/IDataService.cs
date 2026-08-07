using System.Text.Json;
using _05_ResponseCaching.Api.Models;

namespace _05_ResponseCaching.Api.Services;

public interface IDataService
{
    Task<List<Order>> GetOrdersAsync();
    Task<Order?> GetOrderAsync(int id);
}
public class DataService : IDataService
{
    private readonly string _jsonFileUrl = Path.Combine(AppContext.BaseDirectory, "Data", "orders.json");

    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
    public async Task<List<Order>> GetOrdersAsync()
    {
        var content = await File.ReadAllTextAsync(_jsonFileUrl);
        var orders = JsonSerializer.Deserialize<List<Order>>(content,_jsonOptions);
        return orders ?? [];
    }
    public async Task<Order?> GetOrderAsync(int id)
    {
        var orders = await GetOrdersAsync();
        await Task.Delay(2000);
        return orders.FirstOrDefault(o => o.Id == id);
    }
}