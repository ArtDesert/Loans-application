using BenchmarkDotNet.Running;

namespace Homework.Optimization.Benchmarks;

internal class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run<SearchAlgorithmsBenchmarks>();
    }
}
