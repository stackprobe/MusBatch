using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public partial class SettingWin : Form
	{
		public SettingWin()
		{
			InitializeComponent();
		}

		private void SettingWin_Load(object sender, EventArgs e)
		{
			this.LoadData();
		}

		private void SettingWin_Shown(object sender, EventArgs e)
		{
			// refresh_ui
			{
				this.RecStartMin_CheckedChanged(null, null);
				this.StartMin_CheckedChanged(null, null);
			}

			Utils.PostShown(this);
		}

		private void SettingWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void SettingWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.SaveData();
		}

		// ---- 全フィールドなめる系 ----

		private void LoadData()
		{
			this.SndInputBatLocal.Checked = Gnd.I.SndInputBatLocal;
			this.RecStrokeMillis.Text = "" + Gnd.I.RecStrokeMillis;
			this.RecClickMillis.Text = "" + Gnd.I.RecClickMillis;
			this.RecDblClickMillis.Text = "" + Gnd.I.RecDblClickMillis;
			this.Recまとめ.Checked = Gnd.I.Recまとめ;
			this.StrokeMillis.Text = "" + Gnd.I.StrokeMillis;
			this.ClickMillis.Text = "" + Gnd.I.ClickMillis;
			this.DblClickMillis.Text = "" + Gnd.I.DblClickMillis;
			this.SamplingMillis.Text = "" + Gnd.I.SamplingMillis;
			this.RecStartMin.Checked = Gnd.I.RecStartMin;
			this.RecEndUnmin.Checked = Gnd.I.RecEndUnmin;
			this.RecRCtrl停止.Checked = Gnd.I.RecRCtrl停止;
			this.StartMin.Checked = Gnd.I.StartMin;
			this.EndUnmin.Checked = Gnd.I.EndUnmin;
			this.RCtrl中断.Checked = Gnd.I.RCtrl中断;
			// ここへ追加..
		}

		private void SaveData()
		{
			Gnd.I.SndInputBatLocal = this.SndInputBatLocal.Checked;
			Gnd.I.RecStrokeMillis = IntTools.ToInt(this.RecStrokeMillis.Text, 0, 60000, Gnd.I.RecStrokeMillis);
			Gnd.I.RecClickMillis = IntTools.ToInt(this.RecClickMillis.Text, 0, 60000, Gnd.I.RecClickMillis);
			Gnd.I.RecDblClickMillis = IntTools.ToInt(this.RecDblClickMillis.Text, 0, 60000, Gnd.I.RecDblClickMillis);
			Gnd.I.Recまとめ = this.Recまとめ.Checked;
			Gnd.I.StrokeMillis = IntTools.ToInt(this.StrokeMillis.Text, 0, 60000, Gnd.I.StrokeMillis);
			Gnd.I.ClickMillis = IntTools.ToInt(this.ClickMillis.Text, 0, 60000, Gnd.I.ClickMillis);
			Gnd.I.DblClickMillis = IntTools.ToInt(this.DblClickMillis.Text, 0, 60000, Gnd.I.DblClickMillis);
			Gnd.I.SamplingMillis = IntTools.ToInt(this.SamplingMillis.Text, 1, 60000, Gnd.I.SamplingMillis);
			Gnd.I.RecStartMin = this.RecStartMin.Checked;
			Gnd.I.RecEndUnmin = this.RecEndUnmin.Checked;
			Gnd.I.RecRCtrl停止 = this.RecRCtrl停止.Checked;
			Gnd.I.StartMin = this.StartMin.Checked;
			Gnd.I.EndUnmin = this.EndUnmin.Checked;
			Gnd.I.RCtrl中断 = this.RCtrl中断.Checked;
			// ここへ追加..
		}

		private void DoExport(string file)
		{
			XNode xml = new XNode("MusBatchSetting");

			xml.Children.Add(new XNode("SndInputBatLocal", StringTools.ToString(this.SndInputBatLocal.Checked)));
			xml.Children.Add(new XNode("RecStrokeMillis", this.RecStrokeMillis.Text));
			xml.Children.Add(new XNode("RecClickMillis", this.RecClickMillis.Text));
			xml.Children.Add(new XNode("RecDblClickMillis", this.RecDblClickMillis.Text));
			xml.Children.Add(new XNode("RecMatome", StringTools.ToString(this.Recまとめ.Checked)));
			xml.Children.Add(new XNode("StrokeMillis", this.StrokeMillis.Text));
			xml.Children.Add(new XNode("ClickMillis", this.ClickMillis.Text));
			xml.Children.Add(new XNode("DblClickMillis", this.DblClickMillis.Text));
			xml.Children.Add(new XNode("SamplingMillis", this.SamplingMillis.Text));
			xml.Children.Add(new XNode("RecStartMin", StringTools.ToString(this.RecStartMin.Checked)));
			xml.Children.Add(new XNode("RecEndUnmin", StringTools.ToString(this.RecEndUnmin.Checked)));
			xml.Children.Add(new XNode("RecRCtrlTeishi", StringTools.ToString(this.RecRCtrl停止.Checked)));
			xml.Children.Add(new XNode("StartMin", StringTools.ToString(this.StartMin.Checked)));
			xml.Children.Add(new XNode("EndUnmin", StringTools.ToString(this.EndUnmin.Checked)));
			xml.Children.Add(new XNode("RCtrlChuudan", StringTools.ToString(this.RCtrl中断.Checked)));
			// ここへ追加..

			xml.Save(file);
		}

		private void DoImport(string file)
		{
			XNode xml = XNode.Load(file);

			this.SndInputBatLocal.Checked = StringTools.ToFlag(xml.GetNode("SndInputBatLocal").Value);
			this.RecStrokeMillis.Text = xml.GetNode("RecStrokeMillis").Value;
			this.RecClickMillis.Text = xml.GetNode("RecClickMillis").Value;
			this.RecDblClickMillis.Text = xml.GetNode("RecDblClickMillis").Value;
			this.Recまとめ.Checked = StringTools.ToFlag(xml.GetNode("RecMatome").Value);
			this.StrokeMillis.Text = xml.GetNode("StrokeMillis").Value;
			this.ClickMillis.Text = xml.GetNode("ClickMillis").Value;
			this.DblClickMillis.Text = xml.GetNode("DblClickMillis").Value;
			this.SamplingMillis.Text = xml.GetNode("SamplingMillis").Value;
			this.RecStartMin.Checked = StringTools.ToFlag(xml.GetNode("RecStartMin").Value);
			this.RecEndUnmin.Checked = StringTools.ToFlag(xml.GetNode("RecEndUnmin").Value);
			this.RecRCtrl停止.Checked = StringTools.ToFlag(xml.GetNode("RecRCtrlTeishi").Value);
			this.StartMin.Checked = StringTools.ToFlag(xml.GetNode("StartMin").Value);
			this.EndUnmin.Checked = StringTools.ToFlag(xml.GetNode("EndUnmin").Value);
			this.RCtrl中断.Checked = StringTools.ToFlag(xml.GetNode("RCtrlChuudan").Value);
			// ここへ追加..
		}

		// ----

		private void BtnReset_Click(object sender, EventArgs e)
		{
			this.LoadData();
		}

		private void MillisCommonChanged(TextBox tb, int minval = 0)
		{
			try
			{
				IntTools.Parse(tb.Text, minval, 60000);

				tb.ForeColor = new TextBox().ForeColor;
				tb.BackColor = new TextBox().BackColor;
			}
			catch
			{
				tb.ForeColor = Color.Red;
				tb.BackColor = Color.FromArgb(255, 255, 200);
			}
		}

		private void RecStrokeMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.RecStrokeMillis);
		}

		private void RecClickMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.RecClickMillis);
		}

		private void RecDblClickMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.RecDblClickMillis);
		}

		private void StrokeMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.StrokeMillis);
		}

		private void ClickMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.ClickMillis);
		}

		private void DblClickMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.DblClickMillis);
		}

		private void SamplingMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.SamplingMillis, 1);
		}

		private void RecStartMin_CheckedChanged(object sender, EventArgs e)
		{
			this.RecEndUnmin.Enabled = this.RecStartMin.Checked;
		}

		private void StartMin_CheckedChanged(object sender, EventArgs e)
		{
			this.EndUnmin.Enabled = this.StartMin.Checked;
		}

		private void CommonKeyPress(KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				SendKeys.Send("{TAB}");
				e.Handled = true;
			}
		}

		private void SndInputBatLocal_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecStrokeMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecClickMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecDblClickMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void Recまとめ_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void StrokeMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void ClickMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void DblClickMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void SamplingMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecStartMin_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecEndUnmin_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecRCtrl停止_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void StartMin_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void EndUnmin_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RCtrl中断_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private static string _settingDir = null;

		private static string GetSettingDir()
		{
			if (_settingDir == null)
			{
				string dir = "Setting";

				if (Directory.Exists(dir) == false)
					dir = @"..\..\..\..\doc\Setting";

				_settingDir = Path.GetFullPath(dir);
			}
			return _settingDir;
		}

		private void BtnExport_Click(object sender, EventArgs e)
		{
			try
			{
				string destFile = TimeData.Now().GetSimpleString() + ".xml";

				for (; ; )
				{
					//SaveFileDialogクラスのインスタンスを作成
					using (SaveFileDialog sfd = new SaveFileDialog())
					{
						//はじめのファイル名を指定する
						//sfd.FileName = "新しいファイル.html";
						sfd.FileName = Path.GetFileName(destFile);
						//はじめに表示されるフォルダを指定する
						//sfd.InitialDirectory = @"C:\";
						//sfd.InitialDirectory = Directory.GetCurrentDirectory();
						sfd.InitialDirectory = GetSettingDir();
						//[ファイルの種類]に表示される選択肢を指定する
						sfd.Filter =
							//"HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*";
							"設定ファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
						//[ファイルの種類]ではじめに
						//「すべてのファイル」が選択されているようにする
						//sfd.FilterIndex = 2;
						sfd.FilterIndex = 1;
						//タイトルを設定する
						sfd.Title = "保存先の設定ファイルを入力してください";
						//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
						sfd.RestoreDirectory = true;
						//既に存在するファイル名を指定したとき警告する
						//デフォルトでTrueなので指定する必要はない
						sfd.OverwritePrompt = true;
						//存在しないパスが指定されたとき警告を表示する
						//デフォルトでTrueなので指定する必要はない
						sfd.CheckPathExists = true;

						//ダイアログを表示する
						if (sfd.ShowDialog() == DialogResult.OK)
						{
							Directory.SetCurrentDirectory(BootTools.SelfDir); // 2bs

							//OKボタンがクリックされたとき
							//選択されたファイル名を表示する
							//Console.WriteLine(sfd.FileName);
							destFile = sfd.FileName;

#if false
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
#endif
							this.DoExport(destFile);
						}
						Directory.SetCurrentDirectory(BootTools.SelfDir); // 2bs
					}
					break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					"" + ex,
					"エクスポートに失敗しました",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
					);
			}
		}

		private void BntImport_Click(object sender, EventArgs e)
		{
			try
			{
				//OpenFileDialogクラスのインスタンスを作成
				using (OpenFileDialog ofd = new OpenFileDialog())
				{
					//はじめのファイル名を指定する
					//はじめに「ファイル名」で表示される文字列を指定する
					ofd.FileName = "*.xml";
					//はじめに表示されるフォルダを指定する
					//指定しない（空の文字列）の時は、現在のディレクトリが表示される
					//ofd.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
					ofd.InitialDirectory = GetSettingDir();
					//[ファイルの種類]に表示される選択肢を指定する
					//指定しないとすべてのファイルが表示される
					ofd.Filter =
						"設定ファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
					//"HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*";
					//[ファイルの種類]ではじめに
					//「すべてのファイル」が選択されているようにする
					// フィルタの何番目かってことみたい...
					ofd.FilterIndex = 0;
					//タイトルを設定する
					ofd.Title = "インポートする設定ファイルを選択して下さい";
					//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
					ofd.RestoreDirectory = true;
					//存在しないファイルの名前が指定されたとき警告を表示する
					//デフォルトでTrueなので指定する必要はない
					ofd.CheckFileExists = true;
					//存在しないパスが指定されたとき警告を表示する
					//デフォルトでTrueなので指定する必要はない
					ofd.CheckPathExists = true;

					//ダイアログを表示する
					if (ofd.ShowDialog() == DialogResult.OK) // using ofd
					{
						Directory.SetCurrentDirectory(BootTools.SelfDir); // 2bs

						string selFile = ofd.FileName;

#if false
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
#endif
						this.DoImport(selFile);
					}
					Directory.SetCurrentDirectory(BootTools.SelfDir); // 2bs
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					"" + ex,
					"インポートに失敗しました",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
					);
			}
		}
	}
}
