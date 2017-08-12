using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Charlotte.Tools
{
	public class HttpResponse
	{
		private const int CR = 0x0d;
		private const int LF = 0x0a;
		private const int HEADER_LINE_LENMAX = 65536;
		private const int HEADER_SIZE_MAX = 500000; // 500 KB

		private NetworkStream _rs;
		private int _resBodySizeMax;
		private string _firstLine;
		private Dictionary<string, string> _headerFields = DictionaryTools.CreateIgnoreCase<string>();
		private int _contentLength;
		private bool _chunked;
		private byte[] _body;

		public HttpResponse(NetworkStream rs, int resBodySizeMax)
			: this(rs, resBodySizeMax, false)
		{ }

		public HttpResponse(NetworkStream rs, int resBodySizeMax, bool noBody)
		{
			_rs = rs;
			_resBodySizeMax = resBodySizeMax;
			this.ReadFirstLine();
			this.ReadHeaderFields();
			this.CheckHeaderFields();

			if (!noBody)
			{
				this.ReadBody();
			}
			_rs = null;
		}

		private int _totalSize = 0;

		private String ReadLine()
		{
			byte[] buff = new byte[HEADER_LINE_LENMAX];
			int wPos = 0;

			for (; ; )
			{
				int chr = _rs.ReadByte();

				if (chr == -1)
				{
					throw new Exception("文字列の途中で終端に到達しました。");
				}
				_totalSize++;

				if (chr == CR)
				{
					chr = _rs.ReadByte();

					if (chr != LF)
					{
						throw new Exception("改行の2文字目がLFではありません。");
					}
					break;
				}
				buff[wPos] = (byte)chr;
				wPos++;
			}
			return Encoding.UTF8.GetString(buff, 0, wPos);
		}

		private void ReadFirstLine()
		{
			_firstLine = this.ReadLine();
		}

		private void ReadHeaderFields()
		{
			List<string> lines = new List<string>();

			for (; ; )
			{
				String line = this.ReadLine();

				if (line.Length == 0)
				{
					break;
				}
				if (HEADER_SIZE_MAX < _totalSize)
				{
					throw new Exception("HTTP response-header size is overflow");
				}
				char firstChr = line[0];

				line = line.Trim();

				if (firstChr == '\t' || firstChr == ' ')
				{
					lines[lines.Count - 1] += " " + line;
				}
				else
				{
					lines.Add(line);
				}
			}
			foreach (string line in lines)
			{
				int index = line.IndexOf(':');
				string name = line.Substring(0, index);
				string value = line.Substring(index + 1);

				name = name.Trim();
				value = value.Trim();

				if (_headerFields.ContainsKey(name))
				{
					value = _headerFields[name] + " " + value;
					_headerFields.Remove(name);
				}
				_headerFields.Add(name, value);
			}
		}

		private void CheckHeaderFields()
		{
			String sConLen = DictionaryTools.Get(_headerFields, "Content-Length", null);
			String sTrnEnc = DictionaryTools.Get(_headerFields, "Transfer-Encoding", null);

			if (sConLen != null)
			{
				_contentLength = int.Parse(sConLen);
			}
			if (sTrnEnc != null)
			{
				sTrnEnc = sTrnEnc.ToLower();

				if (sTrnEnc.Contains("chunked"))
				{
					_chunked = true;
				}
			}
		}

		private byte[] ReadBytes(int size)
		{
			byte[] buff = new byte[size];

			for (int wPos = 0; wPos < size; wPos++)
			{
				int chr = _rs.ReadByte();

				if (chr == -1)
				{
					throw new Exception("バイナリデータの途中で終端に到達しました。");
				}
				buff[wPos] = (byte)chr;
			}
			return buff;
		}

		private void ReadBody()
		{
			if (_chunked)
			{
				List<byte[]> parts = new List<byte[]>();
				int totalSize = 0;

				for (; ; )
				{
					String line = this.ReadLine();
					int extPos = line.IndexOf(';');

					if (extPos != -1)
					{
						line = line.Substring(0, extPos); // ignore chunked-extension
					}
					line = line.Trim();
					int partSize = Convert.ToInt32(line, 16);

					if (partSize < 0)
					{
						throw new Exception("HTTP chunked-part size is negative");
					}
					if (partSize == 0)
					{
						break;
					}
					if (_resBodySizeMax - totalSize < partSize)
					{
						throw new Exception("HTTP chunked-body size is overflow");
					}
					byte[] part = this.ReadBytes(partSize);
					parts.Add(part);
					totalSize += partSize;

					_rs.ReadByte(); // CR
					_rs.ReadByte(); // LF
				}
				while (1 <= this.ReadLine().Length) ; // ignore chunked-footer
				_body = new byte[totalSize];
				int wPos = 0;

				foreach (byte[] part in parts)
				{
					Array.Copy(part, 0, _body, wPos, part.Length);
					wPos += part.Length;
				}
			}
			else
			{
				if (_resBodySizeMax < _contentLength)
				{
					throw new Exception("HTTP response-body size is overflow");
				}
				_body = this.ReadBytes(_contentLength);
			}
		}

		public string GetFirstLine()
		{
			return _firstLine;
		}

		private string[] _firstLineTokens;

		public string[] GetFirstLineTokens()
		{
			if (_firstLineTokens == null)
			{
				_firstLineTokens = this.GetFirstLine().Split(' ');
			}
			return _firstLineTokens;
		}

		public string GetHTTPVersion()
		{
			return this.GetFirstLineTokens()[0];
		}

		public int GetStatus()
		{
			return int.Parse(this.GetFirstLineTokens()[1]);
		}

		public String GetReasonPhrase()
		{
			return this.GetFirstLineTokens()[2];
		}

		public Dictionary<String, String> GetHeaderFields()
		{
			return _headerFields;
		}

		public byte[] GetBody()
		{
			return _body;
		}
	}
}
