using System.Collections.Concurrent;
using System.Text.Json;
using _04_CacheStampede.Demo.Model;
using Microsoft.Extensions.Caching.Distributed;

namespace _04_CacheStampede.Demo.Services;

public interface IDataService
{
    Task<Product?> GetProductAsync(int id);
}
public class DataService(IDistributedCache cache) : IDataService
{
    private readonly string _dataSourceUrl = Path.Combine(AppContext.BaseDirectory, "Data", "products.json");
    private static int _dbRefCounter = 0;
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> KeyLocks = [];
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
    private async Task<List<Product>> GetProductsAsync()
    {
        await Task.Delay(2000);
        var json = await File.ReadAllTextAsync(_dataSourceUrl);
        var product = JsonSerializer.Deserialize<List<Product>>(json, _serializerOptions) ?? [];

        return product;
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        var key = $"1product:{id}";
        var product = await GetFromCache(key);
        if (product is not null) return product;


        var keyLock = KeyLocks.GetOrAdd(key, _ => new SemaphoreSlim(1, 2));
        await keyLock.WaitAsync();
        
        try
        {
            product = await GetFromCache(key);
            if (product is not null) return product;

            await Task.Delay(2000);
            var products = await GetProductsAsync();
            product = products.FirstOrDefault(p => p.Id == id);
            if (product is not null)
            {
                await cache.SetStringAsync(key, JsonSerializer.Serialize(product), new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(10)
                });
            }

            Interlocked.Increment(ref _dbRefCounter);
            Console.WriteLine(
                $"Product: {product?.Name}, Price: {product?.Price} , -- ({_dbRefCounter}th reference to the database)");
            return product;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
        finally
        {
            keyLock.Release();
        }
    }

    private async Task<Product?> GetFromCache(string key)
    {
        var jsonCached = await cache.GetStringAsync(key);
        if (jsonCached != null)
        {
            var cachedProduct = JsonSerializer.Deserialize<Product>(jsonCached, _serializerOptions);
            Console.WriteLine($"Product: {cachedProduct?.Name}, Price: {cachedProduct?.Price} , -- (Cache (Hit))");
            return cachedProduct;
        }

        return null;
    }

   
}