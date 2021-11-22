using System;
using System.Linq;

namespace MathPuzzles
{
	public class FourLockers
	{
		#region Fields

		private int[] _booksPerDoor;
		private Random _rnd = new Random();

		#endregion Fields

		#region Constructors

		public FourLockers()
		{
			_booksPerDoor = BuildRandomDistribution();
		}

		#endregion Constructors

		#region Methods

		private int[] BuildRandomDistribution()
		{
			int[] list = new int[4];
			do
			{
				for (int i = 0; i < 4; i++)
				{
					int num = i;
					while (num == i)
						num = _rnd.Next(4);
					list[i] = num;
				}
			} while (list.Distinct().Count() < 4);
			return list;
		}

		public int MakeAttempt()
		{
			int score = 0;
			int door = 0;
			int book = _rnd.Next(3) + 1;
			if (_booksPerDoor[door] == book)
				score++;
			door = book;
			book = _rnd.Next(3);
			if (door == book)
				book = 3;
			if (_booksPerDoor[door] == book)
				score++;
			return score + 2;
		}

		#endregion Methods
	}
}