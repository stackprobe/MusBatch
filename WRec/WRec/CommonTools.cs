using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Charlotte
{
	public class CommonTools
	{
		public static Process StartProc(string file, string args = "")
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = file;
			psi.Arguments = args;
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;
			psi.WorkingDirectory = BootTools.SelfDir;

			return Process.Start(psi);
		}

		[DllImport("user32.dll")]
		private static extern int GetAsyncKeyState(int vKey);
		private static readonly int VK_RCONTROL = 0x000000a3;

		public static bool IsRCtrlPressed()
		{
			return GetAsyncKeyState(VK_RCONTROL) != 0;
		}
	}
}
