using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class StringTools
	{
		public static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);
		public static readonly Encoding ENCODING_EUC = Encoding.GetEncoding(51932);

		public static List<char> ToCharList(string src)
		{
			return src.ToCharArray().ToList();
		}

		public static string ToString(List<char> src)
		{
			return new string(src.ToArray());
		}

		public static string Set(string str, int index, char chr)
		{
			List<char> tmp = ToCharList(str);
			tmp[index] = chr;
			return ToString(tmp);
		}

		public static string Insert(string str, int insertPos, string ptn)
		{
			List<char> tmp = ToCharList(str);
			tmp.InsertRange(insertPos, ptn.ToCharArray());
			return ToString(tmp);
		}

		public static string Remove(string str, int startPos, int count)
		{
			List<char> tmp = ToCharList(str);
			tmp.RemoveRange(startPos, count);
			return ToString(tmp);
		}

		public static string ZPad(int value, int minlen = 1)
		{
			return ZPad("" + value, minlen);
		}

		public static string ZPad(string str, int minlen = 1)
		{
			return LPad(str, minlen, '0');
		}

		public static string LPad(string str, int minlen, char padding)
		{
			while (str.Length < minlen)
			{
				str = padding + str;
			}
			return str;
		}

		public static int Comp(string a, string b)
		{
			return a.CompareTo(b);
		}

		public static int CompIgnoreCase(string a, string b)
		{
			return a.ToLower().CompareTo(b.ToLower());
		}

		public class IEComp : IEqualityComparer<string>
		{
			public bool Equals(string a, string b)
			{
				return a == b;
			}

			public int GetHashCode(string str)
			{
				return str.GetHashCode();
			}
		}

		public class IECompIgnoreCase : IEqualityComparer<string>
		{
			public bool Equals(string a, string b)
			{
				return a.ToLower() == b.ToLower();
			}

			public int GetHashCode(string str)
			{
				return str.ToLower().GetHashCode();
			}
		}

		public static bool IsSame(string[] lines1, string[] lines2, bool ignoreCase = false)
		{
			if (lines1.Length != lines2.Length)
				return false;

			for (int index = 0; index < lines1.Length; index++)
				if (IsSame(lines1[index], lines2[index], ignoreCase) == false)
					return false;

			return true;
		}

		public static bool IsSame(string str1, string str2, bool ignoreCase = false)
		{
			if (ignoreCase)
			{
				str1 = str1.ToLower();
				str2 = str2.ToLower();
			}
			return str1 == str2;
		}

		public static string Combine(string path1, string path2)
		{
			return PathFltr(path1 + '\\' + path2);
		}

		public static string PathFltr(string path)
		{
			path = path.Replace('/', '\\');

			bool networkFlag = path.StartsWith("\\\\");

			for (int c = 0; c < 20; c++)
				path = path.Replace("\\\\", "\\");

			if (networkFlag)
				path = '\\' + path;

			return path;
		}

		public static string Trim(string str)
		{
			for (int index = 0; index < str.Length; index++)
				if (str[index] < ' ')
					Set(str, index, ' ');

			for (int c = 0; c < 20; c++)
				str = str.Replace("  ", " ");

			str = StringTools.RemoveStartsWith(str, " ");
			str = StringTools.RemoveEndsWith(str, " ");

			return str;
		}

		public static string RemoveStartsWith(string str, string ptn, bool ignoreCase = false)
		{
			if (StartsWith(str, ptn, ignoreCase))
				return str.Substring(ptn.Length);

			return str;
		}

		public static string RemoveEndsWith(string str, string ptn, bool ignoreCase = false)
		{
			if (EndsWith(str, ptn, ignoreCase))
				return str.Substring(0, str.Length - ptn.Length);

			return str;
		}

		public static bool StartsWith(string str, string ptn, bool ignoreCase)
		{
			if (ignoreCase)
			{
				str = str.ToLower();
				ptn = ptn.ToLower();
			}
			return str.StartsWith(ptn);
		}

		public static bool EndsWith(string str, string ptn, bool ignoreCase)
		{
			if (ignoreCase)
			{
				str = str.ToLower();
				ptn = ptn.ToLower();
			}
			return str.EndsWith(ptn);
		}

		public static string ToBase64(byte[] block)
		{
			return System.Convert.ToBase64String(block);
		}

		public static byte[] DecodeBase64(string str)
		{
			return System.Convert.FromBase64String(str);
		}

		public const string DIGIT = "0123456789";
		public const string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string alpha = "abcdefghijklmnopqrstuvwxyz";
		public const string HEXADECIMAL = "0123456789ABCDEF";
		public const string hexadecimal = "0123456789abcdef";

		private static StringIndexOf _hexadecimal_i = new StringIndexOf(hexadecimal);

		public static byte[] Hex(string str)
		{
			List<byte> buff = new List<byte>();

			for (int index = 0; index < str.Length; index += 2)
			{
				int h = _hexadecimal_i.IndexOf(str[index]);
				int l = _hexadecimal_i.IndexOf(str[index + 1]);

				buff.Add((byte)((h << 4) | l));
			}
			return buff.ToArray();
		}

		public static string ToHex(byte[] block)
		{
			StringBuilder buff = new StringBuilder();

			foreach (byte chr in block)
			{
				buff.Append(hexadecimal[chr >> 4]);
				buff.Append(hexadecimal[chr & 15]);
			}
			return buff.ToString();
		}

		public static string Repeat(char chr, int count)
		{
			StringBuilder buff = new StringBuilder();

			while (1 <= count)
			{
				buff.Append(chr);
				count--;
			}
			return buff.ToString();
		}

		public static string ReplaceChar(string str, string fromChrs, char toChr)
		{
			foreach (char fromChr in fromChrs)
				str = str.Replace(fromChr, toChr);

			return str;
		}

		public static string ReplaceChar(string str, string fromChrs, string toChrs)
		{
			for (int index = 0; index < fromChrs.Length; index++)
				str = str.Replace(fromChrs[index], toChrs[index]);

			return str;
		}

		public static string ReplaceLoop(string str, string srcPtn, string destPtn, int count = 20)
		{
			for (int index = 0; index < count; index++)
				str = str.Replace(srcPtn, destPtn);

			return str;
		}

		public static int IndexOfChar(string str, string chrs)
		{
			int ret = int.MaxValue;

			foreach (char chr in chrs)
			{
				int index = str.IndexOf(chrs);

				if (index != -1)
					ret = Math.Min(index, ret);
			}
			if (ret == int.MaxValue)
				ret = -1;

			return ret;
		}

		public static int LastIndexOfChar(string str, string chrs)
		{
			int ret = -1;

			foreach (char chr in chrs)
				ret = Math.Max(ret, str.IndexOf(chr));

			return ret;
		}

		public static bool ContainsChar(string str, string chrs)
		{
			foreach (char chr in chrs)
				if (str.Contains(chr))
					return true;

			return false;
		}

		public static List<string> NumericTokenize(string str)
		{
			return MeaningTokenize(str, StringTools.DIGIT);
		}

		public static List<string> MeaningTokenize(string str, string meanings)
		{
			return Tokenize(str, meanings, true, true);
		}

		public static List<string> Tokenize(string str, string delimiters, bool meaningFlag = false, bool ignoreEmpty = false)
		{
			List<string> tokens = new List<string>();
			StringBuilder buff = new StringBuilder();

			foreach (char chr in str)
			{
				if (delimiters.Contains(chr) != meaningFlag)
				{
					tokens.Add(buff.ToString());
					buff = new StringBuilder();
				}
				else
					buff.Append(chr);
			}
			tokens.Add(buff.ToString());

			if (ignoreEmpty)
				RemoveEmpty(tokens);

			return tokens;
		}

		public static void RemoveEmpty(List<string> lines)
		{
			for (int index = 0; index < lines.Count; index++)
			{
				if (lines[index] == null || lines[index] == "")
				{
					lines.RemoveAt(index);
					index--;
				}
			}
		}

		public static readonly string ASCII = Encoding.ASCII.GetString(ArrayTools.ByteSq(0x21, 0x7e));
		public static readonly string ASCII_SPC = Encoding.ASCII.GetString(ArrayTools.ByteSq(0x20, 0x7e));

		public static string TextToLine(string src)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char chr in src)
			{
				if (chr == '\t')
				{
					buff.Append('\\');
					buff.Append('t');
				}
				else if (chr == '\n')
				{
					buff.Append('\\');
					buff.Append('n');
				}
				else if (chr == ' ')
				{
					buff.Append('\\');
					buff.Append('s');
				}
				else if (chr == '\\')
				{
					buff.Append('\\');
					buff.Append('\\');
				}
				else if (chr < ' ') // ? control code
				{
					// ignore
				}
				else
				{
					buff.Append(chr);
				}
			}
			return buff.ToString();
		}

		public static string LineToText(string src)
		{
			StringBuilder buff = new StringBuilder();
			bool escapeMode = false;

			foreach (char chr in src)
			{
				if (escapeMode)
				{
					char trueChr;

					switch (chr)
					{
						case 't':
							trueChr = '\t';
							break;
						case 'n':
							trueChr = '\n';
							break;
						case 's':
							trueChr = ' ';
							break;
						case '\\':
							trueChr = '\\';
							break;

						default:
							trueChr = chr;
							break;
					}
					buff.Append(trueChr);
					escapeMode = false;
				}
				else
				{
					if (chr == '\\')
					{
						escapeMode = true;
					}
					else
					{
						buff.Append(chr);
					}
				}
			}
			return buff.ToString();
		}

		public const string S_TRUE = "true";
		public const string S_FALSE = "false";

		public static string ToString(bool flag)
		{
			return flag ? S_TRUE : S_FALSE;
		}

		public static bool ToFlag(string str)
		{
			return IsSame(str, S_TRUE, true);
		}

		public static int IndexOfIgnoreCase(string str, string ptn)
		{
			return str.ToLower().IndexOf(ptn.ToLower());
		}

		public class UrlData
		{
			public string Scheme;
			public string Host; // Domain or (Domain + ":" + Port)
			public string Domain;
			public string Port;
			public string Path;

			public UrlData(string url)
			{
				int index;

				index = url.IndexOf("://");
				if (index != -1)
				{
					this.Scheme = url.Substring(0, index);
					url = url.Substring(index + 3);
				}
				else
					this.Scheme = "http";

				index = url.IndexOf('/');
				if (index != -1)
				{
					this.Host = url.Substring(0, index);
					this.Path = url.Substring(index);
				}
				else
				{
					this.Host = url;
					this.Path = "/";
				}

				index = this.Host.IndexOf(':');
				if (index != -1)
				{
					this.Domain = this.Host.Substring(0, index);
					this.Port = this.Host.Substring(index + 1);
				}
				else
				{
					this.Domain = this.Host;

					if (this.Scheme == "http")
						this.Port = "" + 80;
					else if (this.Scheme == "https")
						this.Port = "" + 443;
					else
						this.Port = "" + 65535;
				}
			}
		}

		public static string Reverse(string src)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = src.Length - 1; 0 <= index; index--)
				buff.Append(src[index]);

			return buff.ToString();
		}
	}
}
