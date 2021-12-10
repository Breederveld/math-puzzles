using System;
using System.Collections.Generic;
using System.Linq;

namespace MathPuzzles
{
    /// <summary>
    /// Logical grid puzzle solver, using facts to deduce a unique source to target mapping.
    /// https://en.wikipedia.org/wiki/Logic_puzzle#Logic_grid_puzzles
    /// Well known for the "Zebra puzzle".
    /// </summary>
    public class LogicGridPuzzle
    {
        private readonly ICollection<Fact> _facts;
        private State _state;

        public LogicGridPuzzle(ICollection<Fact> facts, IDictionary<string, string[]> propertyOptions)
        {
            _facts = facts;
            _state = new State(propertyOptions);
        }

        public LogicGridPuzzle(ICollection<Fact> facts)
            : this(facts, ExtractPropertyOptions(facts))
        {
        }

        public State Solve()
        {
            // Apply the facts.
            var facts = _facts.ToList();
            var knownSets = new HashSet<(string key0, string value0, string key1, string value1)>();
            var count = 0;
            do
            {
                count = _state.Count;

                // Apply all remaining facts.
                foreach (var fact in facts.ToArray())
                {
                    // Try to apply the fact.
                    var conditions = fact.Conditions
                        .Select(kv => (kv.Key, kv.Value(_state)?.Distinct()?.ToArray()))
                        .Where(kv => kv.Item2 != null)
                        .ToDictionary(kv => kv.Key, kv => kv.Item2);
                    if (conditions.Count < 2)
                    {
                        continue;
                    }

                    // Get the subset of items that match all conditions.
                    var subset = _state.Items.Where(item => conditions.All(condition => condition.Value.Contains(item[condition.Key]))).ToArray();
                    // Trim the conditions for this new context.
                    foreach (var key in conditions.Keys)
                    {
                        conditions[key] = conditions[key].Intersect(subset.Select(item => item[key]).Distinct()).ToArray();
                    }
                    // Get the superset of items that match any fixed condition.
                    var superset = _state.Items.Where(item => conditions.Any(condition => condition.Value.Length == 1 && condition.Value.Contains(item[condition.Key]))).ToArray();
                    // Remove superset - subset.
                    _state.Items = _state.Items.Except(superset).Concat(subset).ToList();

                    // Remove fact if it doesn't give any new information.
                    if (conditions.All(c => c.Value.Length == 1))
                    {
                        facts.Remove(fact);
                    }
                }

                // Find all new known sets and add them as facts.
                foreach (var key0 in _state.Items[0].Keys)
                {
                    foreach (var option0 in _state.GetOptions(key0))
                    {
                        var state = _state.Filter(key0, option0);
                        foreach (var key1 in _state.Items[0].Keys)
                        {
                            if (key1 == key0)
                            {
                                continue;
                            }
                            if (state.TryGetOption(key1, out var option1))
                            {
                                var set = (key0, option0, key1, option1);
                                if (!knownSets.Contains(set))
                                {
                                    knownSets.Add(set);
                                    facts.Add(new Fact().Add(key0, option0).Add(key1, option1));
                                }
                            }
                        }
                    }
                }
            }
            while (count > _state.Count);

            return _state;
        }

        private static IDictionary<string, string[]> ExtractPropertyOptions(ICollection<Fact> facts)
        {
            var propertyOptions = new Dictionary<string, List<string>>();
            var state = new State(null, new List<IDictionary<string, string>>());
            foreach (var fact in facts)
            {
                foreach (var condition in fact.Conditions)
                {
                    if (!propertyOptions.ContainsKey(condition.Key))
                    {
                        propertyOptions[condition.Key] = new List<string>();
                    }
                    var values = condition.Value(state);
                    if (values != null)
                    {
                        propertyOptions[condition.Key].AddRange(values);
                    }
                }
            }
            return propertyOptions
                .ToDictionary(kv => kv.Key, kv => kv.Value.Distinct().ToArray());
        }

        public class State
        {
            private readonly IDictionary<string, string[]> _propertyOptions;

            public State(IDictionary<string, string[]> propertyOptions)
            {
                _propertyOptions = propertyOptions;

                var itemMapping = new Dictionary<string, Func<int, int>>();
                var modulo = 1;
                var createModulo = new Func<int, int, Func<int, int>>((modulo, divide) => new Func<int, int>(i => (i % modulo) / divide));
                foreach (var kv in propertyOptions)
                {
                    var newModulo = modulo * kv.Value.Length;
                    itemMapping[kv.Key] = createModulo(newModulo, modulo);
                    modulo = newModulo;
                }
                var itemCount = propertyOptions.Aggregate(1, (acc, kv) => acc * kv.Value.Length);
                Items = Enumerable.Range(0, itemCount)
                    .Select(item => (IDictionary<string, string>)propertyOptions.ToDictionary(kv => kv.Key, kv => kv.Value[itemMapping[kv.Key](item)]))
                    .ToList();
            }

            public State(IDictionary<string, string[]> propertyOptions, IList<IDictionary<string, string>> items)
            {
                _propertyOptions = propertyOptions;

                Items = items;
            }

            public int Count => Items.Count;

            public IList<IDictionary<string, string>> Items { get; set; }

            public State Filter(string key, string value)
                => Filter(dict => dict[key] == value);

            public State FilterNot(string key, string value)
                => Filter(dict => dict[key] != value);

            public State Filter(Func<IDictionary<string, string>, bool> filter)
            {
                var filtered = Items.Where(filter).ToList();
                return new State(_propertyOptions, filtered);
            }

            public IEnumerable<string> GetOptions(string key)
            {
                return Items
                    .Select(dict => dict[key])
                    .Distinct();
            }

            public bool TryGetOption(string key, out string value)
            {
                var options = GetOptions(key).Take(2).ToArray();
                if (options.Length != 1)
                {
                    value = null;
                    return false;
                }
                value = options[0];
                return true;
            }

            public void Print(string axis)
            {
                var options = GetOptions(axis).OrderBy(s => s).ToArray();
                var fit = new Func<string, string>(s => s.PadRight(9).Substring(0, 9) + ' ');
                Console.Write(fit(axis));
                foreach (var option in options)
                {
                    Console.Write(fit(option));
                }
                Console.WriteLine();
                foreach (var key in Items[0].Keys.Except(new[] { axis }))
                {
                    Console.Write(fit(key));
                    foreach (var option in options)
                    {
                        var values = Items
                            .Where(dict => dict[axis] == option)
                            .Select(dict => dict[key])
                            .Distinct()
                            .ToArray();
                        if (values.Length == 1)
                        {
                            Console.Write(fit(values[0]));
                        }
                        else
                        {
                            Console.Write(fit($"#{values.Length}"));
                        }
                    }
                    Console.WriteLine();
                }
            }

            public void PrintFull()
            {
                var fit = new Func<string, int, string>((s, len) => s.PadRight(len - 1).Substring(0, len - 1) + ' ');
                var options = _propertyOptions
                    .OrderBy(kv => kv.Key == "House" ? 6 : kv.Key == "Pet" ? 5 : kv.Key == "Smoke" ? 4 : kv.Key == "Drink" ? 3 : kv.Key == "Nationality" ? 2 : 1)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.OrderBy(x => x).ToArray());
                Console.Write(fit(string.Empty, 4));
                foreach (var key in options.Keys.Skip(1).Reverse())
                {
                    Console.Write(" | ");
                    foreach (var option in options[key])
                    {
                        Console.Write(new string(new[] { key[0], option[0], ' ' }));
                    }
                }
                Console.WriteLine();
                Console.WriteLine(new String('-', 80));

                foreach (var key0 in options.Keys.Reverse().Skip(1).Reverse())
                {
                    foreach (var option0 in options[key0])
                    {
                        Console.Write(fit(new string(new[] { key0[0], option0[0], ' ' }), 4));
                        foreach (var key1 in options.Keys.Reverse().TakeWhile(key => key != key0))
                        {
                            Console.Write(" | ");
                            foreach (var option1 in options[key1])
                            {
                                var items = Items
                                    .Where(dict => dict[key0] == option0 && dict[key1] == option1)
                                    .ToArray();

                                if (items.Length == 0)
                                {
                                    Console.Write(" / ");
                                }
                                else
                                {
                                    Console.Write(" . ");
                                }
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine(new String('-', 80));
                }
            }
        }

        public class Fact
        {
            public IDictionary<string, Func<State, ICollection<string>>> Conditions { get; set; } = new Dictionary<string, Func<State, ICollection<string>>>();

            public Fact Add(string key, Func<State, ICollection<string>> fact)
            {
                Conditions.Add(key, fact);
                return this;
            }

            public Fact Add(string key, string fact)
            {
                Conditions.Add(key, _ => new[] { fact });
                return this;
            }
        }
    }
}