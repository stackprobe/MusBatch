using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informatix.Tools
{
	public class BracketParser
	{
		/// <summary>
		/// $$ -> $
		/// ${aaa}, $(bbb), $[ccc], etc.
		/// </summary>
		private const char E = '$';

		public delegate string Filter_d(string text);

		public class Bracket
		{
			/// <summary>
			/// "{}", "()", "[]", etc.
			/// </summary>
			public string BrChrs;
			public Filter_d D_Filter;

			public Bracket(string b, Filter_d f)
			{
				BrChrs = b;
				D_Filter = f;
			}
		}

		private List<Bracket> Brackets = new List<Bracket>();

		public void Add(Bracket bracket)
		{
			Brackets.Add(bracket);
		}

		private Stack<Info> Infos = new Stack<Info>();

		private class Info
		{
			public Bracket Bracket;
			public int LeftIndex; // 左括弧の位置
		}

		public string Perform(string text)
		{
			Infos.Clear();

			for (int index = 0; index < text.Length; )
			{
				if (1 <= Infos.Count && Infos.Peek().Bracket.BrChrs[1] == text[index])
				{
					Info info = Infos.Pop();
					string innerText = text.Substring(info.LeftIndex + 1, index - info.LeftIndex - 1);

					innerText = info.Bracket.D_Filter(innerText);

					string lead = text.Substring(0, info.LeftIndex) + innerText;
					string trail = text.Substring(index + 1);

					text = lead + trail;
					index = lead.Length;
				}
				else
				{
					if (text[index] == E)
					{
						text = text.Substring(0, index) + text.Substring(index + 1); // erase E

						foreach (Bracket b in Brackets)
						{
							if (b.BrChrs[0] == text[index])
							{
								Info info = new Info();

								info.Bracket = b;
								info.LeftIndex = index;

								Infos.Push(info);
								break;
							}
						}
					}
					index++;
				}
			}
			return text;
		}
	}
}
