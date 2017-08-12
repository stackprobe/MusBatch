using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class CrossDictionaryTools
	{
		public static CrossDictionary<string, string> Create()
		{
			return new CrossDictionary<string, string>(
				DictionaryTools.Create<string>(),
				DictionaryTools.Create<string>()
				);
		}

		public static CrossDictionary<string, string> CreateIgnoreCase()
		{
			return new CrossDictionary<string, string>(
				DictionaryTools.CreateIgnoreCase<string>(),
				DictionaryTools.CreateIgnoreCase<string>()
				);
		}
	}
}
