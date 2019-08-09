using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class DateTimeTools
	{
		public static string GetCommonString(DateTime dt)
		{
			return GetString(dt, "Y/M/D h:m:s");
		}

		public static string GetSimpleString(DateTime dt)
		{
			return GetString(dt, "YMDhms");
		}

		public static string GetString(DateTime dt, string format)
		{
			string ret = format;

			ret = ret.Replace("Y", StringTools.ZPad(dt.Year, 4));
			ret = ret.Replace("M", StringTools.ZPad(dt.Month, 2));
			ret = ret.Replace("D", StringTools.ZPad(dt.Day, 2));
			ret = ret.Replace("h", StringTools.ZPad(dt.Hour, 2));
			ret = ret.Replace("m", StringTools.ZPad(dt.Minute, 2));
			ret = ret.Replace("s", StringTools.ZPad(dt.Second, 2));

			return ret;
		}
	}
}
