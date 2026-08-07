using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace _08_Benchmarks.Console;

[MemoryDiagnoser]
public class MemoryVsRedisCacheBenchmarks
{
    private const string Key = "benchmark_product";
    private readonly List<int> _data = [1, 2, 3, 4, 5];
    private IMemoryCache _memoryCache = null!;
    private IDistributedCache _redisCache = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var memoryService = new ServiceCollection();
        memoryService.AddMemoryCache();
        _memoryCache = memoryService.BuildServiceProvider().GetRequiredService<IMemoryCache>();
        _memoryCache.Set(Key, _data);

        var redisService = new ServiceCollection();
        redisService.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "Benchmarks:";
        });
        _redisCache = redisService.BuildServiceProvider().GetRequiredService<IDistributedCache>();
        var json = JsonSerializer.Serialize(_data);
        _redisCache.SetString(Key, json);
    }

    [Benchmark(Baseline = true)]
    public List<int>? MemoryCache_Get()
    {
        _memoryCache.TryGetValue(Key, out List<int>? value);
        return value;
    }
    [Benchmark]
    public List<int>? RedisCache_Get()
    {
        var json = _redisCache.GetString(Key);
        return json is not null ? JsonSerializer.Deserialize<List<int>>(json) : null;
    }
}