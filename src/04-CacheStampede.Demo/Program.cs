using System.Diagnostics;
using _04_CacheStampede.Demo.Model;
using _04_CacheStampede.Demo.Services;
using Microsoft.Extensions.DependencyInjection;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

var services = new ServiceCollection();
services.AddScoped<IDataService, DataService>();
services.AddStackExchangeRedisCache(op =>
{
    op.Configuration = "localhost:6379";
    op.InstanceName = "04-CacheStampede.Demo:";
});
var dataService = services.BuildServiceProvider().GetRequiredService<IDataService>();


List<Func<int, Task<Product?>>> tasks = [];

for (var i = 0; i < 50; i++)
{
    tasks.Add(async id=>await dataService.GetProductAsync(id));
}

while (true)
{
    var sw = new Stopwatch();
    sw.Start();
    await Task.WhenAll(tasks.Select(async task =>await task(1)));
    sw.Stop();

    Console.WriteLine("=====================================================");
    Console.WriteLine($"The entire operation was completed in {sw.ElapsedMilliseconds} ms");

    
    Console.WriteLine("=====================================================");
    Console.Write("Do you want to try again ? (y/n)");
    var input = Console.ReadLine();
    Console.Write("");
    Console.WriteLine("=====================================================");
    if(input == "y")
        continue;
    else break;
}