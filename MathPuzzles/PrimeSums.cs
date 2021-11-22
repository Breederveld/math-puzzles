using System;
using System.Collections.Generic;
using System.Linq;

namespace MathPuzzles
{
	public class PrimeSums
	{
		#region Fields

		private int[] _primes;

		#endregion Fields

		#region Constructors

		public PrimeSums()
		{
			_primes = MathHelper.GetPrimes(999).Where(num => num > 99).ToArray();
		}

		#endregion Constructors

		#region Methods

		public IEnumerable<int> FindSmallestSet()
		{
			int smallestSum = int.MaxValue;
			List<int> smallesSet = null;
			foreach (IEnumerable<int> sets in GetSets(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }))
			{
				List<int> setList = sets.ToList();
				int sum = Sum(setList);
				if (sum < smallestSum)
				{
					smallestSum = sum;
					smallesSet = setList;
				}
			}
			return smallesSet;
		}

		private IEnumerable<IEnumerable<int>> GetSets(int[] availableNumbers)
		{
			foreach (int prime in _primes)
			{
				List<int> numbers = new List<int>();
				int ij = prime;
				do
				{
					numbers.Add(ij % 10);
					ij /= 10;
				} while ( ij > 0);

				// Check for duplicates in the prime itself.
				if (numbers.GroupBy(num => num)
					.Any(grp => grp.Count() > 1))
					continue;

				// Check if all numbers are available.
				if (!numbers.All(num => availableNumbers.Contains(num)))
					continue;

				// Get the new available numbers.
				int[] newAvailableNumbers = availableNumbers
					.Except(numbers)
					.ToArray();

				if (newAvailableNumbers.Length == 0)
					yield return new[] { prime };
				else
				{
					foreach (IEnumerable<int> innerSet in GetSets(newAvailableNumbers))
					{
						yield return innerSet.Union(new[] { prime });
					}
				}
			}
		}

		private int Sum(IEnumerable<int> numbers)
		{
			int total = 0;
			foreach (int number in numbers)
				total += number;
			return total;
		}

		#endregion Methods
	}
}