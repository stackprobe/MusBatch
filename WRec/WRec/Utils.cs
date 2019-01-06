using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class Utils
	{
		public static void AntiWindowsDefenderSmartScreen()
		{
			WriteLog("awdss_1");

			if (Gnd.I.Is初回起動())
			{
				WriteLog("awdss_2");

				foreach (string exeFile in Directory.GetFiles(BootTools.SelfDir, "*.exe", SearchOption.TopDirectoryOnly))
				{
					try
					{
						WriteLog("awdss_exeFile: " + exeFile);

						if (StringTools.IsSame(exeFile, BootTools.SelfFile, true))
						{
							WriteLog("awdss_self_noop");
						}
						else
						{
							byte[] exeData = File.ReadAllBytes(exeFile);
							File.Delete(exeFile);
							File.WriteAllBytes(exeFile, exeData);
						}
						WriteLog("awdss_OK");
					}
					catch (Exception e)
					{
						WriteLog(e);
					}
				}
				WriteLog("awdss_3");
			}
			WriteLog("awdss_4");
		}

		private static string LogFile = null;
		private static long WL_Count = 0;

		public static void WriteLog(object message)
		{
			try
			{
				if (LogFile == null)
					LogFile = Path.Combine(BootTools.SelfDir, Path.GetFileNameWithoutExtension(BootTools.SelfFile) + ".log");

				using (StreamWriter writer = new StreamWriter(LogFile, WL_Count++ % 1000 != 0, Encoding.UTF8))
				{
					writer.WriteLine("[" + DateTime.Now + "." + WL_Count.ToString("D3") + "] " + message);
				}
			}
			catch
			{ }
		}
	}
}
