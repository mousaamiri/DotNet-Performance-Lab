using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

var services = new ServiceCollection();
services.AddMemoryCache(op => op.SizeLimit = 80);
var provider = services.BuildServiceProvider();
var cache = provider.GetRequiredService<IMemoryCache>();

var productKey = Guid.NewGuid().ToString();
MemoryCacheEntryOptions? options = null;
CacheType cacheType = CacheType.NoCache;

while (true)
{
    Console.WriteLine("========================");
    Console.WriteLine("Enter from menu : ");
    Console.WriteLine("Get product :                     #1");
    Console.WriteLine("Get product :                     #2");
    Console.WriteLine("Enable absolute cache :            #3");
    Console.WriteLine("Enable sliding cache :             #4");
    Console.WriteLine("Disable cache :                    #5");
    Console.WriteLine("Manual remove from cache:          #6");
    Console.WriteLine("Exit app :                        exit ");
    Console.Write("User input : ");
    string? input = Console.ReadLine();
    Console.WriteLine("========================");
    if (string.IsNullOrEmpty(input))
    {
        InvalidInput();
    }
    else if (input == "1")
    {
        input = await GetRequestFromServer(input, GetProductNameAsync);

    }
    else if (input == "2")
    {
        input = await GetRequestFromServer(input, GetProductsAsync);
    }
    else if (input == "3")
    {
        cacheType = CacheType.Absolute;
        options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10));
        options.SetSize(9);
        options.RegisterPostEvictionCallback(EvictionProductNameReason);
        Console.Clear();
        Console.WriteLine("-------------------");
        Console.WriteLine("---- cache absolute enabled (10 second) --");
        Console.WriteLine("-------------------");
        continue;
    }
    else if (input == "4")
    {
        cacheType = CacheType.Sliding;
        options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10));
        options.SetSize(9);
        options.RegisterPostEvictionCallback(EvictionProductNameReason);
        Console.Clear();
        Console.WriteLine("-------------------");
        Console.WriteLine("---- cache sliding enabled (10 second) --");
        Console.WriteLine("-------------------");
        continue;
    }
    else if (input == "5")
    {
        cacheType = CacheType.NoCache;
        options = null;

        Console.Clear();
        Console.WriteLine("-------------------");
        Console.WriteLine("---- cache disabled --");
        Console.WriteLine("-------------------");
        continue;
    }
    else if (input == "6")
    {
        cache.Remove(productKey);

        Console.Clear();
        Console.WriteLine("-------------------");
        Console.WriteLine("---- removed --");
        Console.WriteLine("-------------------");
        continue;
    }
    else
    {
        Console.WriteLine("Invalid input ");
        Console.WriteLine("Please make your selection again.");
    }
}

async Task<List<string>> GetProductsAsync()
{
    List<string>? products = [];
    var sw = Stopwatch.StartNew();

    for (var i = 0; i < 10; i++)
    {
        var getResult = false;
        string? product = null;
        getResult = (cacheType != CacheType.NoCache) ? cache.TryGetValue($"product{i}", out product) : false;
        if (getResult && product is not null)
        {
            products.Add($"{product} | in {sw.ElapsedMilliseconds} ms");
        }
        else
        {
            await Task.Delay(500);
            product = $"Product #{i} | in {sw.ElapsedMilliseconds} ms";
            products.Add(product);
            if ((cacheType != CacheType.NoCache)) cache.Set($"product{i}", $"Product #{i}", options);
        }

    }
    return products;
}
async Task<List<string>> GetProductNameAsync()
{
    string? productName;
    var sw = Stopwatch.StartNew();
    if (cacheType != CacheType.NoCache)
    {
        if (cache.TryGetValue(productKey, out productName))
            return [$"result : {productName} | time:  {sw.ElapsedMilliseconds}ms"];
    }
    Console.WriteLine("Now, reading from the database (slow) ...");
    await Task.Delay(2000);
    productName = "ASUS laptop";
    if (cacheType != CacheType.NoCache)
    {
        cache.Set(productKey, productName, options);
    }
    return [$"result : {productName} | time:  {sw.ElapsedMilliseconds}ms"];
}

void EvictionProductNameReason(object key, object? value, EvictionReason reason, object? state)
{
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine($"The object with key {key} and value -{value}- was removed from the collection due to -{reason.ToString()}- . Status: -{state}-");
    Console.WriteLine("-------------------------------------------");
}

void InvalidInput()
{
    Console.WriteLine("Invalid input ");
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    Console.Clear();
    return;
}

static async Task<string?> GetRequestFromServer(string? input, Func<Task<List<string>>> request)
{
    while (true)
    {
        Console.WriteLine("start .... ");
        var result1 = await request();
        Console.WriteLine($"result : ");
        foreach (var item in result1.ToList())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("========================");
        while (true)
        {
            Console.WriteLine("Press '1' to request again.");
            Console.WriteLine("Press 'm' to go to the menu.");
            Console.Write("User input : ");
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input ");
                Console.WriteLine("Please make your selection again.");
                continue;
            }
            else if (input == "1")
            {
                break;
            }
            else if (input == "m")
            {
                break;
            }
            else if (input == "cls" || input == "clear")
            {
                Console.Clear();
                continue;
            }
            else
            {
                Console.WriteLine("Invalid input ");
                Console.WriteLine("Please make your selection again.");

                continue;
            }
        }
        if (input == "m")
        {
            Console.Clear();
            break;
        }
    }

    return input;
}
enum CacheType
{
    Absolute,
    Sliding,
    NoCache
}