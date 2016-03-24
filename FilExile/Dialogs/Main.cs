using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace FilExile.Dialogs
{
    public partial class Main : Form
    {
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public Main()
        {
            InitializeComponent();

            // If specified in the app.config file, perform an update check
            if (Properties.Settings.Default.autoUpdate)
                NetworkUtils.InitiateVersionCheck(false);
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Objects

        //The target that is going to be deleted
        private Target target;

        #endregion

        // ------------------------------------------------------------------------------------

        #region Fields

        // Holds the initial number of files in the target directory when tracking progress
        private int iNumFiles;

		#endregion

		// ------------------------------------------------------------------------------------

		#region Properties

		/// <summary>
		/// If FilExile should check for updates on startup
		/// </summary>
		public bool AutoUpdate
		{
			get { return checkbox_autoUpdate.Checked; }
			set { checkbox_autoUpdate.Checked = value; }
		}

		/// <summary>
		/// If verbose output should be displayed in the console
		/// </summary>
		public bool ShowOutput
		{
			get { return checkbox_output.Checked; }
			set { checkbox_output.Checked = value; }
		}

		/// <summary>
		/// If logging to a file is enabled
		/// </summary>
		public bool LoggingEnabled
		{
			get { return checkbox_logging.Checked; }
			set { panel_LogFile.Enabled = value; }
		}

		/// <summary>
		/// File (including path) to log to
		/// </summary>
		public string LogTo
		{
			get { return field_logTo.Text; }
			set { field_logTo.Text = value; }
		}

		/// <summary>
		/// If multithreaded Robocopy operations are enabled
		/// </summary>
		public bool MultiThreadingEnabled
		{
			get { return checkbox_multiThreading.Checked; }
			set	{ panel_Multithreading.Enabled = value;	}
		}

		public decimal ThreadCount
		{
			get { return spinner_threadCount.Value; }
			set { spinner_threadCount.Value = value; }
		}

		/// <summary>
		/// If FilExile should remain on top of other applications
		/// </summary>
		public bool AlwaysOnTop
		{
			get { return checkbox_alwaysOnTop.Checked; }
			set { checkbox_alwaysOnTop.Checked = value; }
		}

		/// <summary>
		/// If progress monitoring should be disabled (enhances performance)
		/// </summary>
		public bool DisableProgressMonitoring
		{
			get { return checkbox_disableProgressMonitoring.Checked; }
			set { checkbox_disableProgressMonitoring.Checked = value; }
		}

		/// <summary>
		/// String value of the target to be deleted
		/// </summary>
		public string Target
		{
			get { return field_target.Text; }
			set { field_target.Text = value; }
		}

		/// <summary>
		/// Tooltip displayed in the bottom-left corner
		/// </summary>
		public string Tooltip
		{
			get { return toolStripLabel.Text; }
			set { toolStripLabel.Text = value; }
		}

		#endregion

		// ------------------------------------------------------------------------------------

		#region Private methods

		/// <summary>
		/// Displays a warning message when a user attempts to delete a critical directory
		/// </summary>
		/// <returns>If the user wants to continue</returns>
		private bool CriticalTargetWarning()
		{
			SafetyDlg dlg = new SafetyDlg();
			dlg.ShowDialog();
			if (dlg.DialogResult == DialogResult.Yes)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Associates the completion aciton combobox with databinding
		/// </summary>
		private void InitiateComboBox()
		{
			var dataSource = new List<CompletionAction>();
			dataSource.Add(new CompletionAction() { Name = SharedResources.Properties.Resources.DoNothing, Value = 0 });
			dataSource.Add(new CompletionAction() { Name = SharedResources.Properties.Resources.PlaySound, Value = 1 });
			dataSource.Add(new CompletionAction() { Name = SharedResources.Properties.Resources.Reboot, Value = 2 });
			dataSource.Add(new CompletionAction() { Name = SharedResources.Properties.Resources.Shutdown, Value = 3 });

			comboBox_completionAction.DataSource = dataSource;
			comboBox_completionAction.DisplayMember = "Name";
			comboBox_completionAction.ValueMember = "Value";

			comboBox_completionAction.DropDownStyle = ComboBoxStyle.DropDownList;
		}

        /// <summary>
        /// Continuously checks the number of files in the target directory to
        /// display progress as they are deleted.
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        private void ProgressBarOperation(BackgroundWorker worker, DoWorkEventArgs e)
        {
            int filesRemaining = target.NumberOfFiles;
            if ((iNumFiles - filesRemaining) >= 0)
                worker.ReportProgress(iNumFiles - filesRemaining);
            if (!backgroundWorker_Deletion.IsBusy) return;
            else ProgressBarOperation(worker, e);
        }

        /// <summary>
        /// Sets up the Multithreading and Logging structs based on the control configurations and begins the deletion operation
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        private void RunDeletion(BackgroundWorker worker, DoWorkEventArgs e)
        {
            DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(MultiThreadingEnabled, ThreadCount);
            DeletionOps.Logging log = new DeletionOps.Logging(LoggingEnabled, LogTo);
            DeletionOps.Delete(target, mt, log, ShowOutput);
        }

		#endregion

		// ------------------------------------------------------------------------------------

		#region Events

		/// <summary>
		/// On load, set the controls based on the app.config file. Also disables the delete button
		/// and sets the toolstrip text and icon.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Main_Load(object sender, EventArgs e)
		{
			InitiateComboBox();

			Icon = SharedResources.Properties.Resources.icon;
			Tooltip = SharedResources.Properties.Resources.SelectTip;
			button_delete.Enabled = false;
		}

		/// <summary>
		/// When the user clicks the delete button, disable the controls and start the
		/// deletion operation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_delete_Click(object sender, EventArgs e)
        {
			// Disable the controls
			Enabled = false;
			bool cont = true;

            target = new Target(Target);

			if (target.Exists)
			{
				if (target.IsCritical && !Properties.Settings.Default.disableSafety)
					cont = CriticalTargetWarning();

				if (cont)
				{
					try
					{
						// If the user wants to monitor progress and the target is a directory
						// we need to setup the progress bar
						if (!DisableProgressMonitoring && target.IsDirectory)
						{
							iNumFiles = target.NumberOfFiles;
							progressBar.Maximum = iNumFiles;
							progressBar.Visible = true;
							backgroundWorker_ProgressBar.RunWorkerAsync();
						}

						backgroundWorker_Deletion.RunWorkerAsync();
					}
					catch (UnauthorizedAccessException)
					{
						//TODO: Write code to request elevation for a new process?
					}
					catch (FileNotFoundException)
					{
						//TODO: Write code to handle this strange exception...
					}
					catch (DirectoryNotFoundException)
					{
						//TODO: Write code to handle this strange exception...
					}
				}
				else
				{
					Enabled = true;
				}
			}
			else
			{
				//Target doesn't exist, display an error
				MessageBox.Show(SharedResources.Properties.Resources.TargetNotFound, SharedResources.Properties.Resources.Error);
			}
        }

        /// <summary>
        /// Launch a special dialog that allows the user to select either a file or a folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_browse_Click(object sender, EventArgs e)
        {
            FolderBrowser fb = new FolderBrowser();
				fb.Description = SharedResources.Properties.Resources.FolderBrowserDialogDescription;
				fb.IncludeFiles = true;
				fb.ShowNewFolderButton = false;
            if (fb.ShowDialog() == DialogResult.OK)
                field_target.Text = fb.SelectedPath;
        }
       

        /// <summary>
        /// Launch a save file dialog that allows the user to specify the log file location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_logToBrowse_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = SharedResources.Properties.Resources.SaveFileDialogFilter;
            saveFileDialog.Title = SharedResources.Properties.Resources.SaveFileDialogTitle;
            saveFileDialog.FileName = SharedResources.Properties.Resources.SaveFileDialogFileName;
            saveFileDialog.DefaultExt = SharedResources.Properties.Resources.SaveFileDialogDefaultExt;
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                field_logTo.Text = saveFileDialog.FileName;
        }

        /// <summary>
        /// Resets all settings to their default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_defaults_Click(object sender, EventArgs e)
        {
			Properties.Settings.Default.Reset();
        }

        /// <summary>
        /// Show the "About" dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        /// <summary>
        /// Opens a link using the default browser to the online FilExile help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetworkUtils.LaunchURL(CommonStrings.HelpUrl);
        }

        /// <summary>
        /// Configures the cascading multithreading controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkbox_multiThreading_CheckedChanged(object sender, EventArgs e)
        {
           MultiThreadingEnabled = checkbox_multiThreading.Checked;
        }

        /// <summary>
        /// Prevents the "Force" checkbox from being enabled and checked when it isn't appropriate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_completionAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_completionAction.SelectedIndex > 1)
            {
                checkbox_forceAction.Enabled = true;
            }
            else
            {
                checkbox_forceAction.Checked = false;
                checkbox_forceAction.Enabled = false;
            }
        }

        /// <summary>
        /// Initiates a manual version check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetworkUtils.InitiateVersionCheck(true);
        }

        /// <summary>
        /// Tries to display the local help file. If it isn't found, prompts to launch the online
        /// help instead.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(CommonStrings.LocalHelp))
                Help.ShowHelp(this, CommonStrings.LocalHelp);
            else
            {
                if (MessageBox.Show(SharedResources.Properties.Resources.HelpFileNotFound, SharedResources.Properties.Resources.Error, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    NetworkUtils.LaunchURL(CommonStrings.HelpUrl);
            }
        }

        /// <summary>
        /// When the text in the Target box changes, change the toolstrip text to guide the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void field_target_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Target))
            {
                button_delete.Enabled = true;
                toolStripLabel.Text = SharedResources.Properties.Resources.DeleteTip;
            }
            else
            {
                button_delete.Enabled = false;
                toolStripLabel.Text = SharedResources.Properties.Resources.SelectTip;
            }
        }

        /// <summary>
        /// Call upon the progress bar to update itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressBar_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker) sender;
            ProgressBarOperation(worker, e);
        }

        /// <summary>
        /// Update the progress bar and status strip with the latest progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressBar_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            statusStrip.Text = e.ProgressPercentage + "/" + iNumFiles;
        }

        /// <summary>
        /// Setup the multithreading struct and call the Delete method to remove the files/directories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_Deletion_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker) sender;
            RunDeletion(worker, e);
        }

        /// <summary>
        /// Once everything is complete, reset the form and re-enable the controls, then run the user specified
        /// completion command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_Deletion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
			// Re-enable the controls, reset the form
			Enabled = true;
            progressBar.Visible = false;

            // Remove the now empty directory
            if (Directory.Exists(Target))
                Directory.Delete(Target);

            // Set the target field back to blank
            Target = "";

			// Run the specified completion action
			CompletionAction ca = comboBox_completionAction.SelectedItem as CompletionAction;
			ca.Run(true, checkbox_forceAction.Checked);
        }

        /// <summary>
        /// Indicates that the drag over event will be accepted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Handles the DragDrop event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            //Clear anything that might be in the target field
            Target = string.Empty;

			//TODO: In the future, it'd be nice to be able to handle multiple files at once

            //Grab the filenames from the drop
            string[] filenames = (string[]) e.Data.GetData(DataFormats.FileDrop);

            //If the user tries to drop multiple items, let them know we will only process one
            if (filenames.Length > 1)
                MessageBox.Show(SharedResources.Properties.Resources.MultiFileError, SharedResources.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

            //Set the target to the first filename
            Target = filenames[0];
        }

		/// <summary>
		/// Configures the cascading logging controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkbox_logging_CheckedChanged(object sender, EventArgs e)
		{
			LoggingEnabled = checkbox_logging.Checked;
		}

		/// <summary>
		/// Called whenever a panel's "Enabled" state changes so it can pass
		/// the state onto child controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panel_EnabledChanged(object sender, EventArgs e)
		{
			Panel p = sender as Panel;
			foreach (Control c in p.Controls)
			{
				c.Enabled = c.Parent.Enabled;
				if (c is TextBox)
					((TextBox)c).ReadOnly = !c.Parent.Enabled;
				else if (c is NumericUpDown)
					((NumericUpDown)c).ReadOnly = !c.Parent.Enabled;
			}
		}

		/// <summary>
		/// When the dialog closes, save the properties that have changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Main_Closing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.Save();
		}
	}
    #endregion
}