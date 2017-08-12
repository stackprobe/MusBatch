using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class WorkBenchDir
	{
		private static WorkBenchDir _i = null;

		public static WorkBenchDir I
		{
			get
			{
				if (_i == null)
					_i = new WorkBenchDir();

				return _i;
			}
		}

		private string _rootDir = FileTools.GetTempPath(Program.APP_IDENT);

		private WorkBenchDir()
		{
			this.Clear();
			Directory.CreateDirectory(_rootDir);
		}

		~WorkBenchDir()
		{
			this.Clear();
		}

		public void Clear()
		{
			try
			{
				Directory.Delete(_rootDir, true);
			}
			catch
			{ }
		}

		public string GetPath(string relPath)
		{
			return StringTools.Combine(_rootDir, relPath);
		}

		public string MakePath()
		{
			return StringTools.Combine(_rootDir, Guid.NewGuid().ToString("B"));
		}
	}
}
