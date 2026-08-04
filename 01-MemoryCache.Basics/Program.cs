using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics;

var services = new ServiceCollection();
services.AddMemoryCache();
var provider = services.BuildServiceProvider();
var cach = provider.GetRequiredService<IMemoryCache>();
MemoryCacheEntryOptions? options = null;
CachType cachType = CachType.NoCach;

while (true)
{
    Console.WriteLine("========================");
    Console.WriteLine("Enter from menu : ");
    Console.WriteLine("Get product : #1");
    Console.WriteLine("Enable absolute cach : #2");
    Console.WriteLine("Enable sliding cach : #3");
    Console.WriteLine("Disable cach : #4");
    Console.WriteLine("Exit app : exit ");
    Console.Write("User input : ");
    string? input = Console.ReadLine();
    Console.WriteLine("========================");
    if (string.IsNullOrEmpty(input))
    {
        InvalidInput();
    }
    else if (input == "1")
    {
        while (true)
        {
            Console.WriteLine("start .... ");
            var result1 = await GetProductNameAsync();
            Console.WriteLine($"result : {result1}");

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
    }
    else if (input == "2")
    {
        cachType = CachType.Absolute;
        options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(5));
        Console.Clear();
        Console.WriteLine("-------------------");
        Console.WriteLine("---- cach absolute enabled (5 second) --");
        Console.WriteLine("-------------------");
        continue;
    }
    else if (input == "3")
    {
        cachType = CachType.Sliding;
        options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(5));

        Console.Clear();
        Console.WriteLine("-------------------");
        Console.WriteLine("---- cach sliding enabled (5 second) --");
        Console.WriteLine("-------------------");
        continue;
    }
    else if (input == "4")
    {
        cachType = CachType.NoCach;
        options = null;
        cach.Dispose();

        Console.Clear();
        Console.WriteLine("-------------------");
        Console.WriteLine("---- cach disabled --");
        Console.WriteLine("-------------------");
        continue;
    }
    else
    {
        Console.WriteLine("Invalid input ");
        Console.WriteLine("Please make your selection again.");
    }

}
void InvalidInput()
{
    Console.WriteLine("Invalid input ");
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    Console.Clear();
    return;
}
async Task<string> GetProductNameAsync()
{
    string? productName;
    var sw = Stopwatch.StartNew();
    if (cachType != CachType.NoCach)
    {
        if (cach.TryGetValue("product-name", out productName))
            return $"result : {productName} | time:  {sw.ElapsedMilliseconds}ms";
    }
    Console.WriteLine("Now, reading from the database (slow) ...");
    await Task.Delay(2000);
    productName = "ASUS laptop";
    if (cachType != CachType.NoCach)
    {
        cach.Set("product-name", productName, options);
    }
    return $"result : {productName} | time:  {sw.ElapsedMilliseconds}ms";
}
enum CachType
{
    Absolute,
    Sliding,
    NoCach
}