namespace FilExile.Dialogs
{
	partial class SafetyDlg
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label_SafetyWarning = new System.Windows.Forms.Label();
			this.button_Yes = new System.Windows.Forms.Button();
			this.button_No = new System.Windows.Forms.Button();
			this.pictureBox_WarningIcon = new System.Windows.Forms.PictureBox();
			this.label_Continue = new System.Windows.Forms.Label();
			this.checkBox_DisableSafety = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_WarningIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// label_SafetyWarning
			// 
			this.label_SafetyWarning.AutoSize = true;
			this.label_SafetyWarning.Location = new System.Drawing.Point(66, 16);
			this.label_SafetyWarning.Name = "label_SafetyWarning";
			this.label_SafetyWarning.Size = new System.Drawing.Size(35, 13);
			this.label_SafetyWarning.TabIndex = 1;
			this.label_SafetyWarning.Text = "label1";
			// 
			// button_Yes
			// 
			this.button_Yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.button_Yes.Location = new System.Drawing.Point(230, 77);
			this.button_Yes.Name = "button_Yes";
			this.button_Yes.Size = new System.Drawing.Size(75, 23);
			this.button_Yes.TabIndex = 2;
			this.button_Yes.Text = "&Yes";
			this.button_Yes.UseVisualStyleBackColor = true;
			this.button_Yes.Click += new System.EventHandler(this.button_Yes_Click);
			// 
			// button_No
			// 
			this.button_No.DialogResult = System.Windows.Forms.DialogResult.No;
			this.button_No.Location = new System.Drawing.Point(311, 77);
			this.button_No.Name = "button_No";
			this.button_No.Size = new System.Drawing.Size(75, 23);
			this.button_No.TabIndex = 3;
			this.button_No.Text = "&No";
			this.button_No.UseVisualStyleBackColor = true;
			this.button_No.Click += new System.EventHandler(this.button_No_Click);
			// 
			// pictureBox_WarningIcon
			// 
			this.pictureBox_WarningIcon.Location = new System.Drawing.Point(12, 12);
			this.pictureBox_WarningIcon.Name = "pictureBox_WarningIcon";
			this.pictureBox_WarningIcon.Size = new System.Drawing.Size(48, 48);
			this.pictureBox_WarningIcon.TabIndex = 4;
			this.pictureBox_WarningIcon.TabStop = false;
			// 
			// label_Continue
			// 
			this.label_Continue.AutoSize = true;
			this.label_Continue.Location = new System.Drawing.Point(66, 43);
			this.label_Continue.Name = "label_Continue";
			this.label_Continue.Size = new System.Drawing.Size(35, 13);
			this.label_Continue.TabIndex = 5;
			this.label_Continue.Text = "label1";
			this.label_Continue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkBox_DisableSafety
			// 
			this.checkBox_DisableSafety.AutoSize = true;
			this.checkBox_DisableSafety.Checked = global::FilExile.Properties.Settings.Default.disableSafety;
			this.checkBox_DisableSafety.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FilExile.Properties.Settings.Default, "disableSafety", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBox_DisableSafety.Location = new System.Drawing.Point(12, 81);
			this.checkBox_DisableSafety.Name = "checkBox_DisableSafety";
			this.checkBox_DisableSafety.Size = new System.Drawing.Size(136, 17);
			this.checkBox_DisableSafety.TabIndex = 0;
			this.checkBox_DisableSafety.Text = "&Disable future warnings";
			this.checkBox_DisableSafety.UseVisualStyleBackColor = true;
			// 
			// SafetyDlg
			// 
			this.AcceptButton = this.button_No;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(398, 110);
			this.ControlBox = false;
			this.Controls.Add(this.label_Continue);
			this.Controls.Add(this.pictureBox_WarningIcon);
			this.Controls.Add(this.button_No);
			this.Controls.Add(this.button_Yes);
			this.Controls.Add(this.label_SafetyWarning);
			this.Controls.Add(this.checkBox_DisableSafety);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SafetyDlg";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "SafetyDlg";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SafetyDlg_Closing);
			this.Load += new System.EventHandler(this.SafetyDlg_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_WarningIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBox_DisableSafety;
		private System.Windows.Forms.Label label_SafetyWarning;
		private System.Windows.Forms.Button button_Yes;
		private System.Windows.Forms.Button button_No;
		private System.Windows.Forms.PictureBox pictureBox_WarningIcon;
		private System.Windows.Forms.Label label_Continue;
	}
}