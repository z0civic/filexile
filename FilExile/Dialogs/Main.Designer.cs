namespace FilExile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainWindowMenu = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.label_target = new System.Windows.Forms.Label();
            this.field_target = new System.Windows.Forms.TextBox();
            this.button_browse = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.button_delete = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.optionsTab = new System.Windows.Forms.TabPage();
            this.button_logToBrowse = new System.Windows.Forms.Button();
            this.label_logTo = new System.Windows.Forms.Label();
            this.field_logTo = new System.Windows.Forms.TextBox();
            this.checkbox_forceAction = new System.Windows.Forms.CheckBox();
            this.comboBox_completionAction = new System.Windows.Forms.ComboBox();
            this.label_onCompletion = new System.Windows.Forms.Label();
            this.checkbox_autoUpdate = new System.Windows.Forms.CheckBox();
            this.checkbox_logging = new System.Windows.Forms.CheckBox();
            this.advancedTab = new System.Windows.Forms.TabPage();
            this.button_defaults = new System.Windows.Forms.Button();
            this.checkbox_alwaysOnTop = new System.Windows.Forms.CheckBox();
            this.label_threadBounds = new System.Windows.Forms.Label();
            this.spinner_threadCount = new System.Windows.Forms.NumericUpDown();
            this.label_threadCount = new System.Windows.Forms.Label();
            this.checkbox_multiThreading = new System.Windows.Forms.CheckBox();
            this.checkbox_output = new System.Windows.Forms.CheckBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.checkbox_disableProgressMonitoring = new System.Windows.Forms.CheckBox();
            this.mainWindowMenu.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.optionsTab.SuspendLayout();
            this.advancedTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinner_threadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // mainWindowMenu
            // 
            this.mainWindowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.mainWindowMenu.Location = new System.Drawing.Point(0, 0);
            this.mainWindowMenu.Name = "mainWindowMenu";
            this.mainWindowMenu.Size = new System.Drawing.Size(448, 24);
            this.mainWindowMenu.TabIndex = 0;
            this.mainWindowMenu.Text = "mainWindowMenu";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.onlineHelpToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.contentsToolStripMenuItem.Text = "Contents";
            // 
            // onlineHelpToolStripMenuItem
            // 
            this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
            this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.onlineHelpToolStripMenuItem.Text = "Online help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 256);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(448, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // label_target
            // 
            this.label_target.AutoSize = true;
            this.label_target.Location = new System.Drawing.Point(12, 42);
            this.label_target.Name = "label_target";
            this.label_target.Size = new System.Drawing.Size(41, 13);
            this.label_target.TabIndex = 2;
            this.label_target.Text = "Target:";
            // 
            // field_target
            // 
            this.field_target.Location = new System.Drawing.Point(58, 39);
            this.field_target.Name = "field_target";
            this.field_target.Size = new System.Drawing.Size(295, 20);
            this.field_target.TabIndex = 3;
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(359, 38);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(75, 22);
            this.button_browse.TabIndex = 4;
            this.button_browse.Text = "Browse...";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(58, 66);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(295, 23);
            this.progressBar.TabIndex = 5;
            this.progressBar.Visible = false;
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(359, 66);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(75, 22);
            this.button_delete.TabIndex = 6;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.optionsTab);
            this.tabControl.Controls.Add(this.advancedTab);
            this.tabControl.Location = new System.Drawing.Point(15, 94);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(419, 148);
            this.tabControl.TabIndex = 7;
            // 
            // optionsTab
            // 
            this.optionsTab.Controls.Add(this.button_logToBrowse);
            this.optionsTab.Controls.Add(this.label_logTo);
            this.optionsTab.Controls.Add(this.field_logTo);
            this.optionsTab.Controls.Add(this.checkbox_forceAction);
            this.optionsTab.Controls.Add(this.comboBox_completionAction);
            this.optionsTab.Controls.Add(this.label_onCompletion);
            this.optionsTab.Controls.Add(this.checkbox_autoUpdate);
            this.optionsTab.Controls.Add(this.checkbox_logging);
            this.optionsTab.Location = new System.Drawing.Point(4, 22);
            this.optionsTab.Name = "optionsTab";
            this.optionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.optionsTab.Size = new System.Drawing.Size(411, 122);
            this.optionsTab.TabIndex = 0;
            this.optionsTab.Text = "Options";
            this.optionsTab.UseVisualStyleBackColor = true;
            // 
            // button_logToBrowse
            // 
            this.button_logToBrowse.Enabled = false;
            this.button_logToBrowse.Location = new System.Drawing.Point(384, 28);
            this.button_logToBrowse.Name = "button_logToBrowse";
            this.button_logToBrowse.Size = new System.Drawing.Size(24, 22);
            this.button_logToBrowse.TabIndex = 8;
            this.button_logToBrowse.Text = "...";
            this.button_logToBrowse.UseVisualStyleBackColor = true;
            this.button_logToBrowse.Click += new System.EventHandler(this.button_logToBrowse_Click);
            // 
            // label_logTo
            // 
            this.label_logTo.AutoSize = true;
            this.label_logTo.Location = new System.Drawing.Point(40, 32);
            this.label_logTo.Name = "label_logTo";
            this.label_logTo.Size = new System.Drawing.Size(44, 13);
            this.label_logTo.TabIndex = 7;
            this.label_logTo.Text = "Log file:";
            // 
            // field_logTo
            // 
            this.field_logTo.Enabled = false;
            this.field_logTo.Location = new System.Drawing.Point(91, 29);
            this.field_logTo.Name = "field_logTo";
            this.field_logTo.Size = new System.Drawing.Size(290, 20);
            this.field_logTo.TabIndex = 6;
            // 
            // checkbox_forceAction
            // 
            this.checkbox_forceAction.AutoSize = true;
            this.checkbox_forceAction.Location = new System.Drawing.Point(218, 81);
            this.checkbox_forceAction.Name = "checkbox_forceAction";
            this.checkbox_forceAction.Size = new System.Drawing.Size(53, 17);
            this.checkbox_forceAction.TabIndex = 5;
            this.checkbox_forceAction.Text = "Force";
            this.checkbox_forceAction.UseVisualStyleBackColor = true;
            // 
            // comboBox_completionAction
            // 
            this.comboBox_completionAction.FormattingEnabled = true;
            this.comboBox_completionAction.Items.AddRange(new object[] {
            "Do nothing",
            "Play a sound",
            "Restart",
            "Shutdown"});
            this.comboBox_completionAction.Location = new System.Drawing.Point(91, 78);
            this.comboBox_completionAction.Name = "comboBox_completionAction";
            this.comboBox_completionAction.Size = new System.Drawing.Size(121, 21);
            this.comboBox_completionAction.TabIndex = 4;
            // 
            // label_onCompletion
            // 
            this.label_onCompletion.AutoSize = true;
            this.label_onCompletion.Location = new System.Drawing.Point(6, 81);
            this.label_onCompletion.Name = "label_onCompletion";
            this.label_onCompletion.Size = new System.Drawing.Size(78, 13);
            this.label_onCompletion.TabIndex = 3;
            this.label_onCompletion.Text = "On completion:";
            // 
            // checkbox_autoUpdate
            // 
            this.checkbox_autoUpdate.AutoSize = true;
            this.checkbox_autoUpdate.Location = new System.Drawing.Point(7, 55);
            this.checkbox_autoUpdate.Name = "checkbox_autoUpdate";
            this.checkbox_autoUpdate.Size = new System.Drawing.Size(163, 17);
            this.checkbox_autoUpdate.TabIndex = 1;
            this.checkbox_autoUpdate.Text = "Check for updates on startup";
            this.checkbox_autoUpdate.UseVisualStyleBackColor = true;
            // 
            // checkbox_logging
            // 
            this.checkbox_logging.AutoSize = true;
            this.checkbox_logging.Location = new System.Drawing.Point(7, 7);
            this.checkbox_logging.Name = "checkbox_logging";
            this.checkbox_logging.Size = new System.Drawing.Size(105, 17);
            this.checkbox_logging.TabIndex = 0;
            this.checkbox_logging.Text = "Log output to file";
            this.checkbox_logging.UseVisualStyleBackColor = true;
            this.checkbox_logging.CheckedChanged += new System.EventHandler(this.checkbox_logging_CheckedChanged);
            // 
            // advancedTab
            // 
            this.advancedTab.Controls.Add(this.checkbox_disableProgressMonitoring);
            this.advancedTab.Controls.Add(this.button_defaults);
            this.advancedTab.Controls.Add(this.checkbox_alwaysOnTop);
            this.advancedTab.Controls.Add(this.label_threadBounds);
            this.advancedTab.Controls.Add(this.spinner_threadCount);
            this.advancedTab.Controls.Add(this.label_threadCount);
            this.advancedTab.Controls.Add(this.checkbox_multiThreading);
            this.advancedTab.Controls.Add(this.checkbox_output);
            this.advancedTab.Location = new System.Drawing.Point(4, 22);
            this.advancedTab.Name = "advancedTab";
            this.advancedTab.Padding = new System.Windows.Forms.Padding(3);
            this.advancedTab.Size = new System.Drawing.Size(411, 122);
            this.advancedTab.TabIndex = 1;
            this.advancedTab.Text = "Advanced";
            this.advancedTab.UseVisualStyleBackColor = true;
            // 
            // button_defaults
            // 
            this.button_defaults.Location = new System.Drawing.Point(330, 7);
            this.button_defaults.Name = "button_defaults";
            this.button_defaults.Size = new System.Drawing.Size(75, 23);
            this.button_defaults.TabIndex = 6;
            this.button_defaults.Text = "Defaults";
            this.button_defaults.UseVisualStyleBackColor = true;
            this.button_defaults.Click += new System.EventHandler(this.button_defaults_Click);
            // 
            // checkbox_alwaysOnTop
            // 
            this.checkbox_alwaysOnTop.AutoSize = true;
            this.checkbox_alwaysOnTop.Location = new System.Drawing.Point(7, 77);
            this.checkbox_alwaysOnTop.Name = "checkbox_alwaysOnTop";
            this.checkbox_alwaysOnTop.Size = new System.Drawing.Size(92, 17);
            this.checkbox_alwaysOnTop.TabIndex = 5;
            this.checkbox_alwaysOnTop.Text = "Always on top";
            this.checkbox_alwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // label_threadBounds
            // 
            this.label_threadBounds.AutoSize = true;
            this.label_threadBounds.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label_threadBounds.Location = new System.Drawing.Point(145, 56);
            this.label_threadBounds.Name = "label_threadBounds";
            this.label_threadBounds.Size = new System.Drawing.Size(40, 13);
            this.label_threadBounds.TabIndex = 4;
            this.label_threadBounds.Text = "[1-128]";
            // 
            // spinner_threadCount
            // 
            this.spinner_threadCount.Location = new System.Drawing.Point(91, 53);
            this.spinner_threadCount.Name = "spinner_threadCount";
            this.spinner_threadCount.Size = new System.Drawing.Size(48, 20);
            this.spinner_threadCount.TabIndex = 3;
            // 
            // label_threadCount
            // 
            this.label_threadCount.AutoSize = true;
            this.label_threadCount.Location = new System.Drawing.Point(36, 55);
            this.label_threadCount.Name = "label_threadCount";
            this.label_threadCount.Size = new System.Drawing.Size(49, 13);
            this.label_threadCount.TabIndex = 2;
            this.label_threadCount.Text = "Threads:";
            // 
            // checkbox_multiThreading
            // 
            this.checkbox_multiThreading.AutoSize = true;
            this.checkbox_multiThreading.Location = new System.Drawing.Point(7, 30);
            this.checkbox_multiThreading.Name = "checkbox_multiThreading";
            this.checkbox_multiThreading.Size = new System.Drawing.Size(182, 17);
            this.checkbox_multiThreading.TabIndex = 1;
            this.checkbox_multiThreading.Text = "Enable Robocopy multi-threading";
            this.checkbox_multiThreading.UseVisualStyleBackColor = true;
            // 
            // checkbox_output
            // 
            this.checkbox_output.AutoSize = true;
            this.checkbox_output.Location = new System.Drawing.Point(7, 7);
            this.checkbox_output.Name = "checkbox_output";
            this.checkbox_output.Size = new System.Drawing.Size(199, 17);
            this.checkbox_output.TabIndex = 0;
            this.checkbox_output.Text = "Display verbose output while running";
            this.checkbox_output.UseVisualStyleBackColor = true;
            // 
            // checkbox_disableProgressMonitoring
            // 
            this.checkbox_disableProgressMonitoring.AutoSize = true;
            this.checkbox_disableProgressMonitoring.Location = new System.Drawing.Point(7, 99);
            this.checkbox_disableProgressMonitoring.Name = "checkbox_disableProgressMonitoring";
            this.checkbox_disableProgressMonitoring.Size = new System.Drawing.Size(155, 17);
            this.checkbox_disableProgressMonitoring.TabIndex = 7;
            this.checkbox_disableProgressMonitoring.Text = "Disable progress monitoring";
            this.checkbox_disableProgressMonitoring.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 278);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button_browse);
            this.Controls.Add(this.field_target);
            this.Controls.Add(this.label_target);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainWindowMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainWindowMenu;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "FilExile";
            this.Load += new System.EventHandler(this.Main_Load);
            this.mainWindowMenu.ResumeLayout(false);
            this.mainWindowMenu.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.optionsTab.ResumeLayout(false);
            this.optionsTab.PerformLayout();
            this.advancedTab.ResumeLayout(false);
            this.advancedTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinner_threadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainWindowMenu;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Label label_target;
        private System.Windows.Forms.TextBox field_target;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage optionsTab;
        private System.Windows.Forms.TabPage advancedTab;
        private System.Windows.Forms.CheckBox checkbox_logging;
        private System.Windows.Forms.ComboBox comboBox_completionAction;
        private System.Windows.Forms.Label label_onCompletion;
        private System.Windows.Forms.CheckBox checkbox_autoUpdate;
        private System.Windows.Forms.CheckBox checkbox_forceAction;
        private System.Windows.Forms.CheckBox checkbox_output;
        private System.Windows.Forms.CheckBox checkbox_multiThreading;
        private System.Windows.Forms.Label label_threadCount;
        private System.Windows.Forms.Label label_threadBounds;
        private System.Windows.Forms.NumericUpDown spinner_threadCount;
        private System.Windows.Forms.CheckBox checkbox_alwaysOnTop;
        private System.Windows.Forms.Label label_logTo;
        private System.Windows.Forms.TextBox field_logTo;
        private System.Windows.Forms.Button button_logToBrowse;
        private System.Windows.Forms.Button button_defaults;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox checkbox_disableProgressMonitoring;
    }
}

