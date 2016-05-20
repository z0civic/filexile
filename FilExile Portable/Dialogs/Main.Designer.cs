namespace FilExile.Dialogs
{
    partial class Main
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
			this.button_delete = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.button_browse = new System.Windows.Forms.Button();
			this.field_target = new System.Windows.Forms.TextBox();
			this.label_target = new System.Windows.Forms.Label();
			this.mainWindowMenu = new System.Windows.Forms.MenuStrip();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorker_ProgressBar = new System.ComponentModel.BackgroundWorker();
			this.backgroundWorker_Deletion = new System.ComponentModel.BackgroundWorker();
			this.mainWindowMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_delete
			// 
			this.button_delete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.button_delete.Location = new System.Drawing.Point(359, 66);
			this.button_delete.Name = "button_delete";
			this.button_delete.Size = new System.Drawing.Size(75, 22);
			this.button_delete.TabIndex = 11;
			this.button_delete.Text = "Delete";
			this.button_delete.UseVisualStyleBackColor = true;
			this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
			// 
			// progressBar
			// 
			this.progressBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.progressBar.Location = new System.Drawing.Point(58, 66);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(295, 23);
			this.progressBar.TabIndex = 10;
			this.progressBar.Visible = false;
			// 
			// button_browse
			// 
			this.button_browse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.button_browse.Location = new System.Drawing.Point(359, 38);
			this.button_browse.Name = "button_browse";
			this.button_browse.Size = new System.Drawing.Size(75, 22);
			this.button_browse.TabIndex = 9;
			this.button_browse.Text = "Browse...";
			this.button_browse.UseVisualStyleBackColor = true;
			this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
			// 
			// field_target
			// 
			this.field_target.Location = new System.Drawing.Point(58, 39);
			this.field_target.Name = "field_target";
			this.field_target.Size = new System.Drawing.Size(295, 20);
			this.field_target.TabIndex = 8;
			this.field_target.TextChanged += new System.EventHandler(this.field_target_TextChanged);
			// 
			// label_target
			// 
			this.label_target.AutoSize = true;
			this.label_target.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label_target.Location = new System.Drawing.Point(12, 42);
			this.label_target.Name = "label_target";
			this.label_target.Size = new System.Drawing.Size(41, 13);
			this.label_target.TabIndex = 7;
			this.label_target.Text = "Target:";
			// 
			// mainWindowMenu
			// 
			this.mainWindowMenu.BackColor = System.Drawing.SystemColors.Control;
			this.mainWindowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
			this.mainWindowMenu.Location = new System.Drawing.Point(0, 0);
			this.mainWindowMenu.Name = "mainWindowMenu";
			this.mainWindowMenu.Size = new System.Drawing.Size(448, 24);
			this.mainWindowMenu.TabIndex = 12;
			this.mainWindowMenu.Text = "menuStrip1";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineHelpToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// onlineHelpToolStripMenuItem
			// 
			this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
			this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.onlineHelpToolStripMenuItem.Text = "Online help";
			this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.onlineHelpToolStripMenuItem_Click);
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
			this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// backgroundWorker_ProgressBar
			// 
			this.backgroundWorker_ProgressBar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_ProgressBar_DoWork);
			this.backgroundWorker_ProgressBar.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressBar_ProgressChanged);
			// 
			// backgroundWorker_Deletion
			// 
			this.backgroundWorker_Deletion.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Deletion_DoWork);
			this.backgroundWorker_Deletion.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_Deletion_RunWorkerCompleted);
			// 
			// Main
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(448, 103);
			this.Controls.Add(this.button_delete);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.button_browse);
			this.Controls.Add(this.field_target);
			this.Controls.Add(this.label_target);
			this.Controls.Add(this.mainWindowMenu);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.mainWindowMenu;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Main";
			this.Text = "FilExile (Portable Edition)";
			this.Load += new System.EventHandler(this.Main_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.Main_DragOver);
			this.mainWindowMenu.ResumeLayout(false);
			this.mainWindowMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.TextBox field_target;
        private System.Windows.Forms.Label label_target;
        private System.Windows.Forms.MenuStrip mainWindowMenu;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker backgroundWorker_ProgressBar;
		private System.ComponentModel.BackgroundWorker backgroundWorker_Deletion;
	}
}

