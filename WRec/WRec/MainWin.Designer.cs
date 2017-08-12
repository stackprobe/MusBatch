namespace Charlotte
{
	partial class MainWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.アプリケーションAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.常に手前に表示するTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.繰り返し再生するRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.その他の設定OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.Btn記録停止 = new System.Windows.Forms.Button();
			this.Btn記録開始 = new System.Windows.Forms.Button();
			this.Cmbマウス = new System.Windows.Forms.ComboBox();
			this.Cmbキーボード = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Btnもう一度再生する = new System.Windows.Forms.Button();
			this.Btn再生中断 = new System.Windows.Forms.Button();
			this.Btn再生開始 = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.SouthText = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.BtnOpenOutDir = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリケーションAToolStripMenuItem,
            this.設定SToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(272, 26);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// アプリケーションAToolStripMenuItem
			// 
			this.アプリケーションAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem});
			this.アプリケーションAToolStripMenuItem.Name = "アプリケーションAToolStripMenuItem";
			this.アプリケーションAToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.アプリケーションAToolStripMenuItem.Text = "アプリケーション(&A)";
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
			// 
			// 設定SToolStripMenuItem
			// 
			this.設定SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.常に手前に表示するTToolStripMenuItem,
            this.繰り返し再生するRToolStripMenuItem,
            this.toolStripMenuItem1,
            this.その他の設定OToolStripMenuItem});
			this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
			this.設定SToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
			this.設定SToolStripMenuItem.Text = "設定(&S)";
			// 
			// 常に手前に表示するTToolStripMenuItem
			// 
			this.常に手前に表示するTToolStripMenuItem.Name = "常に手前に表示するTToolStripMenuItem";
			this.常に手前に表示するTToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.常に手前に表示するTToolStripMenuItem.Text = "常に手前に表示する(&T)";
			this.常に手前に表示するTToolStripMenuItem.Click += new System.EventHandler(this.常に手前に表示するTToolStripMenuItem_Click);
			// 
			// 繰り返し再生するRToolStripMenuItem
			// 
			this.繰り返し再生するRToolStripMenuItem.Name = "繰り返し再生するRToolStripMenuItem";
			this.繰り返し再生するRToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.繰り返し再生するRToolStripMenuItem.Text = "繰り返し再生する(&R)";
			this.繰り返し再生するRToolStripMenuItem.Click += new System.EventHandler(this.繰り返し再生するRToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(199, 6);
			// 
			// その他の設定OToolStripMenuItem
			// 
			this.その他の設定OToolStripMenuItem.Name = "その他の設定OToolStripMenuItem";
			this.その他の設定OToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.その他の設定OToolStripMenuItem.Text = "その他の設定(&O)";
			this.その他の設定OToolStripMenuItem.Click += new System.EventHandler(this.その他の設定OToolStripMenuItem_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.Btn記録停止);
			this.groupBox1.Controls.Add(this.Btn記録開始);
			this.groupBox1.Controls.Add(this.Cmbマウス);
			this.groupBox1.Controls.Add(this.Cmbキーボード);
			this.groupBox1.Location = new System.Drawing.Point(12, 29);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(248, 190);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "記録";
			// 
			// Btn記録停止
			// 
			this.Btn記録停止.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Btn記録停止.Location = new System.Drawing.Point(127, 94);
			this.Btn記録停止.Name = "Btn記録停止";
			this.Btn記録停止.Size = new System.Drawing.Size(115, 90);
			this.Btn記録停止.TabIndex = 3;
			this.Btn記録停止.Text = "停止";
			this.Btn記録停止.UseVisualStyleBackColor = true;
			this.Btn記録停止.Click += new System.EventHandler(this.Btn記録停止_Click);
			// 
			// Btn記録開始
			// 
			this.Btn記録開始.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Btn記録開始.Location = new System.Drawing.Point(6, 94);
			this.Btn記録開始.Name = "Btn記録開始";
			this.Btn記録開始.Size = new System.Drawing.Size(115, 90);
			this.Btn記録開始.TabIndex = 2;
			this.Btn記録開始.Text = "開始";
			this.Btn記録開始.UseVisualStyleBackColor = true;
			this.Btn記録開始.Click += new System.EventHandler(this.Btn記録開始_Click);
			// 
			// Cmbマウス
			// 
			this.Cmbマウス.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Cmbマウス.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmbマウス.FormattingEnabled = true;
			this.Cmbマウス.Items.AddRange(new object[] {
            "マウス操作を記録する",
            "マウスのボタン操作のみ記録する",
            "マウス操作を記録しない"});
			this.Cmbマウス.Location = new System.Drawing.Point(6, 60);
			this.Cmbマウス.Name = "Cmbマウス";
			this.Cmbマウス.Size = new System.Drawing.Size(236, 28);
			this.Cmbマウス.TabIndex = 1;
			// 
			// Cmbキーボード
			// 
			this.Cmbキーボード.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Cmbキーボード.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cmbキーボード.FormattingEnabled = true;
			this.Cmbキーボード.Items.AddRange(new object[] {
            "キー操作を記録する",
            "キー操作を記録しない"});
			this.Cmbキーボード.Location = new System.Drawing.Point(6, 26);
			this.Cmbキーボード.Name = "Cmbキーボード";
			this.Cmbキーボード.Size = new System.Drawing.Size(236, 28);
			this.Cmbキーボード.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.Btnもう一度再生する);
			this.groupBox2.Controls.Add(this.Btn再生中断);
			this.groupBox2.Controls.Add(this.Btn再生開始);
			this.groupBox2.Location = new System.Drawing.Point(12, 275);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(248, 172);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "再生";
			// 
			// Btnもう一度再生する
			// 
			this.Btnもう一度再生する.Location = new System.Drawing.Point(6, 122);
			this.Btnもう一度再生する.Name = "Btnもう一度再生する";
			this.Btnもう一度再生する.Size = new System.Drawing.Size(236, 44);
			this.Btnもう一度再生する.TabIndex = 2;
			this.Btnもう一度再生する.Text = "もう一度再生する";
			this.Btnもう一度再生する.UseVisualStyleBackColor = true;
			this.Btnもう一度再生する.Click += new System.EventHandler(this.Btnもう一度再生する_Click);
			// 
			// Btn再生中断
			// 
			this.Btn再生中断.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Btn再生中断.Location = new System.Drawing.Point(127, 26);
			this.Btn再生中断.Name = "Btn再生中断";
			this.Btn再生中断.Size = new System.Drawing.Size(115, 90);
			this.Btn再生中断.TabIndex = 1;
			this.Btn再生中断.Text = "中断";
			this.Btn再生中断.UseVisualStyleBackColor = true;
			this.Btn再生中断.Click += new System.EventHandler(this.Btn再生中断_Click);
			// 
			// Btn再生開始
			// 
			this.Btn再生開始.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Btn再生開始.Location = new System.Drawing.Point(6, 26);
			this.Btn再生開始.Name = "Btn再生開始";
			this.Btn再生開始.Size = new System.Drawing.Size(115, 90);
			this.Btn再生開始.TabIndex = 0;
			this.Btn再生開始.Text = "再生";
			this.Btn再生開始.UseVisualStyleBackColor = true;
			this.Btn再生開始.Click += new System.EventHandler(this.Btn再生開始_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SouthText});
			this.statusStrip1.Location = new System.Drawing.Point(0, 450);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(272, 23);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// SouthText
			// 
			this.SouthText.Name = "SouthText";
			this.SouthText.Size = new System.Drawing.Size(104, 18);
			this.SouthText.Text = "準備しています...";
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// BtnOpenOutDir
			// 
			this.BtnOpenOutDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOpenOutDir.Location = new System.Drawing.Point(18, 225);
			this.BtnOpenOutDir.Name = "BtnOpenOutDir";
			this.BtnOpenOutDir.Size = new System.Drawing.Size(236, 44);
			this.BtnOpenOutDir.TabIndex = 2;
			this.BtnOpenOutDir.Text = "保存先を開く";
			this.BtnOpenOutDir.UseVisualStyleBackColor = true;
			this.BtnOpenOutDir.Click += new System.EventHandler(this.BtnOpenOutDir_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(272, 473);
			this.Controls.Add(this.BtnOpenOutDir);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "MainWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WRec";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem アプリケーションAToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button Btn記録停止;
		private System.Windows.Forms.Button Btn記録開始;
		private System.Windows.Forms.ComboBox Cmbマウス;
		private System.Windows.Forms.ComboBox Cmbキーボード;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button Btn再生中断;
		private System.Windows.Forms.Button Btn再生開始;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel SouthText;
		private System.Windows.Forms.Button Btnもう一度再生する;
		private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 常に手前に表示するTToolStripMenuItem;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.Button BtnOpenOutDir;
		private System.Windows.Forms.ToolStripMenuItem その他の設定OToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 繰り返し再生するRToolStripMenuItem;
	}
}

