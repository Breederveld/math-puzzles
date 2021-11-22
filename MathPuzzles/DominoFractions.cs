using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPuzzles
{
	public class DominoFractions
	{
		#region Fields

		private KeyValuePair<int, int>[] _dominos;

		#endregion Fields

		#region Constructors

		public DominoFractions()
		{
			_dominos = Enumerable.Range(1, 6)
				.SelectMany(top => Enumerable.Range(top, 7 - top)
				.Select(bottom => new KeyValuePair<int, int>(top, bottom)))
				.ToArray();
		}

		#endregion Constructors

		#region Methods

		/// <summary>
		/// Find all dominos sets of the given size that have a product of the given numerator and denominator, without using that particular dominos.
		/// </summary>
		/// <param name="numerator">Numerator of the result.</param>
		/// <param name="denominator">Denominator of the result.</param>
		/// <param name="numberOfDominosInProduct">Number of dominos used in the product.</param>
		/// <returns>All possible sets yielding the given result.</returns>
		public IEnumerable<KeyValuePair<int, int>[]> FindSetsWithProductOf(int numerator, int denominator, int numberOfDominosInProduct)
		{
			KeyValuePair<int, int>[] dominos = _dominos
				.Where(kv => (kv.Key != numerator || kv.Value != denominator) && (kv.Key != denominator || kv.Value != numerator))
				.ToArray();
			
			int numberOfDominos = dominos.Length;

			int[] dominosPositions = new int[numberOfDominosInProduct];
			for (int i = 0; i < numberOfDominosInProduct; i++)
				dominosPositions[i] = i * 2;
			do
			{
				KeyValuePair<int, int>[] fractions = new KeyValuePair<int, int>[numberOfDominosInProduct];
				for (int pos = 0; pos < numberOfDominosInProduct; pos++)
				{
					KeyValuePair<int, int> currentDominos = dominos[dominosPositions[pos]/2];
					if (dominosPositions[pos]%2 == 0)
						fractions[pos] = currentDominos;
					else
						fractions[pos] = new KeyValuePair<int, int>(currentDominos.Value, currentDominos.Key);
				}
				if (CheckProduct(fractions, numerator, denominator))
					yield return fractions;
			} while (TryIncreasePositions(dominosPositions, numberOfDominos*2));
		}

		/// <summary>
		/// Find all dominos sets of the given size that have a product of the given numerator and denominator, without using that particular dominos.
		/// </summary>
		/// <param name="numerator">Numerator of the result.</param>
		/// <param name="denominator">Denominator of the result.</param>
		/// <param name="numberOfDominosInProduct">Number of dominos used in the product.</param>
		/// <returns>All possible sets yielding the given result.</returns>
		public IEnumerable<KeyValuePair<int, int>[]> FindSetsWithSumOf(int numerator, int denominator, int numberOfDominosInProduct)
		{
			KeyValuePair<int, int>[] dominos = _dominos
				.Where(kv => (kv.Key != numerator || kv.Value != denominator) && (kv.Key != denominator || kv.Value != numerator))
				.ToArray();

			int numberOfDominos = dominos.Length;

			int[] dominosPositions = new int[numberOfDominosInProduct];
			for (int i = 0; i < numberOfDominosInProduct; i++)
				dominosPositions[i] = i * 2;
			do
			{
				KeyValuePair<int, int>[] fractions = new KeyValuePair<int, int>[numberOfDominosInProduct];
				for (int pos = 0; pos < numberOfDominosInProduct; pos++)
				{
					KeyValuePair<int, int> currentDominos = dominos[dominosPositions[pos] / 2];
					if (dominosPositions[pos] % 2 == 0)
						fractions[pos] = currentDominos;
					else
						fractions[pos] = new KeyValuePair<int, int>(currentDominos.Value, currentDominos.Key);
				}
				if (CheckSum(fractions, numerator, denominator))
					yield return fractions;
			} while (TryIncreasePositions(dominosPositions, numberOfDominos * 2));
		}

		private bool CheckProduct(KeyValuePair<int, int>[] fractions, int numerator, int denominator)
		{
			int numeratorProduct = 1;
			int denominatorProduct = 1;
			foreach (KeyValuePair<int, int> fraction in fractions)
			{
				numeratorProduct *= fraction.Key;
				denominatorProduct *= fraction.Value;
			}
			return numeratorProduct * denominator == denominatorProduct * numerator;
		}

		private bool CheckSum(KeyValuePair<int, int>[] fractions, int numerator, int denominator)
		{
			int commonDevisor = MathHelper.GetLeastCommonMultiple(fractions.Select(kv => kv.Value).Union(new[] { denominator }));
			int numeratorSum = 0;
			foreach (KeyValuePair<int, int> fraction in fractions)
			{
				numeratorSum += fraction.Key*commonDevisor/fraction.Value;
			}
			return numeratorSum == numerator * commonDevisor / denominator;
		}

		private bool TryIncreasePositions(int[] positions, int maxValue)
		{
			for (int i = positions.Length - 1; i >= 0; i--)
			{
				positions[i]++;
				if (positions[i] + (positions.Length - i) * 2 <= maxValue)
				{
					int basePosition = (positions[i]/2)*2;
					for (int j = 1; j < positions.Length - i; j++)
					{
						positions[i + j] = basePosition + 2*j;
					}
					return true;
				}
			}
			return false;
		}

		#endregion Methods
	}
}