using System;
using System.Collections.Generic;

namespace MathPuzzles
{
	public class FunctionEqualityComparer<T> : IEqualityComparer<T>
	{
		#region Fields

		private readonly Func<T, T, bool> _equalityFunction;

		#endregion Fields

		#region Constructors

		public FunctionEqualityComparer(Func<T, T, bool> equalityFunction)
		{
			_equalityFunction = equalityFunction;
		}

		#endregion Constructors

		#region Methods

		public bool Equals(T x, T y)
		{
			return _equalityFunction(x, y);
		}

		public int GetHashCode(T obj)
		{
			return 0;
		}

		#endregion Methods
	}
}