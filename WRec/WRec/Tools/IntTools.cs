using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class IntTools
	{
		public const int IMAX = 1000000000; // 10^9

		public static int Comp(int a, int b)
		{
			if (a < b)
				return -1;

			if (b < a)
				return 1;

			return 0;
		}

		public static bool IsRange(int value, int minval = 0, int maxval = IMAX)
		{
			return minval <= value && value <= maxval;
		}

		public static int ToRange(int value, int minval = 0, int maxval = IMAX)
		{
			return Math.Min(Math.Max(value, minval), maxval);
		}

		public static int ToInt(double value)
		{
			if (value < 0.0)
				return (int)(value - 0.5);
			else
				return (int)(value + 0.5);
		}

		public static int ToInt(string str, int minval = 0, int maxval = IMAX, int defval = 0)
		{
			try
			{
				int value = int.Parse(str);

				if (IsRange(value, minval, maxval))
					return value;
			}
			catch
			{ }

			return defval;
		}

		public static int Parse(string str, int minval = 0, int maxval = IMAX)
		{
			int value = int.Parse(str);

			if (IsRange(value, minval, maxval) == false)
				throw new Exception();

			return value;
		}

		public static uint Root(UInt64 value)
		{
			uint ret = 0u;

			for (uint bit = 1u << 31; bit != 0; bit >>= 1)
			{
				uint t = ret | bit;

				if ((UInt64)t * t <= value)
					ret = t;
			}
			return ret;
		}
	}
}
