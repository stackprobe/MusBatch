using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Text;
using System.IO;

namespace Charlotte
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			BootTools.OnBoot();

			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			SystemEvents.SessionEnding += new SessionEndingEventHandler(SessionEnding);

			Mutex procMutex = new Mutex(false, APP_IDENT);

			if (procMutex.WaitOne(0) && GlobalProcMtx.Create(APP_IDENT, APP_TITLE))
			{
				CheckSelfDir();
				CheckCopiedExe();

				Utils.AntiWindowsDefenderSmartScreen();

				Gnd.I.LoadConf();
				Gnd.I.LoadData();

				// orig >

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWin());

				// < orig

				Gnd.I.SaveData();

				GlobalProcMtx.Release();
				procMutex.ReleaseMutex();
			}
			procMutex.Close();
		}

		public const string APP_IDENT = "{9ab69a07-ed8d-4b03-88a0-a6075ba9a21c}";
		public const string APP_TITLE = "WRec";

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[Application_ThreadException]\n" + e.Exception,
					APP_TITLE + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(1);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[CurrentDomain_UnhandledException]\n" + e.ExceptionObject,
					APP_TITLE + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(2);
		}

		private static void SessionEnding(object sender, SessionEndingEventArgs e)
		{
			Environment.Exit(3);
		}

		private static void CheckSelfDir()
		{
			string dir = BootTools.SelfDir;
			Encoding SJIS = Encoding.GetEncoding(932);

			if (dir != SJIS.GetString(SJIS.GetBytes(dir)))
			{
				MessageBox.Show(
					"Shift_JIS に変換出来ない文字を含むパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(4);
			}
			if (dir.StartsWith("\\\\"))
			{
				MessageBox.Show(
					"ネットワークフォルダからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(5);
			}
		}

		private static void CheckCopiedExe()
		{
			if (File.Exists("RecInput.exe")) // リリースに含まれるファイル
				return;

			if (Directory.Exists(@"..\Debug")) // ? devenv
				return;

			MessageBox.Show(
				"WHY AM I ALONE ?",
				"",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);

			Environment.Exit(6);
		}
	}
}
