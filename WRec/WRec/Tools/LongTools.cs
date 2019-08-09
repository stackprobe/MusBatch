using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class LongTools
	{
		public static int Comp(long a, long b)
		{
			if (a < b)
				return -1;

			if (b < a)
				return 1;

			return 0;
		}
	}
}
