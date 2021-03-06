﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	public partial class RestartDlg : Form
	{
		public int RemainingTime = 50; // 単位 == 100 ミリ秒

		// <---- prm

		public bool OkPressed = false;

		// <---- ret

		public RestartDlg()
		{
			InitializeComponent();
		}

		private void RestartDlg_Load(object sender, EventArgs e)
		{
			this.UpdateMainMessage();
		}

		private void RestartDlg_Shown(object sender, EventArgs e)
		{
			this.BtnCancel.Focus();
			this.MT_Enabled = true;
		}

		private void RestartDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void RestartDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			this.OkPressed = true;
			this.Close();
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
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
				if (Gnd.I.RestartDlgTopMost && 1 <= this.MT_Count && this.MT_Count <= 3) // 2bs
				{
					this.TopMost = this.MT_Count % 2 == 1;
				}
				this.RemainingTime--;

				if (this.RemainingTime <= 0)
				{
					this.OkPressed = true;

					this.MT_Enabled = false;
					this.Close();
					return;
				}
				this.UpdateMainMessage();
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}

		private void UpdateMainMessage()
		{
			this.MainMessage.Text = (this.RemainingTime / 10) + " 秒後に同じバッチファイルを再生します...";
		}
	}
}
