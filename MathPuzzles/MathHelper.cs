using System;
using System.Collections.Generic;
using System.Linq;

namespace MathPuzzles
{
	public static class MathHelper
	{
		public static IEnumerable<int> GetPrimes(int max)
		{
			return Enumerable.Range(0, (int)Math.Sqrt(max)).Aggregate(
				Enumerable.Range(2, max).ToList(),
				(result, index) =>
				{
					result.RemoveAll(i => i > result[index] && i % result[index] == 0);
					return result;
				});
		}

		public static int GetGreatestCommonDivisor(IEnumerable<int> numbers)
		{
			return numbers.Aggregate(GetGreatestCommonDivisor);
		}

		public static int GetGreatestCommonDivisor(int l, int r)
		{
			return r == 0 ? l : GetGreatestCommonDivisor(r, l % r);
		}

		public static int GetLeastCommonMultiple(IEnumerable<int> numbers)
		{
			return numbers.Aggregate(GetLeastCommonMultiple);
		}

		public static int GetLeastCommonMultiple(int l, int r)
		{
			return (l*r)/GetGreatestCommonDivisor(l, r);
		}
	}
}