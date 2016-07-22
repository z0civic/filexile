namespace FilExile.Dialogs
{
	partial class RobocopyDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RobocopyDlg));
			this.lblRobocopyError = new System.Windows.Forms.Label();
			this.pb_ErrorIcon = new System.Windows.Forms.PictureBox();
			this.btn_Ignore = new System.Windows.Forms.Button();
			this.lnklbl_downloadLink = new System.Windows.Forms.LinkLabel();
			this.btn_Close = new System.Windows.Forms.Button();
			this.chk_AlwaysIgnore = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pb_ErrorIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// lblRobocopyError
			// 
			this.lblRobocopyError.Location = new System.Drawing.Point(66, 12);
			this.lblRobocopyError.Name = "lblRobocopyError";
			this.lblRobocopyError.Size = new System.Drawing.Size(416, 122);
			this.lblRobocopyError.TabIndex = 0;
			this.lblRobocopyError.Text = resources.GetString("lblRobocopyError.Text");
			// 
			// pb_ErrorIcon
			// 
			this.pb_ErrorIcon.Location = new System.Drawing.Point(12, 12);
			this.pb_ErrorIcon.Name = "pb_ErrorIcon";
			this.pb_ErrorIcon.Size = new System.Drawing.Size(48, 48);
			this.pb_ErrorIcon.TabIndex = 1;
			this.pb_ErrorIcon.TabStop = false;
			// 
			// btn_Ignore
			// 
			this.btn_Ignore.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Ignore.Location = new System.Drawing.Point(319, 148);
			this.btn_Ignore.Name = "btn_Ignore";
			this.btn_Ignore.Size = new System.Drawing.Size(75, 24);
			this.btn_Ignore.TabIndex = 2;
			this.btn_Ignore.Text = "&Ignore";
			this.btn_Ignore.UseVisualStyleBackColor = true;
			this.btn_Ignore.Click += new System.EventHandler(this.btn_Ignore_Click);
			// 
			// lnklbl_downloadLink
			// 
			this.lnklbl_downloadLink.AutoSize = true;
			this.lnklbl_downloadLink.Location = new System.Drawing.Point(67, 121);
			this.lnklbl_downloadLink.Name = "lnklbl_downloadLink";
			this.lnklbl_downloadLink.Size = new System.Drawing.Size(327, 13);
			this.lnklbl_downloadLink.TabIndex = 3;
			this.lnklbl_downloadLink.TabStop = true;
			this.lnklbl_downloadLink.Text = "https://www.microsoft.com/en-us/download/details.aspx?id=17657";
			this.lnklbl_downloadLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklbl_downloadLink_LinkClicked);
			// 
			// btn_Close
			// 
			this.btn_Close.Location = new System.Drawing.Point(400, 148);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Size = new System.Drawing.Size(75, 24);
			this.btn_Close.TabIndex = 4;
			this.btn_Close.Text = "&Close";
			this.btn_Close.UseVisualStyleBackColor = true;
			this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// RobocopyDlg
			// 
			this.AcceptButton = this.btn_Close;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Close;
			this.ClientSize = new System.Drawing.Size(487, 184);
			this.ControlBox = false;
			this.Controls.Add(this.chk_AlwaysIgnore);
			this.Controls.Add(this.btn_Close);
			this.Controls.Add(this.lnklbl_downloadLink);
			this.Controls.Add(this.btn_Ignore);
			this.Controls.Add(this.pb_ErrorIcon);
			this.Controls.Add(this.lblRobocopyError);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RobocopyDlg";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Error";
			this.Load += new System.EventHandler(this.RobocopyDlg_Load);
			((System.ComponentModel.ISupportInitialize)(this.pb_ErrorIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblRobocopyError;
		private System.Windows.Forms.PictureBox pb_ErrorIcon;
		private System.Windows.Forms.Button btn_Ignore;
		private System.Windows.Forms.LinkLabel lnklbl_downloadLink;
		private System.Windows.Forms.Button btn_Close;
		private System.Windows.Forms.CheckBox chk_AlwaysIgnore;
	}
}