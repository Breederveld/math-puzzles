using System.Collections.Generic;
using System.Linq;

namespace MathPuzzles
{
	public class DominoRing
	{
		#region Fields

		private KeyValuePair<int, int>[] _dominos;

		#endregion Fields

		#region Constructors

		public DominoRing()
		{
			_dominos = Enumerable.Range(1, 6)
				.SelectMany(top => Enumerable.Range(top, 7 - top)
				.Select(bottom => new KeyValuePair<int, int>(top, bottom)))
				.ToArray();
		}

		#endregion Constructors

		#region Methods

		public IEnumerable<KeyValuePair<int, int>[]> FindBestSets(int numberOfItemsInRing)
		{
			int numberOfDominos = _dominos.Length;

			int maxSum = 0;
			List<int[]> maxPositionsSets = new List<int[]>();
			foreach (int[] dominosPositions in DataHelper.GetDistinctOrderedSets(numberOfItemsInRing - 1, numberOfDominos - 2, 0)
				.Select(set => set.Union(new[] { numberOfDominos - 1 }).ToArray()))
			{
				KeyValuePair<int, int>[] fractions = new KeyValuePair<int, int>[numberOfItemsInRing];
				for (int pos = 0; pos < numberOfItemsInRing; pos++)
				{
					KeyValuePair<int, int> currentDominos = _dominos[dominosPositions[pos]];
					fractions[pos] = currentDominos;
				}
				if (CheckProducts(fractions))
				{
					int innerSum = GetInnerSum(fractions);
					if (innerSum >= maxSum)
					{
						if (innerSum > maxSum)
						{
							maxSum = innerSum;
							maxPositionsSets.Clear();
						}
						maxPositionsSets.Add(dominosPositions);
					}
				}
			}
			return maxPositionsSets
				.Select(set => set.Select(pos => _dominos[pos]).ToArray());
		}

		private bool CheckProducts(IList<KeyValuePair<int, int>> fractions)
		{
			int product = 0;
			for (int i = 0; i < fractions.Count / 2; i++)
			{
				int newProduct = fractions[i * 2].Key * fractions[i * 2].Value * fractions[i * 2 + 1].Key * fractions[i * 2 + 1].Value;
				if (product == 0)
					product = newProduct;
				else if (newProduct != product)
					return false;
			}
			return true;
		}

		private int GetInnerSum(IEnumerable<KeyValuePair<int, int>> fractions)
		{
			return fractions.Sum(kv => kv.Key);
		}

		#endregion Methods
	}
}