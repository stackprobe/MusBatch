namespace Charlotte
{
	partial class SettingWin
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingWin));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.StrokeMillis = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.RecStrokeMillis = new System.Windows.Forms.TextBox();
			this.Recまとめ = new System.Windows.Forms.CheckBox();
			this.RecEndUnmin = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SamplingMillis = new System.Windows.Forms.TextBox();
			this.RecRCtrl停止 = new System.Windows.Forms.CheckBox();
			this.RecStartMin = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.DblClickMillis = new System.Windows.Forms.TextBox();
			this.ClickMillis = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.RecDblClickMillis = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.RecClickMillis = new System.Windows.Forms.TextBox();
			this.SndInputBatLocal = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.EndUnmin = new System.Windows.Forms.CheckBox();
			this.StartMin = new System.Windows.Forms.CheckBox();
			this.RCtrl中断 = new System.Windows.Forms.CheckBox();
			this.BtnReset = new System.Windows.Forms.Button();
			this.TTip = new System.Windows.Forms.ToolTip(this.components);
			this.BtnExport = new System.Windows.Forms.Button();
			this.BntImport = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.StrokeMillis);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.RecStrokeMillis);
			this.groupBox1.Controls.Add(this.Recまとめ);
			this.groupBox1.Controls.Add(this.RecEndUnmin);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.SamplingMillis);
			this.groupBox1.Controls.Add(this.RecRCtrl停止);
			this.groupBox1.Controls.Add(this.RecStartMin);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.DblClickMillis);
			this.groupBox1.Controls.Add(this.ClickMillis);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.RecDblClickMillis);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.RecClickMillis);
			this.groupBox1.Controls.Add(this.SndInputBatLocal);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(388, 485);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "記録";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 199);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(294, 20);
			this.label8.TabIndex = 8;
			this.label8.Text = "再生時のキーストロークのキー押下時間(ミリ秒)";
			// 
			// StrokeMillis
			// 
			this.StrokeMillis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.StrokeMillis.Location = new System.Drawing.Point(316, 196);
			this.StrokeMillis.MaxLength = 5;
			this.StrokeMillis.Name = "StrokeMillis";
			this.StrokeMillis.Size = new System.Drawing.Size(66, 27);
			this.StrokeMillis.TabIndex = 9;
			this.StrokeMillis.Text = "99999";
			this.StrokeMillis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.StrokeMillis.TextChanged += new System.EventHandler(this.StrokeMillis_TextChanged);
			this.StrokeMillis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StrokeMillis_KeyPress);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(294, 20);
			this.label3.TabIndex = 1;
			this.label3.Text = "キーストロークと見なすキーの押下時間(ミリ秒)";
			// 
			// RecStrokeMillis
			// 
			this.RecStrokeMillis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RecStrokeMillis.Location = new System.Drawing.Point(316, 64);
			this.RecStrokeMillis.MaxLength = 5;
			this.RecStrokeMillis.Name = "RecStrokeMillis";
			this.RecStrokeMillis.Size = new System.Drawing.Size(66, 27);
			this.RecStrokeMillis.TabIndex = 2;
			this.RecStrokeMillis.Text = "99999";
			this.RecStrokeMillis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.RecStrokeMillis.TextChanged += new System.EventHandler(this.RecStrokeMillis_TextChanged);
			this.RecStrokeMillis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RecStrokeMillis_KeyPress);
			// 
			// Recまとめ
			// 
			this.Recまとめ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Recまとめ.AutoSize = true;
			this.Recまとめ.Location = new System.Drawing.Point(68, 166);
			this.Recまとめ.Name = "Recまとめ";
			this.Recまとめ.Size = new System.Drawing.Size(314, 24);
			this.Recまとめ.TabIndex = 7;
			this.Recまとめ.Text = "複数の操作をなるたけ１つのコマンドにまとめる";
			this.Recまとめ.UseVisualStyleBackColor = true;
			this.Recまとめ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Recまとめ_KeyPress);
			// 
			// RecEndUnmin
			// 
			this.RecEndUnmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RecEndUnmin.AutoSize = true;
			this.RecEndUnmin.Location = new System.Drawing.Point(198, 404);
			this.RecEndUnmin.Name = "RecEndUnmin";
			this.RecEndUnmin.Size = new System.Drawing.Size(184, 24);
			this.RecEndUnmin.TabIndex = 19;
			this.RecEndUnmin.Text = "終了時に元のサイズに戻す";
			this.RecEndUnmin.UseVisualStyleBackColor = true;
			this.RecEndUnmin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RecEndUnmin_KeyPress);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 298);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(216, 20);
			this.label7.TabIndex = 14;
			this.label7.Text = "記録時のサンプリング間隔(ミリ秒)";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(316, 295);
			this.textBox1.MaxLength = 0;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(66, 27);
			this.textBox1.TabIndex = 15;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "10";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 331);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(216, 20);
			this.label6.TabIndex = 16;
			this.label6.Text = "再生時のサンプリング間隔(ミリ秒)";
			// 
			// SamplingMillis
			// 
			this.SamplingMillis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SamplingMillis.Location = new System.Drawing.Point(316, 328);
			this.SamplingMillis.MaxLength = 5;
			this.SamplingMillis.Name = "SamplingMillis";
			this.SamplingMillis.Size = new System.Drawing.Size(66, 27);
			this.SamplingMillis.TabIndex = 17;
			this.SamplingMillis.Text = "99999";
			this.SamplingMillis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.SamplingMillis.TextChanged += new System.EventHandler(this.SamplingMillis_TextChanged);
			this.SamplingMillis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SamplingMillis_KeyPress);
			// 
			// RecRCtrl停止
			// 
			this.RecRCtrl停止.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RecRCtrl停止.AutoSize = true;
			this.RecRCtrl停止.Location = new System.Drawing.Point(159, 434);
			this.RecRCtrl停止.Name = "RecRCtrl停止";
			this.RecRCtrl停止.Size = new System.Drawing.Size(223, 24);
			this.RecRCtrl停止.TabIndex = 20;
			this.RecRCtrl停止.Text = "右コントロール押下でも停止する";
			this.RecRCtrl停止.UseVisualStyleBackColor = true;
			this.RecRCtrl停止.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RecRCtrl停止_KeyPress);
			// 
			// RecStartMin
			// 
			this.RecStartMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RecStartMin.AutoSize = true;
			this.RecStartMin.Location = new System.Drawing.Point(237, 374);
			this.RecStartMin.Name = "RecStartMin";
			this.RecStartMin.Size = new System.Drawing.Size(145, 24);
			this.RecStartMin.TabIndex = 18;
			this.RecStartMin.Text = "開始時に最小化する";
			this.RecStartMin.UseVisualStyleBackColor = true;
			this.RecStartMin.CheckedChanged += new System.EventHandler(this.RecStartMin_CheckedChanged);
			this.RecStartMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RecStartMin_KeyPress);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 265);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(294, 20);
			this.label5.TabIndex = 12;
			this.label5.Text = "再生時のダブルクリックのクリック間隔(ミリ秒)";
			// 
			// DblClickMillis
			// 
			this.DblClickMillis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DblClickMillis.Location = new System.Drawing.Point(316, 262);
			this.DblClickMillis.MaxLength = 5;
			this.DblClickMillis.Name = "DblClickMillis";
			this.DblClickMillis.Size = new System.Drawing.Size(66, 27);
			this.DblClickMillis.TabIndex = 13;
			this.DblClickMillis.Text = "99999";
			this.DblClickMillis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.DblClickMillis.TextChanged += new System.EventHandler(this.DblClickMillis_TextChanged);
			this.DblClickMillis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DblClickMillis_KeyPress);
			// 
			// ClickMillis
			// 
			this.ClickMillis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ClickMillis.Location = new System.Drawing.Point(316, 229);
			this.ClickMillis.MaxLength = 5;
			this.ClickMillis.Name = "ClickMillis";
			this.ClickMillis.Size = new System.Drawing.Size(66, 27);
			this.ClickMillis.TabIndex = 11;
			this.ClickMillis.Text = "99999";
			this.ClickMillis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ClickMillis.TextChanged += new System.EventHandler(this.ClickMillis_TextChanged);
			this.ClickMillis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ClickMillis_KeyPress);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 232);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(268, 20);
			this.label4.TabIndex = 10;
			this.label4.Text = "再生時のクリックのボタン押下時間(ミリ秒)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 133);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(281, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "ダブルクリックと見なすクリック間隔(ミリ秒)";
			// 
			// RecDblClickMillis
			// 
			this.RecDblClickMillis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RecDblClickMillis.Location = new System.Drawing.Point(316, 130);
			this.RecDblClickMillis.MaxLength = 5;
			this.RecDblClickMillis.Name = "RecDblClickMillis";
			this.RecDblClickMillis.Size = new System.Drawing.Size(66, 27);
			this.RecDblClickMillis.TabIndex = 6;
			this.RecDblClickMillis.Text = "99999";
			this.RecDblClickMillis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.RecDblClickMillis.TextChanged += new System.EventHandler(this.RecDblClickMillis_TextChanged);
			this.RecDblClickMillis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RecDblClickMillis_KeyPress);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(268, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "クリックと見なすボタンの押下時間(ミリ秒)";
			// 
			// RecClickMillis
			// 
			this.RecClickMillis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RecClickMillis.Location = new System.Drawing.Point(316, 97);
			this.RecClickMillis.MaxLength = 5;
			this.RecClickMillis.Name = "RecClickMillis";
			this.RecClickMillis.Size = new System.Drawing.Size(66, 27);
			this.RecClickMillis.TabIndex = 4;
			this.RecClickMillis.Text = "99999";
			this.RecClickMillis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.RecClickMillis.TextChanged += new System.EventHandler(this.RecClickMillis_TextChanged);
			this.RecClickMillis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RecClickMillis_KeyPress);
			// 
			// SndInputBatLocal
			// 
			this.SndInputBatLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SndInputBatLocal.AutoSize = true;
			this.SndInputBatLocal.Location = new System.Drawing.Point(121, 26);
			this.SndInputBatLocal.Name = "SndInputBatLocal";
			this.SndInputBatLocal.Size = new System.Drawing.Size(261, 24);
			this.SndInputBatLocal.TabIndex = 0;
			this.SndInputBatLocal.Text = "SndInput.exe をローカル名で呼び出す";
			this.SndInputBatLocal.UseVisualStyleBackColor = true;
			this.SndInputBatLocal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SndInputBatLocal_KeyPress);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.EndUnmin);
			this.groupBox2.Controls.Add(this.StartMin);
			this.groupBox2.Controls.Add(this.RCtrl中断);
			this.groupBox2.Location = new System.Drawing.Point(406, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(253, 132);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "再生";
			// 
			// EndUnmin
			// 
			this.EndUnmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EndUnmin.AutoSize = true;
			this.EndUnmin.Location = new System.Drawing.Point(63, 56);
			this.EndUnmin.Name = "EndUnmin";
			this.EndUnmin.Size = new System.Drawing.Size(184, 24);
			this.EndUnmin.TabIndex = 1;
			this.EndUnmin.Text = "終了時に元のサイズに戻す";
			this.EndUnmin.UseVisualStyleBackColor = true;
			this.EndUnmin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EndUnmin_KeyPress);
			// 
			// StartMin
			// 
			this.StartMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.StartMin.AutoSize = true;
			this.StartMin.Location = new System.Drawing.Point(102, 26);
			this.StartMin.Name = "StartMin";
			this.StartMin.Size = new System.Drawing.Size(145, 24);
			this.StartMin.TabIndex = 0;
			this.StartMin.Text = "開始時に最小化する";
			this.StartMin.UseVisualStyleBackColor = true;
			this.StartMin.CheckedChanged += new System.EventHandler(this.StartMin_CheckedChanged);
			this.StartMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartMin_KeyPress);
			// 
			// RCtrl中断
			// 
			this.RCtrl中断.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RCtrl中断.AutoSize = true;
			this.RCtrl中断.Location = new System.Drawing.Point(24, 86);
			this.RCtrl中断.Name = "RCtrl中断";
			this.RCtrl中断.Size = new System.Drawing.Size(223, 24);
			this.RCtrl中断.TabIndex = 2;
			this.RCtrl中断.Text = "右コントロール押下でも中断する";
			this.RCtrl中断.UseVisualStyleBackColor = true;
			this.RCtrl中断.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RCtrl中断_KeyPress);
			// 
			// BtnReset
			// 
			this.BtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnReset.Location = new System.Drawing.Point(555, 457);
			this.BtnReset.Name = "BtnReset";
			this.BtnReset.Size = new System.Drawing.Size(104, 40);
			this.BtnReset.TabIndex = 4;
			this.BtnReset.Text = "リセット";
			this.TTip.SetToolTip(this.BtnReset, "このダイアログを開いた時の状態に戻します");
			this.BtnReset.UseVisualStyleBackColor = true;
			this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
			// 
			// BtnExport
			// 
			this.BtnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExport.Location = new System.Drawing.Point(536, 355);
			this.BtnExport.Name = "BtnExport";
			this.BtnExport.Size = new System.Drawing.Size(123, 40);
			this.BtnExport.TabIndex = 2;
			this.BtnExport.Text = "エクスポート";
			this.BtnExport.UseVisualStyleBackColor = true;
			this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
			// 
			// BntImport
			// 
			this.BntImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BntImport.Location = new System.Drawing.Point(536, 401);
			this.BntImport.Name = "BntImport";
			this.BntImport.Size = new System.Drawing.Size(123, 40);
			this.BntImport.TabIndex = 3;
			this.BntImport.Text = "インポート";
			this.BntImport.UseVisualStyleBackColor = true;
			this.BntImport.Click += new System.EventHandler(this.BntImport_Click);
			// 
			// SettingWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(671, 509);
			this.Controls.Add(this.BntImport);
			this.Controls.Add(this.BtnExport);
			this.Controls.Add(this.BtnReset);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WRec - 設定";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingWin_FormClosed);
			this.Load += new System.EventHandler(this.SettingWin_Load);
			this.Shown += new System.EventHandler(this.SettingWin_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox DblClickMillis;
		private System.Windows.Forms.TextBox ClickMillis;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox RecDblClickMillis;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox RecClickMillis;
		private System.Windows.Forms.CheckBox SndInputBatLocal;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox RCtrl中断;
		private System.Windows.Forms.Button BtnReset;
		private System.Windows.Forms.CheckBox RecRCtrl停止;
		private System.Windows.Forms.CheckBox RecStartMin;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox SamplingMillis;
		private System.Windows.Forms.CheckBox StartMin;
		private System.Windows.Forms.CheckBox RecEndUnmin;
		private System.Windows.Forms.CheckBox EndUnmin;
		private System.Windows.Forms.CheckBox Recまとめ;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox StrokeMillis;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox RecStrokeMillis;
		private System.Windows.Forms.ToolTip TTip;
		private System.Windows.Forms.Button BtnExport;
		private System.Windows.Forms.Button BntImport;
	}
}
