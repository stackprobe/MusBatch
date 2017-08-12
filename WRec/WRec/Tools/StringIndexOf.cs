using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class StringIndexOf
	{
		private Dictionary<char, int> _map = new Dictionary<char, int>();

		public StringIndexOf(string str)
		{
			for (int index = str.Length - 1; 0 <= index; index--)
			{
				DictionaryTools.Put(_map, str[index], index);
			}
		}

		public int IndexOf(char chr)
		{
			return DictionaryTools.Get(_map, chr, -1);
		}
	}
}
