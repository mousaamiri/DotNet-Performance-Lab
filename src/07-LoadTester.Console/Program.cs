using System.Diagnostics;

const int concurrency = 250;
const string syncOverAsyncUrl = "https://localhost:7164/orders/sync-over-async";
const string properAsyncUrl = "https://localhost:7164/orders/proper-async";
const string valuetaskDemoUrl = "https://localhost:7164/orders/valuetask-demo";

Console.WriteLine($"Sync over async requests result : ");
await SingleRequests(concurrency, syncOverAsyncUrl);

Console.WriteLine($"Proper async requests result : ");
await SingleRequests(concurrency, properAsyncUrl);

Console.WriteLine($"ValueTask demo requests result : ");
await SingleRequests(concurrency, valuetaskDemoUrl);
return;

static async Task SingleRequests(int time, string url)
{
    var httpClient = new HttpClient();

    var stopwatch = new Stopwatch();
    stopwatch.Start();

    var tasks = new List<Task>();
    for (var i = 0; i < time; i++)
    {
        tasks.Add(httpClient.GetAsync(url));
    }


    await Task.WhenAll(tasks);
    stopwatch.Stop();
    Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
}