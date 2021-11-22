using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MathPuzzles
{
	class Old
	{
		public static void Run()
		{
			//Squares(150);
			//UnitFractions(3, 200, 21, 34);
			//UnitFractions(4, 200, 34, 55);
			//Rectangles27();
			//Recursive2013(2013, 2013);
			TriangularTiling();
		}

		static void Squares(int max)
		{
			List<int> notFound = new List<int>();
			int maxSqr = (int)Math.Sqrt(max);
			for (int i = 0; i <= max; i++)
			{
				bool found = false;
				for (int a = 0; a < maxSqr; a++)
				{
					for (int b = 0; b < maxSqr; b++)
					{
						for (int c = 0; c < maxSqr; c++)
						{
							if (a * a + b * b + c * c == i)
							{
								Console.WriteLine("{0}, {1}, {2} = {3}", a, b, c, i);
								found = true;
							}
						}
					}
				}

				if (!found)
					notFound.Add(i);
			}

			Console.WriteLine("Not found: " + string.Join(", ", notFound));
		}

		static void UnitFractions(int depth, int max, int den, int div)
		{
			int[] nums = new int[depth];
			int curDepth = 0;

			while (true)
			{
				while (nums[curDepth] > max)
				{
					curDepth--;
					if (curDepth < 0)
						return;
				}
				nums[curDepth]++;
				while (curDepth < depth - 1)
				{
					int last = nums[curDepth];
					curDepth++;
					nums[curDepth] = last + 1;
				}

				while (nums[curDepth] < max)
				{
					float result = 0;
					for (int i = 0; i < depth; i++)
						result += (float)div / nums[i];
					if (result == den)
					{
						Console.WriteLine(string.Join(", ", nums));
						break;
					}

					// Check here...
					nums[curDepth]++;
				}

				curDepth--;
			}

			for (int a = 1; a < max; a++)
			{
				for (int b = a + 1; b < max; b++)
				{
					for (int c = b + 1; c < max; c++)
					{
						if ((float)div / a + (float)div / b + (float)div / c == den)
						{
							Console.WriteLine("{0}, {1}, {2}", a, b, c);
							break;
						}
					}
				}
			}
		}


		private static void Recursive2013(int iterations, float startNumber)
		{
			float result = startNumber;
			for (int i = 1; i <= iterations; i++)
			{
				result = (1 + result) / (1 - result);
				Console.WriteLine("After {0} iterations: {1}", i, result);
				Console.ReadKey();
			}
			Console.WriteLine("After {0} iterations: {1}", iterations, result);
		}

		private static void TriangularTiling()
		{
			ObservableCollection<int[]> foundSizes = new ObservableCollection<int[]>();
			foundSizes.CollectionChanged += (sender, args) =>
			{
				foreach (int[] sizes in args.NewItems)
				{
					if (CheckTiling(63, sizes))
					{
						File.AppendAllText(@"tiling.txt", string.Join(", ", sizes) + "\n");
						Debugger.Break();
					}
				}
			};
			//			FindTriangularTiling(15, 63 * 63, new Stack<int>(), foundSizes, 39);
			FindTriangularTiling(15 - 3, 63 * 63, new Stack<int>(new[] { 39, 20, 19 }), foundSizes, 39);
		}

		private static void FindTriangularTiling(int order, int tiles, Stack<int> sizes, Collection<int[]> foundSizes, int maxSize)
		{
			if (order <= 0)
			{
				if (tiles == 0)
					foundSizes.Add(sizes.ToArray());
				return;
			}
			//if (order == 1)
			//	Debugger.Break();

			int min = Math.Max((int)Math.Ceiling((double)(order / 2)), 1);
			int max = tiles;
			for (int i = 1; i <= order / 2; i++)
				max -= 2 * i * i;
			if (order % 2 == 1)
				max -= (int)Math.Pow((order + 1) / 2, 2);
			max = (int)Math.Min(Math.Floor(Math.Sqrt(max) + 1), maxSize);

			for (int size = max; size >= min; size--)
			{
				int remainingTiles = tiles - size * size;
				if (remainingTiles < 0)
					continue;
				if (sizes.Count(s => s == size) == 2)
					continue;
				sizes.Push(size);
				FindTriangularTiling(order - 1, remainingTiles, sizes, foundSizes, size);
				sizes.Pop();
			}
		}

		private static bool CheckTiling(int outerSize, int[] innerSizes)
		{
			bool[][] tiles = new bool[outerSize][];
			for (int y = 0; y < outerSize; y++)
				tiles[y] = new bool[y * 2 + 1];

			return CheckTiling(tiles, outerSize, innerSizes, innerSizes.Length - 1);
		}

		private static bool CheckTiling(bool[][] tiles, int outerSize, int[] innerSizes, int pos)
		{
			if (pos < 0)
				return true;

			int size = innerSizes[pos];
			for (int y = 0; y <= outerSize - size; y++)
			{
				int width = y * 2 + 1;
				for (int x = 0; x < width; x++)
				{
					if (CheckNoCollision(tiles, x, y, size))
					{
						ApplyTiles(tiles, x, y, size, true);
						if (CheckTiling(tiles, outerSize, innerSizes, pos - 1))
							return true;
						ApplyTiles(tiles, x, y, size, false);
					}
					if (CheckNoCollisionRev(tiles, x, y, size))
					{
						ApplyTilesRev(tiles, x, y, size, true);
						if (CheckTiling(tiles, outerSize, innerSizes, pos - 1))
							return true;
						ApplyTilesRev(tiles, x, y, size, false);
					}
				}
			}
			return false;
		}

		private static bool CheckNoCollision(bool[][] tiles, int posX, int posY, int size)
		{
			if (tiles.Length <= posY + size)
				return false;
			for (int y = 0; y < size; y++)
			{
				int width = y * 2 + 1;
				for (int x = 0; x < width; x++)
				{
					if (tiles[y + posY].Length <= x + posX)
						return false;
					if (tiles[y + posY][x + posX])
						return false;
				}
			}
			return true;
		}

		private static bool CheckNoCollisionRev(bool[][] tiles, int posX, int posY, int size)
		{
			if (tiles.Length <= posY + size)
				return false;
			for (int y = 0; y < size; y++)
			{
				int width = (size - y - 1) * 2 + 1;
				for (int x = 0; x < width; x++)
				{
					if (tiles[y + posY].Length <= x + posX)
						return false;
					if (tiles[y + posY][x + posX])
						return false;
				}
			}
			return true;
		}

		private static void ApplyTiles(bool[][] tiles, int posX, int posY, int size, bool value)
		{
			for (int y = 0; y < size; y++)
			{
				int width = y * 2 + 1;
				for (int x = 0; x < width; x++)
				{
					tiles[y + posY][x + posX] = value;
				}
			}
		}

		private static void ApplyTilesRev(bool[][] tiles, int posX, int posY, int size, bool value)
		{
			for (int y = 0; y < size; y++)
			{
				int width = (size - y - 1) * 2 + 1;
				for (int x = 0; x < width; x++)
				{
					tiles[y + posY][x + posX] = value;
				}
			}
		}
	}
}