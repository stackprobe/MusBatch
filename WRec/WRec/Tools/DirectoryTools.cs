using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Charlotte.Tools
{
	public class DirectoryTools
	{
		public static List<string> GetAllDir(string rootDir)
		{
			return GetAllPath(rootDir, true, false);
		}

		public static List<string> GetAllFile(string rootDir)
		{
			return GetAllPath(rootDir, false, true);
		}

		public static List<string> GetAllPath(string rootDir, bool dirFlag = true, bool fileFlag = true, List<string> dest = null)
		{
			if (dest == null)
				dest = new List<string>();

			foreach (string dir in Directory.GetDirectories(rootDir))
			{
				if (dirFlag)
					dest.Add(dir);

				GetAllPath(dir, dirFlag, fileFlag, dest);
			}
			if (fileFlag)
				foreach (string file in Directory.GetFiles(rootDir))
					dest.Add(file);

			return dest;
		}

		public class Into : IDisposable
		{
			private string _home;

			public Into(string dir)
			{
				_home = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(dir);
			}

			public void Dispose()
			{
				if (_home != null)
				{
					Directory.SetCurrentDirectory(_home);
					_home = null;
				}
			}
		}

		public static void DeleteAllSubDirUnhandled(string dir)
		{
			DeleteSubDirsUnhandled(dir, Directory.GetDirectories(dir));
		}

		public static void DeleteSubDirsUnhandled(string dir, string[] subDirs)
		{
			foreach (string subDir in subDirs)
			{
				string delDir = "$del_" + Guid.NewGuid().ToString("B");

				{
					ProcessStartInfo psi = new ProcessStartInfo();

					psi.WorkingDirectory = dir;
					psi.FileName = "cmd.exe";
					psi.Arguments = "/C ren \"" + Path.GetFileName(subDir) + "\" " + delDir;
					psi.CreateNoWindow = true;
					psi.UseShellExecute = false;

					Process.Start(psi).WaitForExit();
				}

				Directory.Delete(StringTools.Combine(dir, delDir));
			}
		}

		public static void DeleteDirIfExist(string dir)
		{
			if (Directory.Exists(dir))
				Directory.Delete(dir, true);
		}
	}
}
