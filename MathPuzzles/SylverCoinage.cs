using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPuzzles
{
	public class SylverCoinage
	{
		#region Fields

		private readonly IEnumerable<int> _initialPicks;

		#endregion Fields

		#region Constructors

		public SylverCoinage(IEnumerable<int> initialPicks)
		{
			_initialPicks = initialPicks;
		}

		#endregion Constructors

		#region Methods

		public int GetBestNextMove()
		{
			List<int> bestPath;
			int nextMove = FindWinningMove(new Stack<int>(_initialPicks), out bestPath);
			return nextMove;
		}

		private int FindWinningMove(Stack<int> picks, out List<int> bestPath)
		{
			List<int> remainingMoves = RemainingMoves(picks.ToList()).ToList();
			bestPath = null;
			if (remainingMoves.Count == 0)
				return 1;

			foreach (int move in remainingMoves)
			{
				picks.Push(move);
				int nextMove = FindWinningMove(picks, out bestPath);
				picks.Pop();
				if (nextMove == 1)
				{
					if (picks.Count%2 == 1)
					{
						bestPath = picks.ToList();
						return move;
					}
					return 0;
				}
			}
			return 1;
		}

		private IEnumerable<int> RemainingMoves(IList<int> picks)
		{
			int max = 100;

			List<int> invalidMoves = new List<int>();
			invalidMoves.Add(0);
			foreach (int pick in picks.OrderByDescending(n => n))
			{
				bool foundNew;
				do
				{
					foundNew = false;
					foreach (int invalidMove in invalidMoves.ToList())
					{
						int choice = invalidMove;
						while (choice < max)
						{
							choice += pick;
							if (!invalidMoves.Contains(choice))
							{
								invalidMoves.Add(choice);
								foundNew = true;
							}
						}
					}
				} while (foundNew);
			}
			for (int i = 2; i <= max; i++)
				if (!invalidMoves.Contains(i))
					yield return i;
		}

		/*
		private bool IsValidMove(int move, IList<int> picks)
		{
			var allPrimeFactors = picks
				.SelectMany(GetPrimeFactors)
				.ToList();
			var movePrimeFactors = GetPrimeFactors(move);
			int missingFactors = 0;
			foreach (int factor in movePrimeFactors)
			{
				if (!allPrimeFactors.Contains(factor))
				{
					missingFactors++;
					if (missingFactors > 1)
						return true;
				}
				allPrimeFactors.Remove(factor);
			}
			return false;
		}
		*/


		/*
		private IEnumerable<int> GetPrimeFactors(int n)
		{
			int d = 2;
			while (n > 1)
			{
				while (n%d == 0)
				{
					yield return d;
					n /= d;
				}
				d++;
				if (d*d > n)
				{
					if (n > 1)
						yield return n;
					yield break;
				}
			}
		}
		*/

		#endregion Methods
	}
}