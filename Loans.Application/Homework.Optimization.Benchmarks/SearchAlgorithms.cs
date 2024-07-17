namespace Homework.Optimization.Benchmarks;

public static class SearchAlgorithms
{
	/// <summary>
	/// Найти элемент в отсортированном массиве целых чисел. Сложность O(n) по времени, O(1) по памяти.
	/// </summary>
	/// <remarks>
	/// Алгоритм не выполняет проверку, что массив отсортирован.
	/// </remarks>
	/// <param name="array">Отсортированный массив целых чисел.</param>
	/// <param name="value">Искомое число.</param>
	/// <returns>Искомое число. Если его нет в массиве, то -1.</returns>
	public static int FindElementInSortedArray(int[] array, int value)
	{
		for (var index = 0; index < array.Length; index++)
		{
			var item = array[index];
			if (item == value)
			{
				return index;
			}
		}

		return -1;
	}

	/// <summary>
	/// Найти элемент в отсортированном массиве целых чисел. Сложность O(log n) по времени, O(1) по памяти. 
	/// </summary>
	/// <remarks>
	/// Алгоритм выполняет проверку, что массив отсортирован.
	/// </remarks>
	/// <param name="array">Отсортированный массив целых чисел.</param>
	/// <param name="value">Искомое число.</param>
	/// <returns>Искомое число. Если его нет в массиве, то -1.</returns>
	public static int FindElementInSortedArrayOptimized(int[] array, int value)
	{
		int a = 0;
		int b = array.Length;
		while (a < b)
		{
			int middle = (a + b) / 2;
			int curValue = array[middle];
			if (curValue < value)
			{
				a = middle;
			}
			else if (curValue > value)
			{
				b = middle;
			}
			else
			{
				return middle;
			}
		}
		return -1;
	}
}
