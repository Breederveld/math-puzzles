using System;
using System.Collections.Generic;
using System.Linq;

namespace MathPuzzles
{
    public class EdgePuzzle<TSide>
    {
        private readonly Func<TSide, TSide> _getMatch;
        private readonly Piece[] _pieces;
        private readonly int _width;
        private readonly int _sides;

        public EdgePuzzle(Func<TSide, TSide> getMatch, params TSide[][] pieces)
        {
            _getMatch = getMatch;
            _pieces = pieces.Select((sides, idx) => new Piece(idx, sides)).ToArray();
            _width = (int)Math.Sqrt(_pieces.Length);
            _sides = _pieces[0].Sides.Length;
        }

        public IEnumerable<Piece[]> Solve()
        {
            var dbg = GetOptions(new int[0]).ToArray();
            var options = new Stack<int[]>();
            options.Push(new int[0]);
            while (options.Count != 0)
            {
                var current = options.Pop();
                foreach (var option in GetOptions(current))
                {
                    var arr = new int[current.Length + 1];
                    current.CopyTo(arr, 0);
                    arr[current.Length] = option;
                    if (arr.Length == _pieces.Length)
                    {
                        yield return arr.Select(idx => GetPiece(idx)).ToArray();
                    }
                    else
                    {
                        options.Push(arr);
                    }
                }
            }
        }

        private IEnumerable<int> GetOptions(int[] current)
        {
            var remaining = Enumerable.Range(0, _pieces.Length)
                .Except(current.Select(i => i / _sides));
            var expected = GetSides(current, current.Length).Select(_getMatch).ToArray();
            foreach (var pieceIdx in remaining)
            {
                for (int rotation = 0; rotation < _sides; rotation++)
                {
                    if (IsValid(_pieces[pieceIdx], rotation, expected))
                    {
                        yield return pieceIdx * _sides + rotation;
                    }
                }
            }
        }

        private IEnumerable<TSide> GetSides(int[] current, int position)
        {
            var x = position % _width;
            var y = position / _width;

            yield return y > 0
                ? GetSide(current, position - _width, 2)
                : default;
            yield return x < _width - 1
                ? GetSide(current, position + 1, 3)
                : default;
            yield return y < _width - 1
                ? GetSide(current, position + _width, 0)
                : default;
            yield return x > 0
                ? GetSide(current, position - 1, 1)
                : default;
        }

        private TSide GetSide(int[] current, int position, int side)
        {
            if (position >= current.Length)
            {
                return default;
            }
            var piece = _pieces[current[position] / _sides];
            return piece.Sides[(current[position] + side) % _sides];
        }

        private bool IsValid(Piece piece, int rotation, TSide[] expected)
        {
            for (int i = 0; i < _sides; i++)
            {
                if (!expected[i].Equals(default(TSide)) && !expected[i].Equals(piece.Sides[(i + rotation) % _sides]))
                {
                    return false;
                }
            }
            return true;
        }

        private Piece GetPiece(int pieceVal)
        {
            var piece = _pieces[pieceVal / _sides];
            var rotation = pieceVal % _sides;
            var sides = piece.Sides
                .Skip(rotation)
                .Take(_sides - rotation)
                .Concat(piece.Sides.Take(rotation))
                .ToArray();
            return new Piece(piece.Number, sides);
        }

        public class Piece
        {
            public Piece(int number, TSide[] sides)
            {
                Number = number;
                Sides = sides;
            }

            public int Number { get; set; }
            public TSide[] Sides { get; set; }
        }

        public void PrintGrid(Piece[] grid)
        {
            Action<TSide> printFixed = (side) =>
            {
                var str = side.ToString();
                if (str.Length > 20)
                    Console.Write(str.Substring(0, 20));
                else
                    Console.Write(str.PadRight(20));
            };

            for (var y = 0; y < grid.Length / _width; y++)
            {
                for (var layer = 0; layer < 3; layer++)
                {
                    for (var x = 0; x < _width; x++)
                    {
                        var idx = y * _width + x;
                        switch (layer)
                        {
                            case 0:
                                Console.Write("           ");
                                printFixed(grid[idx].Sides[0]);
                                Console.Write("           ");
                                break;
                            case 1:
                                printFixed(grid[idx].Sides[3]);
                                Console.Write(" ");
                                printFixed(grid[idx].Sides[1]);
                                Console.Write(" ");
                                break;
                            case 2:
                                Console.Write("           ");
                                printFixed(grid[idx].Sides[2]);
                                Console.Write("           ");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}