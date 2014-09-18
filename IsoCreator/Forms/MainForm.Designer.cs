namespace IsoCreator.Forms {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing ) {
			if ( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.textBoxVolumeName = new System.Windows.Forms.TextBox();
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            this.buttonBrowseIso = new System.Windows.Forms.Button();
            this.buttonStartAbort = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxIsoPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ISO File Path";
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolder.Location = new System.Drawing.Point(9, 21);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.ReadOnly = true;
            this.textBoxFolder.Size = new System.Drawing.Size(266, 20);
            this.textBoxFolder.TabIndex = 2;
            // 
            // textBoxVolumeName
            // 
            this.textBoxVolumeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVolumeName.Location = new System.Drawing.Point(94, 47);
            this.textBoxVolumeName.Name = "textBoxVolumeName";
            this.textBoxVolumeName.Size = new System.Drawing.Size(262, 20);
            this.textBoxVolumeName.TabIndex = 3;
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseFolder.Location = new System.Drawing.Point(281, 19);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseFolder.TabIndex = 4;
            this.buttonBrowseFolder.Text = "Browse...";
            this.buttonBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // buttonBrowseIso
            // 
            this.buttonBrowseIso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseIso.Location = new System.Drawing.Point(281, 19);
            this.buttonBrowseIso.Name = "buttonBrowseIso";
            this.buttonBrowseIso.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseIso.TabIndex = 5;
            this.buttonBrowseIso.Text = "Browse...";
            this.buttonBrowseIso.UseVisualStyleBackColor = true;
            this.buttonBrowseIso.Click += new System.EventHandler(this.buttonBrowseIso_Click);
            // 
            // buttonStartAbort
            // 
            this.buttonStartAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartAbort.Location = new System.Drawing.Point(301, 217);
            this.buttonStartAbort.Name = "buttonStartAbort";
            this.buttonStartAbort.Size = new System.Drawing.Size(75, 23);
            this.buttonStartAbort.TabIndex = 6;
            this.buttonStartAbort.Text = "Start";
            this.buttonStartAbort.UseVisualStyleBackColor = true;
            this.buttonStartAbort.Click += new System.EventHandler(this.buttonStartAbort_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Volume Name";
            // 
            // textBoxIsoPath
            // 
            this.textBoxIsoPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIsoPath.Location = new System.Drawing.Point(94, 21);
            this.textBoxIsoPath.Name = "textBoxIsoPath";
            this.textBoxIsoPath.ReadOnly = true;
            this.textBoxIsoPath.Size = new System.Drawing.Size(181, 20);
            this.textBoxIsoPath.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBoxIsoPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonBrowseIso);
            this.groupBox1.Controls.Add(this.textBoxVolumeName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 76);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ISO Details";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonBrowseFolder);
            this.groupBox3.Controls.Add(this.textBoxFolder);
            this.groupBox3.Location = new System.Drawing.Point(12, 94);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(362, 52);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Folder Path";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(9, 32);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(347, 18);
            this.progressBar.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelStatus);
            this.groupBox2.Controls.Add(this.progressBar);
            this.groupBox2.Location = new System.Drawing.Point(12, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 59);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Progress";
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.labelStatus.Location = new System.Drawing.Point(261, 16);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelStatus.Size = new System.Drawing.Size(98, 13);
            this.labelStatus.TabIndex = 15;
            this.labelStatus.Text = "Process not started";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(388, 248);
            this.Controls.Add(this.buttonStartAbort);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(4067, 287);
            this.MinimumSize = new System.Drawing.Size(404, 287);
            this.Name = "MainForm";
            this.Text = "ISO Creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.TextBox textBoxVolumeName;
		private System.Windows.Forms.Button buttonBrowseFolder;
		private System.Windows.Forms.Button buttonBrowseIso;
		private System.Windows.Forms.Button buttonStartAbort;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxIsoPath;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label labelStatus;
	}
}

