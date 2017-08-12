using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	public class FileTools
	{
		public static void CreateFile(string file)
		{
			using (new FileStream(file, FileMode.Create, FileAccess.Write))
			{ }
		}

		public static byte[] ReadToEnd(Stream s, int size = int.MaxValue)
		{
			ByteBuffer buff = new ByteBuffer();

			for (int count = 0; count < size; count++)
			{
				int chr = s.ReadByte();

				if (chr == -1)
					break;

				buff.Add((byte)chr);
			}
			return buff.Join();
		}

		public static byte[] ReadToSize(Stream s, int size, bool readZeroKeepReading = false)
		{
			byte[] buff = new byte[size];
			int waitMillis = 0;
			int wPos = 0;

			while (wPos < size)
			{
				int readSize = s.Read(buff, wPos, size - wPos);

				if (readSize < 0)
					break;

				if (readSize == 0)
				{
					if (readZeroKeepReading == false)
						break;

					if (waitMillis < 200)
						waitMillis++;

					Thread.Sleep(waitMillis);
				}
				else
				{
					waitMillis = 0;
					wPos += readSize;
				}
			}
			if (wPos < size)
				buff = ArrayTools.GetPart(buff, 0, wPos);

			return buff;
		}

		public static void Write(Stream s, byte[] block)
		{
			s.Write(block, 0, block.Length);
		}

		public static string GetTempPath(string relPath)
		{
			return StringTools.Combine(Environment.GetEnvironmentVariable("TMP"), relPath);
		}

		public static string MakeTempPath()
		{
			return StringTools.Combine(Environment.GetEnvironmentVariable("TMP"), Guid.NewGuid().ToString("B"));
		}

		public static bool IsSame(string file1, string file2)
		{
			using (FileStream fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.Read, 1000000))
			using (FileStream fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.Read, 1000000))
			{
				for (; ; )
				{
					int chr1 = fs1.ReadByte();
					int chr2 = fs2.ReadByte();

					if (chr1 != chr2)
					{
						return false;
					}
					if (chr1 == -1)
					{
						break;
					}
				}
			}
			return true;
		}

		public static void TryDelete(string file)
		{
			try
			{
				File.Delete(file);
			}
			catch
			{ }
		}

		public static string ChangeRoot(string file, string oldRootDir, string newRootDir = null)
		{
			if (oldRootDir != null)
			{
				oldRootDir = StringTools.PathFltr(oldRootDir + '\\');

				if (StringTools.StartsWith(file, oldRootDir, true) == false)
					throw new Exception("Wrong oldRootDir: " + oldRootDir + ", " + file);

				file = file.Substring(oldRootDir.Length);
			}
			if (newRootDir != null)
			{
				file = StringTools.Combine(newRootDir, file);
			}
			return file;
		}

		public enum CreatePath_e
		{
			Dir,
			File,
			None,
		}

		public static void CreatePath(string path, CreatePath_e mode = CreatePath_e.None, bool delFlag = false)
		{
			if (delFlag)
			{
				DeleteFileIfExist(path);
				DirectoryTools.DeleteDirIfExist(path);
			}
			Directory.CreateDirectory(path);

			switch (mode)
			{
				case CreatePath_e.Dir:
					// noop
					break;

				case CreatePath_e.File:
					Directory.Delete(path);
					CreateFile(path);
					break;

				case CreatePath_e.None:
					Directory.Delete(path);
					break;

				default:
					throw null;
			}
		}

		public static void CreatePathIfNotExist(string path, CreatePath_e mode = CreatePath_e.None)
		{
			if (File.Exists(path))
				return;

			if (Directory.Exists(path))
				return;

			CreatePath(path, mode);
		}

		public static void DeleteFileIfExist(string file)
		{
			if (File.Exists(file))
				File.Delete(file);
		}
	}
}
