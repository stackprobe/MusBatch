﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using System.Diagnostics;
using System.Windows.Forms;

namespace Charlotte
{
	public class Gnd
	{
		private static Gnd _i = null;

		public static Gnd I
		{
			get
			{
				if (_i == null)
					_i = new Gnd();

				return _i;
			}
		}

		private Gnd()
		{ }

		public readonly string ConfFile = StringTools.Combine(BootTools.SelfDir, "WRec.conf");

		// ---- Items ----

		public bool IgnoreVk16To18 = true;
		public int LoopModeWaitSec = 5;
		public bool RestartDlgTopMost = true;
		public bool AntiScreenSaver = false;
		public int ClearLogCycle = 1000;
		// ここへ追加..

		// ----

		public void LoadConf()
		{
			try
			{
				string[] lines = File.ReadAllLines(ConfFile, StringTools.ENCODING_SJIS);
				List<string> dest = new List<string>();

				foreach (string line in lines)
					if (line != "" && line[0] != ';')
						dest.Add(line);

				lines = dest.ToArray();
				int c = 0;

				if (int.Parse(lines[c++]) != lines.Length)
					throw new Exception("Bad element number");

				// ---- Items ----

				IgnoreVk16To18 = int.Parse(lines[c++]) != 0;
				LoopModeWaitSec = Utils.ToRange(int.Parse(lines[c++]), 1, 86400000); // 1 sec ～ 1000 day
				RestartDlgTopMost = int.Parse(lines[c++]) != 0;
				AntiScreenSaver = int.Parse(lines[c++]) != 0;
				ClearLogCycle = Utils.ToRange(int.Parse(lines[c++]), 10, 1000000000); // 10 ～ IMAX
				// ここへ追加..

				// ----

				// 2019.7.2
				// \e 排除
				// 項目が追加されたとき、古い設定ファイルを読み込むと \e を項目として読んでしまう。
				// この場合、例外(out of range)を投げてデフォルト値のままになってくれた方が良い。
				// \e による行の過不足のチェックは有効要素数のチェックで代替する。

				Utils.WriteLog("Conf...");

				// ---- Items ---- log

				Utils.WriteLog(IgnoreVk16To18);
				Utils.WriteLog(LoopModeWaitSec);
				Utils.WriteLog(RestartDlgTopMost);
				Utils.WriteLog(AntiScreenSaver);
				Utils.WriteLog(ClearLogCycle);
				// ここへ追加..

				// ----
			}
			catch (Exception e)
			{
				Utils.WriteLog(e);
			}
		}

		public readonly string DatFile = StringTools.Combine(BootTools.SelfDir, "WRec.dat");

		public int MainWin_L = -IntTools.IMAX; // -IMAX == 未設定
		public int MainWin_T = 0;
		public bool SndInputBatLocal = false;
		public int RecStrokeMillis = 150; // 0 ～ 60000
		public int RecClickMillis = 150; // 0 ～ 60000
		public int RecDblClickMillis = 200; // 0 ～ 60000
		public bool Recまとめ = false;
		public int StrokeMillis = 30; // 0 ～ 60000
		public int ClickMillis = 30; // 0 ～ 60000
		public int DblClickMillis = 30; // 0 ～ 60000
		public int SamplingMillis = 10; // 1 ～ 60000
		public bool RecStartMin = false;
		public bool RecEndUnmin = false;
		public bool RecRCtrl停止 = false;
		public bool StartMin = false;
		public bool EndUnmin = false;
		public bool RCtrl中断 = false;

		public enum KeyRec_e
		{
			記録する,
			記録しない,
		}

		public enum MouseRec_e
		{
			記録する,
			ボタン操作のみ,
			記録しない,
		}

		public KeyRec_e KeyRec = KeyRec_e.記録する;
		public MouseRec_e MouseRec = MouseRec_e.記録する;
		public bool AlwaysTop = false;
		public bool LoopMode = false;

		public string OutDir = BootTools.SelfDir;

		// ---- 全フィールドなめる系 ----

		public void LoadData()
		{
			if (File.Exists(DatFile) == false)
				return;

			try
			{
				string[] lines = File.ReadAllLines(DatFile, Encoding.UTF8);
				int c = 0;

				MainWin_L = int.Parse(lines[c++]);
				MainWin_T = int.Parse(lines[c++]);
				SndInputBatLocal = StringTools.ToFlag(lines[c++]);
				RecStrokeMillis = IntTools.Parse(lines[c++], 0, 60000);
				RecClickMillis = IntTools.Parse(lines[c++], 0, 60000);
				RecDblClickMillis = IntTools.Parse(lines[c++], 0, 60000);
				Recまとめ = StringTools.ToFlag(lines[c++]);
				StrokeMillis = IntTools.Parse(lines[c++], 0, 60000);
				ClickMillis = IntTools.Parse(lines[c++], 0, 60000);
				DblClickMillis = IntTools.Parse(lines[c++], 0, 60000);
				SamplingMillis = IntTools.Parse(lines[c++], 1, 60000);
				RecStartMin = StringTools.ToFlag(lines[c++]);
				RecEndUnmin = StringTools.ToFlag(lines[c++]);
				RecRCtrl停止 = StringTools.ToFlag(lines[c++]);
				StartMin = StringTools.ToFlag(lines[c++]);
				EndUnmin = StringTools.ToFlag(lines[c++]);
				RCtrl中断 = StringTools.ToFlag(lines[c++]);
				KeyRec = (KeyRec_e)int.Parse(lines[c++]);
				MouseRec = (MouseRec_e)int.Parse(lines[c++]);
				AlwaysTop = StringTools.ToFlag(lines[c++]);
				LoopMode = StringTools.ToFlag(lines[c++]);
				OutDir = EraseDq(lines[c++]);
				// ここへ追加..
			}
			catch (Exception e)
			{
				Utils.WriteLog(e);
			}
		}

		public void SaveData()
		{
			try
			{
				List<string> lines = new List<string>();

				lines.Add("" + MainWin_L);
				lines.Add("" + MainWin_T);
				lines.Add(StringTools.ToString(SndInputBatLocal));
				lines.Add("" + RecStrokeMillis);
				lines.Add("" + RecClickMillis);
				lines.Add("" + RecDblClickMillis);
				lines.Add(StringTools.ToString(Recまとめ));
				lines.Add("" + StrokeMillis);
				lines.Add("" + ClickMillis);
				lines.Add("" + DblClickMillis);
				lines.Add("" + SamplingMillis);
				lines.Add(StringTools.ToString(RecStartMin));
				lines.Add(StringTools.ToString(RecEndUnmin));
				lines.Add(StringTools.ToString(RecRCtrl停止));
				lines.Add(StringTools.ToString(StartMin));
				lines.Add(StringTools.ToString(EndUnmin));
				lines.Add(StringTools.ToString(RCtrl中断));
				lines.Add("" + (int)KeyRec);
				lines.Add("" + (int)MouseRec);
				lines.Add(StringTools.ToString(AlwaysTop));
				lines.Add(StringTools.ToString(LoopMode));
				lines.Add("\"" + OutDir + "\"");
				// ここへ追加..

				File.WriteAllLines(DatFile, lines, Encoding.UTF8);
			}
			catch (Exception e)
			{
				Utils.WriteLog(e);
			}
		}

		// ----

		private static string EraseDq(string str)
		{
			if (str.StartsWith("\""))
				str = str.Substring(1);

			if (str.EndsWith("\""))
				str = str.Substring(0, str.Length - 1);

			return str;
		}

		public enum Status_e
		{
			何もしていない,
			記録中,
			再生中,
		}

		public Status_e Status
		{
			get
			{
				if (Proc記録 != null)
					return Status_e.記録中;

				if (Proc再生 != null)
					return Status_e.再生中;

				return Status_e.何もしていない;
			}
		}

		public Process Proc記録 = null; // null == 未実行
		public Process Proc再生 = null; // null == 未実行

		public readonly string RecFile = WorkBenchDir.I.MakePath();
		public string LastRanBatFile = null;

		public bool Is初回起動()
		{
			return File.Exists(DatFile) == false; // ? SaveData()を1度も実行していない。
		}
	}
}
