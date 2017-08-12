using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class DebugTools
	{
		private static object LogStrm_SYNCROOT = new object();
		private static FileStream LogStrm;

		public static void WriteLog(string line)
		{
			lock (LogStrm_SYNCROOT)
			{
				if (LogStrm == null)
					LogStrm = new FileStream(@"C:\temp\Module.log", FileMode.Create, FileAccess.Write);

				FileTools.Write(
					LogStrm,
					StringTools.ENCODING_SJIS.GetBytes("[" + DateTimeTools.GetCommonString(DateTime.Now) + "] " + line + "\r\n")
					);
			}
		}

		public static void MakeRandTextFile(string file, Encoding encoding, string chrs, string lineNew, int linecnt, int chrcntMin, int chrcntMax)
		{
			byte[] bLineNew = encoding.GetBytes(lineNew);

			using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
			{
				for (int index = 0; index < linecnt; index++)
				{
					FileTools.Write(fs, encoding.GetBytes(MakeRandString(chrs, MathTools.Random(chrcntMin, chrcntMax))));
					FileTools.Write(fs, bLineNew);
				}
			}
		}

		public static string MakeRandString(string chrs, int chrcnt)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < chrcnt; index++)
			{
				buff.Append(chrs[MathTools.Random(chrs.Length)]);
			}
			return buff.ToString();
		}

		public static byte[] MakeRandBytes(int size)
		{
			byte[] buff = new byte[size];

			for (int index = 0; index < size; index++)
			{
				buff[index] = (byte)MathTools.Random(256);
			}
			return buff;
		}
	}
}
