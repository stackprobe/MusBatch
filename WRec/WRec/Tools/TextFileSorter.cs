using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class TextFileSorter : FileSorter<StreamReader, StreamWriter, string>
	{
		private Encoding _encoding;

		public TextFileSorter(Encoding encoding)
		{
			_encoding = encoding;
		}

		protected override StreamReader ReadOpen(string file)
		{
			return new StreamReader(file, _encoding);
		}

		protected override string ReadRecord(StreamReader reader)
		{
			StringBuilder buff = new StringBuilder();

			for (; ; )
			{
				int chr = reader.Read();

				if (chr == -1)
				{
					if (buff.Length == 0)
					{
						return null;
					}
					break;
				}
				if (chr == '\n')
				{
					break;
				}
				if (chr == '\r')
				{
					continue;
				}
				buff.Append((char)chr);
			}
			return buff.ToString();
		}

		protected override void ReadClose(StreamReader reader)
		{
			reader.Close();
		}

		protected override StreamWriter WriteOpen(string file)
		{
			return new StreamWriter(file, false, _encoding);
		}

		protected override void WriteRecord(StreamWriter writer, string record)
		{
			writer.Write(record);
			writer.Write('\r');
			writer.Write('\n');
		}

		protected override void WriteClose(StreamWriter writer)
		{
			writer.Close();
		}

		protected override long GetWeight(string record)
		{
			return 100 + record.Length * 2;
		}

		protected override long GetWeightMax()
		{
			return 50000000; // 50 MB
		}

		protected override int Comp(string str1, string str2)
		{
			byte[] bStr1 = _encoding.GetBytes(str1);
			byte[] bStr2 = _encoding.GetBytes(str2);
			int end = Math.Min(bStr1.Length, bStr2.Length);

			for (int index = 0; index < end; index++)
			{
				int chr1 = bStr1[index];
				int chr2 = bStr2[index];

				if (chr1 < chr2)
					return -1;

				if (chr2 < chr1)
					return 1;
			}
			if (end < bStr1.Length)
				return 1;

			if (end < bStr2.Length)
				return -1;

			return 0;
		}
	}
}
