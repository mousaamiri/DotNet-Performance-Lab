using BenchmarkDotNet.Attributes;

namespace _08_Benchmarks.Console;

[MemoryDiagnoser]
public class TaskVsValueTaskBenchmarks
{
    private List<int> _cachedData = [1, 2, 3, 4, 5];
    [Benchmark(Baseline = true)]
    public async Task<List<int>> TaskFromCachedData()
    {
        return await Task.FromResult(_cachedData);
    }

    [Benchmark]
    public async ValueTask<List<int>> ValueTaskFromCachedData()
    {
        return await ValueTask.FromResult(_cachedData);
    }

    [Benchmark]
    public async Task<List<int>> TaskWithRealAsyncWork()
    {
        await Task.Delay(1); // Simulate some asynchronous work
        return _cachedData;
    }
}