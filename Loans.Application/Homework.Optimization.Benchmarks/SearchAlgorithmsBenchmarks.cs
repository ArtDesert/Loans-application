using BenchmarkDotNet.Attributes;

namespace Homework.Optimization.Benchmarks;

[MemoryDiagnoser(false)]
public class SearchAlgorithmsBenchmarks
{
	public int Left { get; set; } = 1;
	public int Middle { get; set; }
	public int Right { get; set; }
	public int[] Array { get; set; }

	[Params(10, 1_000, 1_000_000, 1_000_000_000 )]
    public int Size { get; set; }

    [GlobalSetup]
	public void Setup()
	{
		Middle = Size / 2;
		Right = Size;
		Array = Enumerable.Range(1, Size).ToArray();
	}

	[Benchmark]
	public void FindElementInSortedArrayBenchmark()
	{
		SearchAlgorithms.FindElementInSortedArray(Array, Left);
		SearchAlgorithms.FindElementInSortedArray(Array, Middle);
		SearchAlgorithms.FindElementInSortedArray(Array, Right);
	}

	[Benchmark]
	public void FindElementInSortedArrayOptimizedBenchmark()
	{
		SearchAlgorithms.FindElementInSortedArrayOptimized(Array, Left);
		SearchAlgorithms.FindElementInSortedArrayOptimized(Array, Middle);
		SearchAlgorithms.FindElementInSortedArrayOptimized(Array, Right);
	}
}
