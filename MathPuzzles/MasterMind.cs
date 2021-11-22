using System;
using System.Collections.Generic;
using System.Linq;

namespace MathPuzzles
{
    public class MasterMind<T>
    {
        private int _positionCount;
        private ICollection<T> _possibleValues;

        public MasterMind(int positionCount = 0, ICollection<T> possibleValues = null)
        {
            _positionCount = positionCount;
            _possibleValues = possibleValues;
        }

        public ICollection<T> Solve(ICollection<Hint> hints)
        {
            if (_positionCount == 0)
            {
                _positionCount = hints.Max(hint => hint.Guess.Length);
            }

            if (_possibleValues == null)
            {
                _possibleValues = hints.SelectMany(hint => hint.Guess).Distinct().ToArray();
            }

            var remainingValues = Enumerable.Range(0, _positionCount)
                .Select(_ => _possibleValues.ToList())
                .ToArray();


            // Then re-examine each hint to identify new truths.
            var hasUpdated = true;
            while (hasUpdated)
            {
                hasUpdated = false;

                // Try to detect exact matches.
                foreach (var hint in hints)
                {
                    foreach ((var pos, var state) in GetNewStates(hint, remainingValues))
                    {
                        if (hint.GuessStates[pos] != PositionState.Unknown)
                        {
                            continue;
                        }

                        hint.GuessStates[pos] = state;
                        switch (state)
                        {
                            case PositionState.OnPosition:
                                remainingValues[pos].Clear();
                                remainingValues[pos].Add(hint.Guess[pos]);
                                hasUpdated = true;
                                break;

                            case PositionState.OutOfPosition:
                                if (remainingValues[pos].Contains(hint.Guess[pos]))
                                {
                                    remainingValues[pos].Remove(hint.Guess[pos]);
                                    hasUpdated = true;
                                }
                                break;

                            case PositionState.Incorrect:
                                for (var ppos = 0; ppos < _positionCount; ppos++)
                                {
                                    if (remainingValues[ppos].Contains(hint.Guess[pos]))
                                    {
                                        remainingValues[ppos].Remove(hint.Guess[pos]);
                                        hasUpdated = true;
                                    }
                                }
                                break;
                        }
                    }

                    // Find all possible positions the almost values could belong to.
                    var possibleAlmostPositions = Enumerable.Range(0, _positionCount)
                        .Where(pos => hint.GuessStates[pos] == PositionState.OutOfPosition)
                        .ToArray();
                    var almostValues = possibleAlmostPositions.Select(pos => hint.Guess[pos]).Distinct().ToArray();
                    var possibleAlmostTargets = Enumerable.Range(0, _positionCount)
                        .Where(pos => remainingValues[pos].Any(val => almostValues.Contains(val)))
                        .ToArray();

                    // If there are as many targets as values, all targets can only have one of those values.
                    if (possibleAlmostTargets.Length == possibleAlmostPositions.Length)
                    {
                        foreach (var pos in possibleAlmostTargets)
                        {
                            var toRemove = remainingValues[pos].Except(almostValues).ToArray();
                            if (toRemove.Length > 0)
                            {
                                foreach (var remove in toRemove)
                                {
                                    remainingValues[pos].Remove(remove);
                                }
                                hasUpdated = true;
                            }
                        }
                    }

                    // First remove all impossible values.
                    if (hint.OnPosition == hint.GuessStates.Count(state => state == PositionState.OnPosition))
                    {
                        for (int pos = 0; pos < _positionCount; pos++)
                        {
                            if (hint.GuessStates[pos] != PositionState.OnPosition)
                            {
                                remainingValues[pos].Remove(hint.Guess[pos]);
                            }
                        }
                    }
                }
            }

            if (remainingValues.Any(values => values.Count != 1))
                throw new ApplicationException("A solution could not be found");
            return remainingValues.Select(values => values.First()).ToArray();
        }

        private IEnumerable<(int position, PositionState state)> GetNewStates(Hint hint, List<T>[] remainingValues)
        {
            if (hint.OnPosition > 0)
            {
                var possibleExactPositions = Enumerable.Range(0, _positionCount)
                    .Where(pos => remainingValues[pos].Contains(hint.Guess[pos]))
                    .ToArray();
                if (possibleExactPositions.Length == hint.OnPosition)
                {
                    foreach (var pos in possibleExactPositions)
                    {
                        if (remainingValues[pos].Count != 1)
                        {
                            remainingValues[pos].Clear();
                            remainingValues[pos].Add(hint.Guess[pos]);
                            hint.GuessStates[pos] = PositionState.OnPosition;
                            yield return (pos, PositionState.OnPosition);
                        }
                    }
                }
            }

            // Try to detect almost matches.
            if (hint.OutOfPosition > 0)
            {
                // When a possible almost value?
                var possibleAlmostPositions = Enumerable.Range(0, _positionCount)
                    // Not the correct value for known position.
                    .Where(pos => remainingValues[pos].Count != 1 || remainingValues[pos][0].Equals(hint.Guess[pos]))
                    // Value exists in possibilities of other positions.
                    .Where(pos => Enumerable.Range(0, _positionCount).Except(new[] { pos }).Any(ppos => remainingValues[ppos].Contains(hint.Guess[pos])))
                    .ToArray();

                // When the number of possible almosts are the same as expected, we know exactly which ones are incorrectly positioned.
                if (possibleAlmostPositions.Length == hint.OutOfPosition)
                {
                    // We can remove the values that are known to be in the incorrect posisions.
                    foreach (var pos in possibleAlmostPositions)
                    {
                        yield return (pos, PositionState.OutOfPosition);
                    }
                }
            }

            // Try to detect incorrect matches.
            if (hint.GuessStates.Any(state => state == PositionState.Unknown)
                && hint.OnPosition == hint.GuessStates.Count(state => state == PositionState.OnPosition)
                && hint.OutOfPosition == hint.GuessStates.Count(state => state == PositionState.OutOfPosition))
            {
                for (int pos = 0; pos < _positionCount; pos++)
                {
                    if (hint.GuessStates[pos] == PositionState.Unknown)
                    {
                        yield return (pos, PositionState.Incorrect);
                    }
                }
            }
        }

        public class Hint
        {
            public Hint(int onPosition, int outOfPosition, params T[] guess)
            {
                Guess = guess;
                OnPosition = onPosition;
                OutOfPosition = outOfPosition;
                GuessStates = new PositionState[guess.Length];
            }

            public T[] Guess { get; set; }
            public int OnPosition { get; set; }
            public int OutOfPosition { get; set; }
            internal PositionState[] GuessStates { get; set; }
        }

        internal enum PositionState
        {
            Unknown = 0,
            OnPosition,
            OutOfPosition,
            Incorrect,
        }
    }
}