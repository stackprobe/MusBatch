using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class UInt64Tools
	{
		public const UInt64 IMAX = 1000000000ul; // 10^9
		public const UInt64 IMAX_64 = 1000000000000000000ul; // 10^18

		public static bool IsRange(UInt64 value, UInt64 minval = 0, UInt64 maxval = IMAX_64)
		{
			return minval <= value && value <= maxval;
		}

		public static UInt64 ToUInt64(string str, UInt64 minval = 0, UInt64 maxval = IMAX_64, UInt64 defval = 0)
		{
			try
			{
				UInt64 value = UInt64.Parse(str);

				if (IsRange(value, minval, maxval))
					return value;
			}
			catch
			{ }

			return defval;
		}
	}
}
