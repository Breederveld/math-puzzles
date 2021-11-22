using System;
using System.Collections.Generic;

namespace MathPuzzles
{
	class PhilosCube
	{
		#region Fields

		private readonly int[][] _orientations;

		#endregion Fields

		#region Constructors

		public PhilosCube()
		{
			_orientations = new int[6][];
			_orientations[0] = new[] { 1, 2, 3 };
			_orientations[1] = new[] { 1, 3, 2 };
			_orientations[2] = new[] { 2, 1, 3 };
			_orientations[3] = new[] { 2, 3, 1 };
			_orientations[4] = new[] { 3, 1, 2 };
			_orientations[5] = new[] { 3, 2, 1 };
		}

		#endregion Constructors

		#region Methods

		public IEnumerable<int[]> FindBlockOrientations()
		{
			// 27 blocks with 6 possible _orientations.
			// Solution is when all blocks in each line have different values for their respective length and there is a fitting situation.

			// Shortcuts to implement:
			// - Remove all symmetries (mirror and rotation)
			int[] blocks = new int[27];

			int currentBlock = 0;
			blocks[currentBlock] = -1;
			do
			{
				do
				{
					blocks[currentBlock]++;
				} while (blocks[currentBlock] < 6 && !CheckOrientations(blocks, currentBlock));

				if (blocks[currentBlock] > 5)
				{
					currentBlock--;
				}
				else
				{
					if (currentBlock == 26)
					{
						if (CheckFit(blocks))
							yield return blocks;
					}
					else
					{
						currentBlock++;
						blocks[currentBlock] = -1;
					}
				}

				if (currentBlock < 0)
				{
					yield break;
				}

			} while (true);
		}

		public void WriteBlockOrientations(int[] blocks)
		{
			for (int i = 0; i < 27; i += 3)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int j = 0; j < 3; j++)
					{
						int[] currentOrientation = _orientations[blocks[i + j]];
						for (int x = 0; x < 3; x++)
						{
							Console.Write(currentOrientation[0] > x && currentOrientation[1] > y ? "o" : " ");
						}
						Console.Write(" ");
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}
		}

		private bool CheckOrientations(int[] blocks, int currentBlock)
		{
			int[] currentOrientation = _orientations[blocks[currentBlock]];

			// Check if all widths in all directions differ.
			for (int x = 1; x <= currentBlock % 3; x++)
			{
				int inlineBlock = currentBlock - x;
				int[] inlineBlockOrientation = _orientations[blocks[inlineBlock]];
				if (currentOrientation[0] == inlineBlockOrientation[0])
					return false;
			}
			for (int y = 1; y <= (currentBlock % 9) / 3; y++)
			{
				int inlineBlock = currentBlock - y * 3;
				int[] inlineBlockOrientation = _orientations[blocks[inlineBlock]];
				if (currentOrientation[1] == inlineBlockOrientation[1])
					return false;
			}
			for (int z = 1; z <= currentBlock / 9; z++)
			{
				int inlineBlock = currentBlock - z * 9;
				int[] inlineBlockOrientation = _orientations[blocks[inlineBlock]];
				if (currentOrientation[2] == inlineBlockOrientation[2])
					return false;
			}

			return true;
		}

		private bool CheckFit(int[] blocks)
		{
			bool[,,] blockFills = new bool[8,8,8];

			for (int blockIdx = 0; blockIdx < 27; blockIdx++)
			{
				int z = 0;
				bool emptyFound = true;
				int offsetX = -1, offsetY = -1, offsetZ = -1;

				// Locate the next start position.
				for (int y = 0; y < 8 && offsetZ < 0; y++)
				{
					for (int x = 0; x < 8 && offsetZ < 0; x++)
					{
						if (!blockFills[x, y, z])
						{
							if (emptyFound)
							{
								offsetX = x;
								offsetY = y;
								offsetZ = z;
								break;
							}
							emptyFound = true;
						}
						else
							emptyFound = false;
					}
					emptyFound = true;
				}

				if (offsetZ < 0)
					return false;

				int[] currentOrientation = _orientations[blocks[blockIdx]];
				for (int x = 0; x < currentOrientation[0]; x++)
					for (int y = 0; y < currentOrientation[1]; y++)
						for (z = 0; z < currentOrientation[2]; z++)
						{
							if (x + offsetX >= 8 || y + offsetY >= 8 || z + offsetZ >= 8)
								return false;
							blockFills[x + offsetX, y + offsetY, z + offsetZ] = true;
						}
			}

			return true;
		}

		#endregion Methods
	}
}