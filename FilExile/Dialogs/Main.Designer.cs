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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.mainWindowMenu = new System.Windows.Forms.MenuStrip();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.label_target = new System.Windows.Forms.Label();
			this.field_target = new System.Windows.Forms.TextBox();
			this.button_browse = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.button_delete = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.optionsTab = new System.Windows.Forms.TabPage();
			this.checkbox_logging = new System.Windows.Forms.CheckBox();
			this.panel_LogFile = new System.Windows.Forms.Panel();
			this.button_logToBrowse = new System.Windows.Forms.Button();
			this.label_logTo = new System.Windows.Forms.Label();
			this.field_logTo = new System.Windows.Forms.TextBox();
			this.checkbox_forceAction = new System.Windows.Forms.CheckBox();
			this.comboBox_completionAction = new System.Windows.Forms.ComboBox();
			this.completionActionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label_onCompletion = new System.Windows.Forms.Label();
			this.checkbox_autoUpdate = new System.Windows.Forms.CheckBox();
			this.advancedTab = new System.Windows.Forms.TabPage();
			this.panel_Multithreading = new System.Windows.Forms.Panel();
			this.label_threadBounds = new System.Windows.Forms.Label();
			this.spinner_threadCount = new System.Windows.Forms.NumericUpDown();
			this.label_threadCount = new System.Windows.Forms.Label();
			this.checkbox_disableProgressMonitoring = new System.Windows.Forms.CheckBox();
			this.button_defaults = new System.Windows.Forms.Button();
			this.checkbox_alwaysOnTop = new System.Windows.Forms.CheckBox();
			this.checkbox_multiThreading = new System.Windows.Forms.CheckBox();
			this.checkbox_output = new System.Windows.Forms.CheckBox();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.backgroundWorker_ProgressBar = new System.ComponentModel.BackgroundWorker();
			this.backgroundWorker_Deletion = new System.ComponentModel.BackgroundWorker();
			this.mainWindowMenu.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.optionsTab.SuspendLayout();
			this.panel_LogFile.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.completionActionsBindingSource)).BeginInit();
			this.advancedTab.SuspendLayout();
			this.panel_Multithreading.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spinner_threadCount)).BeginInit();
			this.SuspendLayout();
			// 
			// mainWindowMenu
			// 
			this.mainWindowMenu.BackColor = System.Drawing.SystemColors.Control;
			this.mainWindowMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.mainWindowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
			resources.ApplyResources(this.mainWindowMenu, "mainWindowMenu");
			this.mainWindowMenu.Name = "mainWindowMenu";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.onlineHelpToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
			// 
			// contentsToolStripMenuItem
			// 
			this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
			resources.ApplyResources(this.contentsToolStripMenuItem, "contentsToolStripMenuItem");
			this.contentsToolStripMenuItem.Click += new System.EventHandler(this.contentsToolStripMenuItem_Click);
			// 
			// onlineHelpToolStripMenuItem
			// 
			this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
			resources.ApplyResources(this.onlineHelpToolStripMenuItem, "onlineHelpToolStripMenuItem");
			this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.onlineHelpToolStripMenuItem_Click);
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			resources.ApplyResources(this.checkForUpdatesToolStripMenuItem, "checkForUpdatesToolStripMenuItem");
			this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel});
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.SizingGrip = false;
			// 
			// toolStripLabel
			// 
			this.toolStripLabel.Name = "toolStripLabel";
			resources.ApplyResources(this.toolStripLabel, "toolStripLabel");
			// 
			// label_target
			// 
			resources.ApplyResources(this.label_target, "label_target");
			this.label_target.Name = "label_target";
			// 
			// field_target
			// 
			resources.ApplyResources(this.field_target, "field_target");
			this.field_target.Name = "field_target";
			this.field_target.TextChanged += new System.EventHandler(this.field_target_TextChanged);
			// 
			// button_browse
			// 
			resources.ApplyResources(this.button_browse, "button_browse");
			this.button_browse.Name = "button_browse";
			this.button_browse.UseVisualStyleBackColor = true;
			this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
			// 
			// progressBar
			// 
			resources.ApplyResources(this.progressBar, "progressBar");
			this.progressBar.Name = "progressBar";
			// 
			// button_delete
			// 
			resources.ApplyResources(this.button_delete, "button_delete");
			this.button_delete.Name = "button_delete";
			this.button_delete.UseVisualStyleBackColor = true;
			this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.optionsTab);
			this.tabControl.Controls.Add(this.advancedTab);
			resources.ApplyResources(this.tabControl, "tabControl");
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			// 
			// optionsTab
			// 
			this.optionsTab.Controls.Add(this.checkbox_logging);
			this.optionsTab.Controls.Add(this.panel_LogFile);
			this.optionsTab.Controls.Add(this.checkbox_forceAction);
			this.optionsTab.Controls.Add(this.comboBox_completionAction);
			this.optionsTab.Controls.Add(this.label_onCompletion);
			this.optionsTab.Controls.Add(this.checkbox_autoUpdate);
			resources.ApplyResources(this.optionsTab, "optionsTab");
			this.optionsTab.Name = "optionsTab";
			this.optionsTab.UseVisualStyleBackColor = true;
			// 
			// checkbox_logging
			// 
			resources.ApplyResources(this.checkbox_logging, "checkbox_logging");
			this.checkbox_logging.Checked = global::FilExile.Properties.Settings.Default.logging;
			this.checkbox_logging.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FilExile.Properties.Settings.Default, "logging", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkbox_logging.Name = "checkbox_logging";
			this.checkbox_logging.UseVisualStyleBackColor = true;
			this.checkbox_logging.CheckedChanged += new System.EventHandler(this.checkbox_logging_CheckedChanged);
			// 
			// panel_LogFile
			// 
			this.panel_LogFile.Controls.Add(this.button_logToBrowse);
			this.panel_LogFile.Controls.Add(this.label_logTo);
			this.panel_LogFile.Controls.Add(this.field_logTo);
			resources.ApplyResources(this.panel_LogFile, "panel_LogFile");
			this.panel_LogFile.Name = "panel_LogFile";
			// 
			// button_logToBrowse
			// 
			resources.ApplyResources(this.button_logToBrowse, "button_logToBrowse");
			this.button_logToBrowse.Name = "button_logToBrowse";
			this.button_logToBrowse.UseVisualStyleBackColor = true;
			this.button_logToBrowse.Click += new System.EventHandler(this.button_logToBrowse_Click);
			// 
			// label_logTo
			// 
			resources.ApplyResources(this.label_logTo, "label_logTo");
			this.label_logTo.Name = "label_logTo";
			// 
			// field_logTo
			// 
			this.field_logTo.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FilExile.Properties.Settings.Default, "logTo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			resources.ApplyResources(this.field_logTo, "field_logTo");
			this.field_logTo.Name = "field_logTo";
			this.field_logTo.Text = global::FilExile.Properties.Settings.Default.logTo;
			// 
			// checkbox_forceAction
			// 
			resources.ApplyResources(this.checkbox_forceAction, "checkbox_forceAction");
			this.checkbox_forceAction.Checked = global::FilExile.Properties.Settings.Default.forceAction;
			this.checkbox_forceAction.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FilExile.Properties.Settings.Default, "forceAction", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkbox_forceAction.Name = "checkbox_forceAction";
			this.checkbox_forceAction.UseVisualStyleBackColor = true;
			// 
			// comboBox_completionAction
			// 
			this.comboBox_completionAction.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.completionActionsBindingSource, "Name", true));
			this.comboBox_completionAction.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.completionActionsBindingSource, "Value", true));
			this.comboBox_completionAction.DataSource = this.completionActionsBindingSource;
			this.comboBox_completionAction.FormattingEnabled = true;
			resources.ApplyResources(this.comboBox_completionAction, "comboBox_completionAction");
			this.comboBox_completionAction.Name = "comboBox_completionAction";
			this.comboBox_completionAction.SelectedIndexChanged += new System.EventHandler(this.comboBox_completionAction_SelectedIndexChanged);
			// 
			// completionActionsBindingSource
			// 
			this.completionActionsBindingSource.DataSource = typeof(Shared.CompletionAction);
			// 
			// label_onCompletion
			// 
			resources.ApplyResources(this.label_onCompletion, "label_onCompletion");
			this.label_onCompletion.Name = "label_onCompletion";
			// 
			// checkbox_autoUpdate
			// 
			resources.ApplyResources(this.checkbox_autoUpdate, "checkbox_autoUpdate");
			this.checkbox_autoUpdate.Checked = global::FilExile.Properties.Settings.Default.autoUpdate;
			this.checkbox_autoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkbox_autoUpdate.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FilExile.Properties.Settings.Default, "autoUpdate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkbox_autoUpdate.Name = "checkbox_autoUpdate";
			this.checkbox_autoUpdate.UseVisualStyleBackColor = true;
			// 
			// advancedTab
			// 
			this.advancedTab.Controls.Add(this.panel_Multithreading);
			this.advancedTab.Controls.Add(this.checkbox_disableProgressMonitoring);
			this.advancedTab.Controls.Add(this.button_defaults);
			this.advancedTab.Controls.Add(this.checkbox_alwaysOnTop);
			this.advancedTab.Controls.Add(this.checkbox_multiThreading);
			this.advancedTab.Controls.Add(this.checkbox_output);
			resources.ApplyResources(this.advancedTab, "advancedTab");
			this.advancedTab.Name = "advancedTab";
			this.advancedTab.UseVisualStyleBackColor = true;
			// 
			// panel_Multithreading
			// 
			this.panel_Multithreading.Controls.Add(this.label_threadBounds);
			this.panel_Multithreading.Controls.Add(this.spinner_threadCount);
			this.panel_Multithreading.Controls.Add(this.label_threadCount);
			resources.ApplyResources(this.panel_Multithreading, "panel_Multithreading");
			this.panel_Multithreading.Name = "panel_Multithreading";
			// 
			// label_threadBounds
			// 
			resources.ApplyResources(this.label_threadBounds, "label_threadBounds");
			this.label_threadBounds.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label_threadBounds.Name = "label_threadBounds";
			// 
			// spinner_threadCount
			// 
			this.spinner_threadCount.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::FilExile.Properties.Settings.Default, "threadCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			resources.ApplyResources(this.spinner_threadCount, "spinner_threadCount");
			this.spinner_threadCount.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.spinner_threadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spinner_threadCount.Name = "spinner_threadCount";
			this.spinner_threadCount.Value = global::FilExile.Properties.Settings.Default.threadCount;
			// 
			// label_threadCount
			// 
			resources.ApplyResources(this.label_threadCount, "label_threadCount");
			this.label_threadCount.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label_threadCount.Name = "label_threadCount";
			// 
			// checkbox_disableProgressMonitoring
			// 
			resources.ApplyResources(this.checkbox_disableProgressMonitoring, "checkbox_disableProgressMonitoring");
			this.checkbox_disableProgressMonitoring.Checked = global::FilExile.Properties.Settings.Default.disableProgressMonitoring;
			this.checkbox_disableProgressMonitoring.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FilExile.Properties.Settings.Default, "disableProgressMonitoring", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkbox_disableProgressMonitoring.Name = "checkbox_disableProgressMonitoring";
			this.checkbox_disableProgressMonitoring.UseVisualStyleBackColor = true;
			// 
			// button_defaults
			// 
			resources.ApplyResources(this.button_defaults, "button_defaults");
			this.button_defaults.Name = "button_defaults";
			this.button_defaults.UseVisualStyleBackColor = true;
			this.button_defaults.Click += new System.EventHandler(this.button_defaults_Click);
			// 
			// checkbox_alwaysOnTop
			// 
			resources.ApplyResources(this.checkbox_alwaysOnTop, "checkbox_alwaysOnTop");
			this.checkbox_alwaysOnTop.Checked = global::FilExile.Properties.Settings.Default.alwaysOnTop;
			this.checkbox_alwaysOnTop.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FilExile.Properties.Settings.Default, "alwaysOnTop", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkbox_alwaysOnTop.Name = "checkbox_alwaysOnTop";
			this.checkbox_alwaysOnTop.UseVisualStyleBackColor = true;
			this.checkbox_alwaysOnTop.CheckedChanged += new System.EventHandler(this.checkbox_alwaysOnTop_CheckedChanged);
			// 
			// checkbox_multiThreading
			// 
			resources.ApplyResources(this.checkbox_multiThreading, "checkbox_multiThreading");
			this.checkbox_multiThreading.Checked = global::FilExile.Properties.Settings.Default.multiThreading;
			this.checkbox_multiThreading.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkbox_multiThreading.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FilExile.Properties.Settings.Default, "multiThreading", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkbox_multiThreading.Name = "checkbox_multiThreading";
			this.checkbox_multiThreading.UseVisualStyleBackColor = true;
			this.checkbox_multiThreading.CheckedChanged += new System.EventHandler(this.checkbox_multiThreading_CheckedChanged);
			// 
			// checkbox_output
			// 
			resources.ApplyResources(this.checkbox_output, "checkbox_output");
			this.checkbox_output.Checked = global::FilExile.Properties.Settings.Default.showOutput;
			this.checkbox_output.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FilExile.Properties.Settings.Default, "showOutput", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkbox_output.Name = "checkbox_output";
			this.checkbox_output.UseVisualStyleBackColor = true;
			// 
			// backgroundWorker_ProgressBar
			// 
			this.backgroundWorker_ProgressBar.WorkerReportsProgress = true;
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
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.button_delete);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.button_browse);
			this.Controls.Add(this.field_target);
			this.Controls.Add(this.label_target);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.mainWindowMenu);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.mainWindowMenu;
			this.MaximizeBox = false;
			this.Name = "Main";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Closing);
			this.Load += new System.EventHandler(this.Main_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.Main_DragOver);
			this.mainWindowMenu.ResumeLayout(false);
			this.mainWindowMenu.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.optionsTab.ResumeLayout(false);
			this.optionsTab.PerformLayout();
			this.panel_LogFile.ResumeLayout(false);
			this.panel_LogFile.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.completionActionsBindingSource)).EndInit();
			this.advancedTab.ResumeLayout(false);
			this.advancedTab.PerformLayout();
			this.panel_Multithreading.ResumeLayout(false);
			this.panel_Multithreading.PerformLayout();
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
        private System.Windows.Forms.ComboBox comboBox_completionAction;
        private System.Windows.Forms.Label label_onCompletion;
        private System.Windows.Forms.CheckBox checkbox_autoUpdate;
        private System.Windows.Forms.CheckBox checkbox_forceAction;
        private System.Windows.Forms.CheckBox checkbox_output;
        private System.Windows.Forms.CheckBox checkbox_multiThreading;
        private System.Windows.Forms.CheckBox checkbox_alwaysOnTop;
        private System.Windows.Forms.Button button_defaults;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox checkbox_disableProgressMonitoring;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker_ProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Deletion;
		private System.Windows.Forms.BindingSource completionActionsBindingSource;
		private System.Windows.Forms.Panel panel_LogFile;
		private System.Windows.Forms.CheckBox checkbox_logging;
		private System.Windows.Forms.Button button_logToBrowse;
		private System.Windows.Forms.Label label_logTo;
		private System.Windows.Forms.TextBox field_logTo;
		private System.Windows.Forms.Panel panel_Multithreading;
		private System.Windows.Forms.Label label_threadBounds;
		private System.Windows.Forms.NumericUpDown spinner_threadCount;
		private System.Windows.Forms.Label label_threadCount;
	}
}

