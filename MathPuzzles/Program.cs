using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MathPuzzles
{
	class Program
	{
        static void Main(string[] args)
        {
            //SylverCoinage game = new SylverCoinage(new[] { 9 });
            //int nextMove = game.GetBestNextMove();
            //PhilosCube game = new PhilosCube();
            //var blockOrientations = game.FindBlockOrientations();
            //var amount = blockOrientations.Count();
            //foreach (int[] blocks in blockOrientations)
            //{
            //	game.WriteBlockOrientations(blocks);
            //}

            //PrimeSums game = new PrimeSums();
            //foreach (int num in game.FindSmallestSet())
            //{
            //	Console.WriteLine(num);
            //}

            //FourLockers game = new FourLockers();
            //int score = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //	score += game.MakeAttempt();
            //}
            //// Estimated score total: 2 2/3 * 1000 ~= 2666

            //DominoFractions game = new DominoFractions();
            //KeyValuePair<int, int>[][] results =
            //	game.FindSetsWithProductOf(3, 1, 4)
            //		.Select(lst => lst.Union(new[] { new KeyValuePair<int, int>(3, 1) }).ToArray())
            //	.Union(game.FindSetsWithProductOf(6, 2, 4)
            //		.Select(lst => lst.Union(new[] { new KeyValuePair<int, int>(6, 2) }).ToArray()))
            //	.Distinct(new FunctionEqualityComparer<KeyValuePair<int, int>[]>((x, y) =>
            //		{
            //			return x.Take(4).All(x1 => y.Take(4).Any(y1 => x1.Key == y1.Key && x1.Value == y1.Value));
            //		}))
            //	.ToArray();
            ////results = results
            ////	.Where(res => res.Any(x => (x.Key == 2 && x.Value == 5) || (x.Key == 5 && x.Value == 2)))
            ////	.ToArray();
            //foreach (KeyValuePair<int, int>[] fractions in results)
            //{
            //int lastIndex = fractions.Length - 1;
            //for (int i = 0; i <= lastIndex; i++)
            //{
            //	Console.Write("{0}/{1}", fractions[i].Key, fractions[i].Value);
            //	if (i < lastIndex - 1)
            //		Console.Write(" * ");
            //	else if (i < lastIndex)
            //		Console.Write(" = ");
            //}
            //Console.WriteLine();
            //}

            //DominoFractions game = new DominoFractions();
            //KeyValuePair<int, int>[][] results =
            //	game.FindSetsWithSumOf(6, 1, 10)
            //	//.Union(game.FindSetsWithSumOf(6, 1, 3))
            //	//.Union(game.FindSetsWithSumOf(6, 1, 4))
            //	//.Union(game.FindSetsWithSumOf(6, 1, 5))
            //	//.Union(game.FindSetsWithSumOf(6, 1, 6))
            //	.Select(lst => lst.Union(new[] { new KeyValuePair<int, int>(6, 5) }).ToArray())
            //	.Distinct(new FunctionEqualityComparer<KeyValuePair<int, int>[]>((x, y) =>
            //	{
            //		return x.Length == y.Length && x.Take(x.Length - 1).All(x1 => y.Take(x.Length - 1).Any(y1 => x1.Key == y1.Key && x1.Value == y1.Value));
            //	}))
            //	.ToArray();
            ////results = results
            ////	.Where(res => res.Any(x => (x.Key == 2 && x.Value == 5) || (x.Key == 5 && x.Value == 2)))
            ////	.ToArray();
            //foreach (KeyValuePair<int, int>[] fractions in results)
            //{
            //	int lastIndex = fractions.Length - 1;
            //	for (int i = 0; i <= lastIndex; i++)
            //	{
            //		Console.Write("{0}/{1}", fractions[i].Key, fractions[i].Value);
            //		if (i < lastIndex - 1)
            //			Console.Write(" + ");
            //		else if (i < lastIndex)
            //			Console.Write(" = ");
            //	}
            //	Console.WriteLine();
            //}

            //DominoRing game = new DominoRing();
            //KeyValuePair<int, int>[][] result = game.FindBestSets(8).ToArray();
            //result = result
            //	.Distinct(new FunctionEqualityComparer<KeyValuePair<int, int>[]>((x, y) =>
            //		{
            //			return x.All(x1 => y.Any(y1 => x1.Key == y1.Key && x1.Value == y1.Value));
            //		}))
            //	.ToArray();

            //var game = new BookCypher();
            //var text = new[]
            //{
            //    //"discovery",
            //    //"$ 20",
            //    "seeds of rebellion tequilla aquavit chamomile lemon pear mustard dill",
            //    "hemingway's last chapter absinthe lemon dry vermouth violette bubbles",
            //    "let's oaxaca around the world mezcal harissa blood orange ra's al hanout",
            //    "epilogue absinthe chartreuse lime maraschino",
            //    "scarlet cheddar cheddar cheese infused gin white balsamic fig honey",
            //};
            //var text = new[]
            //{
            //    "discovery",
            //    "$ 20",
            //    "seeds of rebellion",
            //    "hemingway's last chapter",
            //    "let's oaxaca around the world",
            //    "epilogue",
            //    "scarlet cheddar",
            //};
            //foreach (var option in game.GetOptions(text, new[] { 10, 8, 14, 16, 5, 4, 2, 9, 6, 4, 10 }))
            //{
            //    Console.WriteLine(option);
            //}

            //var game = new Iban();
            //Console.WriteLine(game.CalculateChecksum("INGB0001234567", "NL"));
            //Console.WriteLine(game.ValidateChecksum("GB82WEST12345698765432"));
            //Console.WriteLine(game.ValidateChecksum("NL72RABO0138722145"));
            //Console.WriteLine(game.CalculateChecksum("RABO0138722145", "NL"));
            //Console.WriteLine(game.CalculateChecksum("ABRA0135702468", "NL"));

            //var game = new MasterMind<int>();
            //var hints = new[]
            //{
            //    new MasterMind<int>.Hint(0, 1, 3, 5, 4, 8),
            //    new MasterMind<int>.Hint(1, 1, 4, 6, 7, 1),
            //    new MasterMind<int>.Hint(0, 2, 3, 7, 8, 1),
            //    new MasterMind<int>.Hint(0, 3, 8, 3, 9, 7),
            //    new MasterMind<int>.Hint(0, 0, 5, 1, 2, 4),
            //    new MasterMind<int>.Hint(0, 1, 2, 9, 3, 4),
            //    new MasterMind<int>.Hint(1, 0, 5, 1, 3, 6),
            //};
            //// Expected: 9876
            //Console.WriteLine(string.Join(", ", game.Solve(hints)));
            //var game = new MasterMind<string>();
            //var hints = new[]
            //{
            //    new MasterMind<string>.Hint(1, 0, "R", "R", "R", "R"),
            //    new MasterMind<string>.Hint(1, 0, "O", "O", "O", "O"),
            //    new MasterMind<string>.Hint(0, 0, "Y", "Y", "Y", "Y"),
            //    new MasterMind<string>.Hint(1, 0, "G", "G", "G", "G"),
            //    new MasterMind<string>.Hint(1, 0, "B", "B", "B", "B"),
            //    new MasterMind<string>.Hint(2, 2, "R", "O", "G", "B"),
            //    new MasterMind<string>.Hint(2, 1, "O", "Y", "B", "G"),
            //    new MasterMind<string>.Hint(0, 2, "P", "B", "Y", "O"),
            //};
            //// Expected: ROBG
            //Console.WriteLine(string.Join(", ", game.Solve(hints)));

            //var game = new EdgePuzzle<EdgePuzzleSide>(side => side == default ? default : (EdgePuzzleSide)(((int)side + 3) % 8 + 1),
            //    new[] { EdgePuzzleSide.WhiteT, EdgePuzzleSide.OrangeT, EdgePuzzleSide.BlueF, EdgePuzzleSide.RedT },
            //    new[] { EdgePuzzleSide.BlueT, EdgePuzzleSide.WhiteT, EdgePuzzleSide.RedT, EdgePuzzleSide.OrangeF },
            //    new[] { EdgePuzzleSide.WhiteF, EdgePuzzleSide.RedF, EdgePuzzleSide.OrangeT, EdgePuzzleSide.BlueF },
            //    new[] { EdgePuzzleSide.BlueT, EdgePuzzleSide.OrangeF, EdgePuzzleSide.WhiteT, EdgePuzzleSide.RedT },
            //    new[] { EdgePuzzleSide.RedF, EdgePuzzleSide.WhiteF, EdgePuzzleSide.BlueF, EdgePuzzleSide.OrangeT },
            //    new[] { EdgePuzzleSide.OrangeT, EdgePuzzleSide.RedF, EdgePuzzleSide.RedF, EdgePuzzleSide.WhiteT },
            //    new[] { EdgePuzzleSide.WhiteF, EdgePuzzleSide.OrangeT, EdgePuzzleSide.BlueF, EdgePuzzleSide.RedF },
            //    new[] { EdgePuzzleSide.BlueT, EdgePuzzleSide.WhiteT, EdgePuzzleSide.RedF, EdgePuzzleSide.OrangeF },
            //    new[] { EdgePuzzleSide.RedT, EdgePuzzleSide.BlueF, EdgePuzzleSide.OrangeF, EdgePuzzleSide.WhiteF });
            //var solutions = game.Solve().ToArray();

            //var game = new EdgePuzzle<HolographicEdgePuzzleSide>(side => side,
            //    new[] { HolographicEdgePuzzleSide.Corner, HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Grid, HolographicEdgePuzzleSide.Jagged },
            //    new[] { HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Jagged, HolographicEdgePuzzleSide.Swirls },
            //    new[] { HolographicEdgePuzzleSide.Grid, HolographicEdgePuzzleSide.Radiant, HolographicEdgePuzzleSide.Radiant, HolographicEdgePuzzleSide.Parallel },
            //    new[] { HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Triangles, HolographicEdgePuzzleSide.Radiant, HolographicEdgePuzzleSide.Swirls },
            //    new[] { HolographicEdgePuzzleSide.Corner, HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Corner },
            //    new[] { HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Corner, HolographicEdgePuzzleSide.Corner, HolographicEdgePuzzleSide.Arrows },
            //    new[] { HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Corner },
            //    new[] { HolographicEdgePuzzleSide.Corner, HolographicEdgePuzzleSide.Jagged, HolographicEdgePuzzleSide.Grid, HolographicEdgePuzzleSide.Corner },
            //    new[] { HolographicEdgePuzzleSide.Grid, HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Triangles, HolographicEdgePuzzleSide.Swirls },
            //    new[] { HolographicEdgePuzzleSide.Triangles, HolographicEdgePuzzleSide.Corner, HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Arrows },
            //    new[] { HolographicEdgePuzzleSide.Parallel, HolographicEdgePuzzleSide.Triangles, HolographicEdgePuzzleSide.Triangles, HolographicEdgePuzzleSide.Jagged },
            //    new[] { HolographicEdgePuzzleSide.Triangles, HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Arrows },
            //    new[] { HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Grid, HolographicEdgePuzzleSide.Corner, HolographicEdgePuzzleSide.Arrows },
            //    new[] { HolographicEdgePuzzleSide.Radiant, HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Triangles },
            //    new[] { HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Corner, HolographicEdgePuzzleSide.Swirls },
            //    new[] { HolographicEdgePuzzleSide.Corner, HolographicEdgePuzzleSide.Swirls, HolographicEdgePuzzleSide.Arrows, HolographicEdgePuzzleSide.Swirls });
            //var solutions = game.Solve().ToArray();

            var game = new EdgePuzzle<SeafoodPuzzleSide>(side => side == default ? default : (SeafoodPuzzleSide)(((int)side + 3) % 8 + 1),
                new[] { SeafoodPuzzleSide.PrawnHead, SeafoodPuzzleSide.LobsterTail, SeafoodPuzzleSide.CrabTail, SeafoodPuzzleSide.CrabTail },
                new[] { SeafoodPuzzleSide.CrabTail, SeafoodPuzzleSide.PrawnTail, SeafoodPuzzleSide.HermitHead, SeafoodPuzzleSide.LobsterTail },
                new[] { SeafoodPuzzleSide.LobsterTail, SeafoodPuzzleSide.HermitHead, SeafoodPuzzleSide.PrawnTail, SeafoodPuzzleSide.CrabTail },
                new[] { SeafoodPuzzleSide.CrabHead, SeafoodPuzzleSide.HermitHead, SeafoodPuzzleSide.HermitTail, SeafoodPuzzleSide.PrawnTail },
                new[] { SeafoodPuzzleSide.HermitTail, SeafoodPuzzleSide.PrawnHead, SeafoodPuzzleSide.CrabHead, SeafoodPuzzleSide.LobsterHead },
                new[] { SeafoodPuzzleSide.PrawnTail, SeafoodPuzzleSide.LobsterHead, SeafoodPuzzleSide.HermitTail, SeafoodPuzzleSide.CrabHead },
                new[] { SeafoodPuzzleSide.HermitHead, SeafoodPuzzleSide.LobsterHead, SeafoodPuzzleSide.CrabHead, SeafoodPuzzleSide.PrawnHead },
                new[] { SeafoodPuzzleSide.LobsterTail, SeafoodPuzzleSide.CrabHead, SeafoodPuzzleSide.PrawnTail, SeafoodPuzzleSide.HermitHead },
                new[] { SeafoodPuzzleSide.PrawnTail, SeafoodPuzzleSide.LobsterHead, SeafoodPuzzleSide.HermitHead, SeafoodPuzzleSide.LobsterTail });
            var solutions = game.Solve().ToArray();
            game.PrintGrid(solutions[0]);


            //// Zebra puzzle: https://en.wikipedia.org/wiki/Zebra_Puzzle
            //var nextHouses = new Func<int, string[]>(i => new[] { i - 1, i + 1 }.Select(i => i.ToString()).ToArray());
            //var housesExcept = new Func<int, string[]>(i => Enumerable.Range(1, 5).Except(new[] { i }).Select(i => i.ToString()).ToArray());
            //var game = new LogicGridPuzzle(
            //new[]
            //{
            //    // The Englishman lives in the red house.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Nationality", "Englishman")
            //        .Add("Color", "Red"),
            //    // The Spaniard owns the dog.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Nationality", "Spaniard")
            //        .Add("Pet", "Dog"),
            //    // Coffee is drunk in the green house.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Drink", "Coffee")
            //        .Add("Color", "Green"),
            //    // The Ukrainian drinks tea.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Nationality", "Ukrainian")
            //        .Add("Drink", "Tea"),
            //    // The green house is immediately to the right of the ivory house.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Color", "Green")
            //        .Add("House", context =>
            //        {
            //            var options = context.Filter("Color", "Ivory").GetOptions("House");
            //            return options.Select(s => new string(new[] { (char)(s[0] + 1) })).ToArray();
            //        }),
            //    new LogicGridPuzzle.Fact()
            //        .Add("Color", "Ivory")
            //        .Add("House", context =>
            //        {
            //            var options = context.Filter("Color", "Green").GetOptions("House");
            //            return options.Select(s => new string(new[] { (char)(s[0] - 1) })).ToArray();
            //        }),
            //    // The Old Gold smoker owns snails.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Smoke", "Old Gold")
            //        .Add("Pet", "Snails"),
            //    // Kools are smoked in the yellow house.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Smoke", "Kools")
            //        .Add("Color", "Yellow"),
            //    // Milk is drunk in the middle house.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Drink", "Milk")
            //        .Add("House", "3"),
            //    // The Norwegian lives in the first house.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Nationality", "Norwegian")
            //        .Add("House", "1"),
            //    // The man who smokes Chesterfields lives in the house next to the man with the fox.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Smoke", "Chesterfields")
            //        .Add("House", context =>
            //        {
            //            var options = context.Filter("Pet", "Fox").GetOptions("House");
            //            return options.SelectMany(s => new[] { new string(new[] { (char)(s[0] - 1) }), new string(new[] { (char)(s[0] + 1) }) }).ToArray();
            //        }),
            //    new LogicGridPuzzle.Fact()
            //        .Add("Pet", "Fox")
            //        .Add("House", context =>
            //        {
            //            var options = context.Filter("Smoke", "Chesterfields").GetOptions("House");
            //            return options.SelectMany(s => new[] { new string(new[] { (char)(s[0] - 1) }), new string(new[] { (char)(s[0] + 1) }) }).ToArray();
            //        }),
            //    // Kools are smoked in the house next to the house where the horse is kept.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Smoke", "Kools")
            //        .Add("House", context =>
            //        {
            //            var options = context.Filter("Pet", "Horse").GetOptions("House");
            //            return options.SelectMany(s => new[] { new string(new[] { (char)(s[0] - 1) }), new string(new[] { (char)(s[0] + 1) }) }).ToArray();
            //        }),
            //    new LogicGridPuzzle.Fact()
            //        .Add("Pet", "Horse")
            //        .Add("House", context =>
            //        {
            //            var options = context.Filter("Smoke", "Kools").GetOptions("House");
            //            return options.SelectMany(s => new[] { new string(new[] { (char)(s[0] - 1) }), new string(new[] { (char)(s[0] + 1) }) }).ToArray();
            //        }),
            //    // The Lucky Strike smoker drinks orange juice.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Smoke", "Lucky Strike")
            //        .Add("Drink", "Orange Juice"),
            //    // The Japanese smokes Parliaments.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Nationality", "Japanese")
            //        .Add("Smoke", "Parliament"),
            //    // The Norwegian lives next to the blue house.
            //    new LogicGridPuzzle.Fact()
            //        .Add("Nationality", "Norwegian")
            //        .Add("House", context =>
            //        {
            //            var options = context.Filter("Color", "Blue").GetOptions("House");
            //            return options.SelectMany(s => new[] { new string(new[] { (char)(s[0] - 1) }), new string(new[] { (char)(s[0] + 1) }) }).ToArray();
            //        }),
            //    new LogicGridPuzzle.Fact()
            //        .Add("Color", "Blue")
            //        .Add("House", context =>
            //        {
            //            var options = context.Filter("Nationality", "Norwegian").GetOptions("House");
            //            return options.SelectMany(s => new[] { new string(new[] { (char)(s[0] - 1) }), new string(new[] { (char)(s[0] + 1) }) }).ToArray();
            //        }),
            //}
            //, new Dictionary<string, string[]>
            //{
            //    ["House"] = new[] { "1", "2", "3", "4", "5" },
            //    ["Color"] = new[] { "Yellow", "Blue", "Red", "Ivory", "Green" },
            //    ["Nationality"] = new[] { "Norwegian", "Ukrainian", "Englishman", "Spaniard", "Japanese" },
            //    ["Drink"] = new[] { "Water", "Tea", "Milk", "Orange Juice", "Coffee" },
            //    ["Smoke"] = new[] { "Kools", "Chesterfields", "Old Gold", "Lucky Strike", "Parliament" },
            //    ["Pet"] = new[] { "Fox", "Horse", "Snails", "Dog", "Zebra" },
            //}
            //);
            //var state = game.Solve();
            //state.Print("House");
            //state.PrintFull();

            Console.WriteLine("done");

            Debugger.Break();
            //Console.ReadKey();
        }

        public enum EdgePuzzleSide
        {
            Default = 0,
            RedF = 1,
            WhiteF = 2,
            OrangeF = 3,
            BlueF = 4,
            RedT = 5,
            WhiteT = 6,
            OrangeT = 7,
            BlueT = 8,
        }

        public enum HolographicEdgePuzzleSide
        {
            Default = 0,
            Jagged,
            Grid,
            Swirls,
            Corner,
            Arrows,
            Parallel,
            Radiant,
            Triangles,
        }

        public enum SeafoodPuzzleSide
        {
            Default = 0,
            CrabHead,
            PrawnHead,
            LobsterHead,
            HermitHead,
            CrabTail,
            PrawnTail,
            LobsterTail,
            HermitTail,
        }
    }
}