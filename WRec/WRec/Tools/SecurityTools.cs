using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Charlotte.Tools
{
	public class SecurityTools
	{
		public static string GetSHA512_128String(byte[] block)
		{
			return GetSHA512String(block).Substring(0, 32);
		}

		public static string GetSHA512String(byte[] block)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				return StringTools.ToHex(sha512.ComputeHash(block));
			}
		}

		public static string GetSHA512_128StringByFile(string file)
		{
			return GetSHA512StringByFile(file).Substring(0, 32);
		}

		public static string GetSHA512StringByFile(string file)
		{
			using (SHA512 sha512 = SHA512.Create())
			using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				return StringTools.ToHex(sha512.ComputeHash(fs));
			}
		}
	}
}
