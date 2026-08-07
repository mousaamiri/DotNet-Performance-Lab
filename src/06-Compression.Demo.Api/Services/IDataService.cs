using System.Text.Json;
using _06_Compression.Demo.Api.Models;

namespace _06_Compression.Demo.Api.Services;

public interface IDataService
{
    Task<List<Order>> GetOrdersAsync();
}
public class DataService : IDataService
{
    private readonly string _jsonFileUrl = Path.Combine(AppContext.BaseDirectory, "Data", "orders.json");

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
 
}