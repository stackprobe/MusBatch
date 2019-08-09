using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class MathTools
	{
		private static Random _random = new Random();

		public static int Random(int modulo)
		{
			return _random.Next(modulo);
		}

		public static int Random(int minval, int maxval)
		{
			return _random.Next(minval, maxval);
		}
	}
}
