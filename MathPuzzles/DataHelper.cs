using System;
using System.Collections.Generic;
using System.Linq;

namespace MathPuzzles
{
	public static class DataHelper
	{
		public static IEnumerable<int[]> GetDistinctUnorderedSets(int setSize, int setMin, int setMax, int step = 0)
		{
			if (step == 0)
				step = setMin > setMax ? -1 : 1;

			List<int> numbers = Enumerable.Range(0, (setMax - setMin) / step + 1)
				.Select(val => val * step + setMin)
				.ToList();

			return GetDistinctUnorderedSets(setSize, (setMax - setMin) / step)
				.Select(set => set.Select(val => numbers[val]).ToArray());
		}

		public static IEnumerable<int[]> GetDistinctUnorderedSets(int setSize, int maxValue)
		{
			int[] set = new int[setSize];
			for (int i = 0; i < setSize; i++)
				set[i] = i;
			yield return set.ToArray();

			int position = setSize - 1;
			while (position >= 0)
			{
				set[position]++;
				if (set[position] + (setSize - position - 1) <= maxValue)
				{
					for (int i = position + 1; i < setSize; i++)
						set[i] = set[position] + i - position;
					yield return set.ToArray();
					position = setSize - 1;
				}
				else
					position--;
			}
		}

		public static IEnumerable<int[]> GetDistinctOrderedSets(int setSize, int setMin, int setMax, int step = 0)
		{
			if (step == 0)
				step = setMin > setMax ? -1 : 1;

			List<int> numbers = Enumerable.Range(0, (setMax - setMin) / step + 1)
				.Select(val => val * step + setMin)
				.ToList();

			return GetDistinctOrderedSets(setSize, (setMax - setMin) / step)
				.Select(set => set.Select(val => numbers[val]).ToArray());
		}

		public static IEnumerable<int[]> GetDistinctOrderedSets(int setSize, int maxValue)
		{
			int[] set = new int[setSize];
			for (int i = 0; i < setSize; i++)
				set[i] = i;
			yield return set.ToArray();

			int position = setSize - 1;
			while (position >= 0)
			{
				set[position]++;
				if (set[position] > maxValue)
					position--;
				else if (!set.Take(position).Contains(set[position]))
				{
					if (position == setSize - 1)
						yield return set.ToArray();
					else
					{
						position++;
						set[position] = 0;
					}
				}
			}
		}

	}
}