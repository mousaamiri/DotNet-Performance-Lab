using _08_Benchmarks.Console;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TaskVsValueTaskBenchmarks>();
BenchmarkRunner.Run<MemoryVsRedisCacheBenchmarks>();