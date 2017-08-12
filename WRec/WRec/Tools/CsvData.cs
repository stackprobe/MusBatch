using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class CsvData
	{
		private AutoTable<string> _table = new AutoTable<string>("");
		private char _delimiter;

		public CsvData()
			: this(',')
		{ }

		public static CsvData CreateTsv()
		{
			return new CsvData('\t');
		}

		public CsvData(char delimiter)
		{
			_delimiter = delimiter;
		}

		public void Clear()
		{
			_table.Clear();
		}

		public void ReadFile(string csvFile)
		{
			this.ReadFile(csvFile, StringTools.ENCODING_SJIS);
		}

		public void ReadFile(string csvFile, Encoding encoding)
		{
			this.ReadText(File.ReadAllText(csvFile, encoding));
		}

		private string _text;
		private int _rPos;

		private int NextChar()
		{
			char chr;

			do
			{
				if (_text.Length <= _rPos)
					return -1;

				chr = _text[_rPos];
				_rPos++;
			}
			while (chr == '\r');

			return chr;
		}

		public void ReadText(string text)
		{
			_text = text;
			_rPos = 0;

			_table.Clear();
			_table.AddRow();

			for (; ; )
			{
				int chr = this.NextChar();

				if (chr == -1)
					break;

				StringBuilder buff = new StringBuilder();

				if (chr == '"')
				{
					for (; ; )
					{
						chr = this.NextChar();

						if (chr == -1)
							break;

						if (chr == '"')
						{
							chr = this.NextChar();

							if (chr != '"')
								break;
						}
						buff.Append((char)chr);
					}
				}
				else
				{
					for (; ; )
					{
						if (chr == _delimiter || chr == '\n')
							break;

						buff.Append((char)chr);
						chr = this.NextChar();

						if (chr == -1)
							break;
					}
				}
				_table.Add(buff.ToString());

				if (chr == '\n')
					_table.AddRow();
			}
			_text = null;
		}

		public AutoTable<string> Table
		{
			get
			{
				return _table;
			}
		}

		public string GetCell(int x, int y)
		{
			string cell = _table[x, y];

			if (StringTools.ContainsChar(cell, "\r\n\"" + _delimiter))
				cell = "\"" + cell.Replace("\"", "\"\"") + "\"";

			return cell;
		}

		public string GetLine(int y)
		{
			List<string> cells = new List<string>();

			for (int x = 0; x < _table.Width; x++)
				cells.Add(this.GetCell(x, y));

			return string.Join("" + _delimiter, cells);
		}

		public List<string> GetLines()
		{
			List<string> lines = new List<string>();

			for (int y = 0; y < _table.Height; y++)
				lines.Add(this.GetLine(y));

			return lines;
		}

		public string GetText()
		{
			return string.Join("\r\n", this.GetLines());
		}

		private void WriteFile(string csvFile, Encoding encoding)
		{
			File.WriteAllText(csvFile, this.GetText(), encoding);
		}

		public void WriteFile(string csvFile)
		{
			this.WriteFile(csvFile, StringTools.ENCODING_SJIS);
		}

		public void TT()
		{
			this.TrimAllCell();
			this.Trim();
		}

		public void TrimAllCell()
		{
			for (int x = 0; x < _table.Width; x++)
				for (int y = 0; y < _table.Height; y++)
					_table[x, y] = _table[x, y].Trim();
		}

		public void Trim()
		{
			this.TrimX();
			this.TrimY();
		}

		public void TrimX()
		{
			while (1 <= _table.Width)
			{
				for (int y = 0; y < _table.Height; y++)
					if (_table[_table.Width - 1, y] != "")
						return;

				_table.Width--;
			}
		}

		public void TrimY()
		{
			while (1 <= _table.Height)
			{
				for (int x = 0; x < _table.GetWidth(_table.Height - 1); x++)
					if (_table[x, _table.Height - 1] != "")
						return;

				_table.Height--;
			}
		}

		public class Stream : IDisposable
		{
			public Stream(string file)
				: this(file, StringTools.ENCODING_SJIS)
			{ }

			private string _file;
			private Encoding _encoding;
			private char _delimiter;

			public Stream(string file, Encoding encoding, char delimiter = ',')
			{
				_file = file;
				_encoding = encoding;
				_delimiter = delimiter;
			}

			public static Stream createTsv(string file)
			{
				return createTsv(file, StringTools.ENCODING_SJIS);
			}

			public static Stream createTsv(string file, Encoding encoding)
			{
				return new Stream(file, encoding, '\t');
			}

			// ---- reader ----

			private StreamReader _r = null;

			public void ReadOpen()
			{
				_r = new StreamReader(_file, _encoding);
			}

			public int NextChar()
			{
				int chr;

				do
				{
					chr = _r.Read();
				}
				while (chr == '\r');

				return chr;
			}

			private int _termChr;

			public string ReadCell()
			{
				StringBuilder buff = new StringBuilder();
				int chr = this.NextChar();

				if (chr == '"')
				{
					for (; ; )
					{
						chr = this.NextChar();

						if (chr == -1)
							break;

						if (chr == '"')
						{
							chr = this.NextChar();

							if (chr != '"')
								break;
						}
						buff.Append((char)chr);
					}
				}
				else
				{
					for (; ; )
					{
						if (chr == _delimiter || chr == '\n' || chr == -1)
							break;

						buff.Append((char)chr);
						chr = this.NextChar();
					}
				}
				_termChr = chr;
				return buff.ToString();
			}

			public List<string> ReadRow()
			{
				List<string> row = new List<string>();

				do
				{
					row.Add(this.ReadCell());
				}
				while (_termChr != '\n' && _termChr != -1);

				if (_termChr == -1 && row.Count == 1 && row[0].Length == 0)
				{
					return null;
				}
				return row;
			}

			public List<List<string>> ReadToEnd()
			{
				List<List<string>> rows = new List<List<string>>();

				for (; ; )
				{
					List<string> row = ReadRow();

					if (row == null)
						break;

					rows.Add(row);
				}
				return rows;
			}

			public void ReadClose()
			{
				if (_r != null)
				{
					_r.Dispose();
					_r = null;
				}
			}

			// ---- writer ----

			private StreamWriter _w = null;

			public void WriteOpen()
			{
				_w = new StreamWriter(_file, false, _encoding);
			}

			public void WriteCell(string cell)
			{
				if (StringTools.ContainsChar(cell, "\r\n\"" + _delimiter))
				{
					_w.Write('"');

					foreach (char chr in cell)
					{
						if (chr == '"')
							_w.Write('"');

						_w.Write(chr);
					}
					_w.Write('"');
				}
				else
					_w.Write(cell);
			}

			public void WriteDelimiter()
			{
				_w.Write(_delimiter);
			}

			public void WriteReturn()
			{
				_w.Write("\r\n");
			}

			public void WriteRow(string[] row)
			{
				for (int index = 0; index < row.Length; index++)
				{
					if (1 <= index)
						this.WriteDelimiter();

					this.WriteCell(row[index]);
				}
				this.WriteReturn();
			}

			public void WriteRow(List<string> row)
			{
				this.WriteRow(row.ToArray());
			}

			public void WriteRows(List<List<string>> rows)
			{
				foreach (List<string> row in rows)
					this.WriteRow(row);
			}

			public void WriteClose()
			{
				if (_w != null)
				{
					_w.Dispose();
					_w = null;
				}
			}

			private List<string> _row = new List<string>();

			public void Add(string cell)
			{
				_row.Add(cell);
			}

			public void EndRow()
			{
				this.WriteRow(_row);
				_row.Clear();
			}

			// ----

			public void Dispose()
			{
				this.ReadClose();
				this.WriteClose();
			}
		}
	}
}
