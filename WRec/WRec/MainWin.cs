using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Charlotte.Tools;
using System.IO;
using System.Threading;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// init
			{
				this.Cmbキーボード.SelectedIndex = 0;
				this.Cmbマウス.SelectedIndex = 0;
			}

			this.LoadData();
			this.RefreshUi();
			this.LoadPos();
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Btn記録開始.Focus();

			this.MT_Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;

			if (Gnd.I.Proc記録 != null)
			{
				this.EndRec();
				Gnd.I.Proc記録 = null;
			}
			if (Gnd.I.Proc再生 != null)
			{
				Gnd.I.Proc再生.Kill();
				Gnd.I.Proc再生 = null;
			}
			this.SavePos();
			this.SaveData();
		}

		private void LoadData()
		{
			this.Cmbキーボード.SelectedIndex = (int)Gnd.I.KeyRec;
			this.Cmbマウス.SelectedIndex = (int)Gnd.I.MouseRec;
			this.常に手前に表示するTToolStripMenuItem.Checked = Gnd.I.AlwaysTop;
			this.繰り返し再生するRToolStripMenuItem.Checked = Gnd.I.LoopMode;
		}

		private void SaveData()
		{
			Gnd.I.KeyRec = (Gnd.KeyRec_e)this.Cmbキーボード.SelectedIndex;
			Gnd.I.MouseRec = (Gnd.MouseRec_e)this.Cmbマウス.SelectedIndex;
			Gnd.I.AlwaysTop = this.常に手前に表示するTToolStripMenuItem.Checked;
			Gnd.I.LoopMode = this.繰り返し再生するRToolStripMenuItem.Checked;
		}

		private void RefreshUi()
		{
			this.TopMost = this.常に手前に表示するTToolStripMenuItem.Checked;

			switch (Gnd.I.Status)
			{
				case Gnd.Status_e.何もしていない:
					this.Btn記録開始.Enabled = true;
					this.Btn記録停止.Enabled = false;
					this.Btn再生開始.Enabled = true;
					this.Btn再生中断.Enabled = false;
					this.SouthText.Text = "";
					break;

				case Gnd.Status_e.記録中:
					this.Btn記録開始.Enabled = false;
					this.Btn記録停止.Enabled = true;
					this.Btn再生開始.Enabled = false;
					this.Btn再生中断.Enabled = false;
					this.SouthText.Text = "記録中";
					break;

				case Gnd.Status_e.再生中:
					this.Btn記録開始.Enabled = false;
					this.Btn記録停止.Enabled = false;
					this.Btn再生開始.Enabled = false;
					this.Btn再生中断.Enabled = true;
					this.SouthText.Text = "再生中";
					break;

				default:
					throw null;
			}
			this.Btnもう一度再生する.Enabled = Gnd.I.Status == Gnd.Status_e.何もしていない && Gnd.I.LastRanBatFile != null;
		}

		private void LoadPos()
		{
			if (Gnd.I.MainWin_L != -IntTools.IMAX) // ? ! 未設定
			{
				this.Left = Gnd.I.MainWin_L;
				this.Top = Gnd.I.MainWin_T;
			}
		}

		private void SavePos()
		{
			Gnd.I.MainWin_L = this.Left;
			Gnd.I.MainWin_T = this.Top;
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void その他の設定OToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MT_Enabled = false;
			try
			{
				using (Form f = new SettingWin())
				{
					f.ShowDialog(this);
				}
				//this.SavePos();
				this.SaveData();
				Gnd.I.SaveData();
				GC.Collect();
			}
			finally
			{
				this.MT_Enabled = true;
			}
		}

		private void 常に手前に表示するTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.常に手前に表示するTToolStripMenuItem.Checked = this.常に手前に表示するTToolStripMenuItem.Checked == false;
			this.RefreshUi();
		}

		private void 繰り返し再生するRToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.繰り返し再生するRToolStripMenuItem.Checked = this.繰り返し再生するRToolStripMenuItem.Checked == false;
			this.RefreshUi(); // 2bs
		}

		private void BtnOpenOutDir_Click(object sender, EventArgs e)
		{
			CommonTools.StartProc("CMD.exe", "/C START \"\" \"" + Gnd.I.OutDir + "\"");
			GC.Collect();
		}

		private void Btn記録開始_Click(object sender, EventArgs e)
		{
			if (Gnd.I.Proc記録 != null) // ? already started
				return;

			string recInputExe = "RecInput.exe";

			if (File.Exists(recInputExe) == false)
				recInputExe = @"..\..\..\..\RecInput.exe";

			FileTools.DeleteFileIfExist(Gnd.I.RecFile);
			Gnd.I.Proc記録 = CommonTools.StartProc(recInputExe, "/R \"" + Gnd.I.RecFile + "\" " + (Gnd.I.RecRCtrl停止 ? 1 : 0));
			this.RefreshUi();

			if (Gnd.I.RecStartMin)
				this.WindowState = FormWindowState.Minimized;

			GC.Collect();
		}

		private void Btn記録停止_Click(object sender, EventArgs e)
		{
			if (Gnd.I.Proc記録 == null) // ? not started
				return;

			this.MT_Enabled = false;

			try
			{
				this.EndRec();
				Gnd.I.Proc記録 = null;
				this.RefreshUi();
				this.RecEnded();
				this.Refresh();
				GC.Collect();
			}
			finally
			{
				this.MT_Enabled = true;
			}
		}

		private void EndRec()
		{
			using (NamedEventData rise = new NamedEventData(Consts.REC_INPUT_STOP_EVENT))
			{
				for (int millis = 0; ; )
				{
					rise.Set();

					if (millis < 200)
						millis++;

					if (Gnd.I.Proc記録.WaitForExit(millis))
						break;
				}
			}
		}

		private void RecEnded()
		{
			if (Gnd.I.RecEndUnmin)
				this.WindowState = FormWindowState.Normal;

			string destFile = TimeData.Now().GetSimpleString() + ".bat";

			for (; ; )
			{
				destFile = SaveLoadDialogs.SaveFile("保存先のバッチファイルを入力してください", "バッチ:bat", Gnd.I.OutDir, Path.GetFileName(destFile));

				if (destFile != null)
				{
					if (destFile.StartsWith("\\\\"))
					{
						MessageBox.Show(
							"ネットワークパスは使用できません。",
							"ファイル名に問題があります",
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning
							);
						continue;
					}
					if (JString.IsJString(destFile, true, false, false, true, false) == false)
					{
						MessageBox.Show(
							"Shift_JIS で表現出来ない文字は使用できません。",
							"ファイル名に問題があります",
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning
							);
						continue;
					}
					Gnd.I.OutDir = Path.GetDirectoryName(destFile);

					this.SaveData(); // 設定反映！！！

					BusyDlg.Perform(delegate
					{
						new BatchConv(Gnd.I.RecFile, destFile).Perform();
					});
				}
				break;
			}
		}

		private void Btn再生開始_Click(object sender, EventArgs e)
		{
			if (Gnd.I.Proc再生 != null) // ? already started
				return;

			this.MT_Enabled = false;

			try
			{
				string selFile = SaveLoadDialogs.LoadFile("実行するバッチファイルを選択して下さい", "バッチ:bat", Gnd.I.OutDir, "*.bat");

				if (selFile != null)
				{
					if (selFile.StartsWith("\\\\"))
					{
						MessageBox.Show(
							"ネットワークパスは開けません。",
							"ファイル名に問題があります",
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning
							);
						return;
					}
					if (JString.IsJString(selFile, true, false, false, true, false) == false)
					{
						MessageBox.Show(
							"Shift_JIS で表現出来ない文字を含むパスは開けません。",
							"ファイル名に問題があります",
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning
							);
						return;
					}
					if (StringTools.IsSame(".bat", Path.GetExtension(selFile), true) == false)
					{
						MessageBox.Show(
							"選択されたパスはバッチファイルではありません。",
							"ファイル名に問題があります",
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning
							);
						return;
					}
					Gnd.I.LastRanBatFile = selFile;
					Gnd.I.OutDir = Path.GetDirectoryName(Gnd.I.LastRanBatFile);

					Gnd.I.Proc再生 = CommonTools.StartProc(Gnd.I.LastRanBatFile);
					this.RefreshUi();

					if (Gnd.I.StartMin)
						this.WindowState = FormWindowState.Minimized;
				}
			}
			finally
			{
				GC.Collect();
				this.MT_Enabled = true;
			}
		}

		private void Btn再生中断_Click(object sender, EventArgs e)
		{
			if (Gnd.I.Proc再生 == null) // ? not started
				return;

			Gnd.I.Proc再生.Kill();
			Gnd.I.Proc再生 = null;
			this.RefreshUi();
			this.KilledBatch();
			GC.Collect();
		}

		private void KilledBatch()
		{
			if (Gnd.I.EndUnmin)
				this.WindowState = FormWindowState.Normal;

			MessageBox.Show("再生中のバッチファイルを強制終了しました。", "WRec", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void Btnもう一度再生する_Click(object sender, EventArgs e)
		{
			if (Gnd.I.Proc再生 != null) // ? already started
				return;

			if (Gnd.I.LastRanBatFile == null) // ? not ran batch
				return;

			if (File.Exists(Gnd.I.LastRanBatFile) == false) // ? lost batch
				return;

			Gnd.I.Proc再生 = CommonTools.StartProc(Gnd.I.LastRanBatFile);
			this.RefreshUi();
			GC.Collect();

			if (Gnd.I.StartMin)
				this.WindowState = FormWindowState.Minimized;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				if (Gnd.I.Proc記録 != null)
				{
					if (Gnd.I.Proc記録.WaitForExit(0))
					{
						Gnd.I.Proc記録 = null;
						this.RefreshUi();
						this.RecEnded();
						return;
					}
				}
				if (Gnd.I.Proc再生 != null)
				{
					if (Gnd.I.Proc再生.WaitForExit(0))
					{
						Gnd.I.Proc再生 = null;
						this.RefreshUi();
						this.BatchEnded();
						return;
					}
					if (Gnd.I.RCtrl中断 && CommonTools.IsRCtrlPressed())
					{
						Gnd.I.Proc再生.Kill();
						Gnd.I.Proc再生 = null;
						this.RefreshUi();
						this.KilledBatch();
						return;
					}
				}
				if (Gnd.I.AntiScreenSaver)
				{
					switch ((int)(this.MT_Count % 300L))
					{
						case 0:
							Utils.WriteLog("ES_SYSTEM_REQUIRED");
							Win32.SetThreadExecutionState(Win32.ExecutionState.ES_SYSTEM_REQUIRED);
							break;

						case 1:
							Utils.WriteLog("ES_DISPLAY_REQUIRED");
							Win32.SetThreadExecutionState(Win32.ExecutionState.ES_DISPLAY_REQUIRED);
							break;
					}
				}
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}

		private void BatchEnded()
		{
			if (Gnd.I.EndUnmin)
				this.WindowState = FormWindowState.Normal;

			this.SaveData(); // 設定反映！！！

			if (Gnd.I.LoopMode)
			{
				using (RestartDlg f = new RestartDlg())
				{
					f.RemainingTime = Gnd.I.LoopModeWaitSec * 10;

					f.ShowDialog(this);

					if (f.OkPressed)
						this.Btnもう一度再生する_Click(null, null);
				}
				GC.Collect();
			}
		}
	}
}
