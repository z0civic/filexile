using Shared;
using System;
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
        /// Based on the values read in from the app.config, set the various controls
        /// to the user defined values.
        /// </summary>
        private void SetControls()
        {
            // Options - Logging
            SetLogging(Properties.Settings.Default.logging);
            field_logTo.Text = Properties.Settings.Default.logTo;

            // Options - Automatically check for updates
            checkbox_autoUpdate.Checked = Properties.Settings.Default.autoUpdate;

            // Options - Completion action
            comboBox_completionAction.Text = comboBox_completionAction.Items[Properties.Settings.Default.completionAction].ToString();
            if (Properties.Settings.Default.completionAction > 1)
            {
                checkbox_forceAction.Checked = Properties.Settings.Default.forceAction;
                checkbox_forceAction.Enabled = true;
            }
            else
            {
                checkbox_forceAction.Checked = false;
                checkbox_forceAction.Enabled = false;
            }

            // Advanced - Output
            checkbox_output.Checked = Properties.Settings.Default.showOutput;

            // Advanced - Multithreading
            SetMultiThreading(Properties.Settings.Default.multiThreading);
            int tc = Properties.Settings.Default.threadCount;
            if (tc < 1 || tc > 128)
                tc = 8;
            spinner_threadCount.Value = tc;

            // Advanced - Always on top
            checkbox_alwaysOnTop.Checked = Properties.Settings.Default.alwaysOnTop;

            // Advaned - Progress monitor
            checkbox_disableProgressMonitoring.Checked = Properties.Settings.Default.disableProgressMonitoring;
        }

        /// <summary>
        /// Enables or disables logging based on the passed value
        /// </summary>
        /// <param name="bEnabled">If logging is enabled</param>
        private void SetLogging(bool bEnabled)
        {
            checkbox_logging.Checked = bEnabled;
            button_logToBrowse.Enabled = bEnabled;
            if (bEnabled)
                label_logTo.ForeColor = System.Drawing.SystemColors.ControlText;
            else
                label_logTo.ForeColor = System.Drawing.SystemColors.GrayText;
        }

        /// <summary>
        /// Enables or disables multithreading based on the passed value
        /// </summary>
        /// <param name="bEnabled">If multithreading is enabled</param>
        private void SetMultiThreading(bool bEnabled)
        {
            if (bEnabled)
            {
                checkbox_multiThreading.Checked = true;
                spinner_threadCount.Enabled = true;
                label_threadCount.ForeColor = System.Drawing.SystemColors.ControlText;
                label_threadBounds.ForeColor = System.Drawing.SystemColors.ControlText;
            }
            else
            {
                checkbox_multiThreading.Checked = false;
                spinner_threadCount.Enabled = false;
                label_threadCount.ForeColor = System.Drawing.SystemColors.GrayText;
                label_threadBounds.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        /// <summary>
        /// Changes the enabled state of all main form controls based on the passed value
        /// </summary>
        /// <param name="bEnabled">If the controls should be enabled</param>
        private void ChangeControlStates(bool bEnabled)
        {
            // Buttons
            button_browse.Enabled = bEnabled;
            button_delete.Enabled = bEnabled;
            button_logToBrowse.Enabled = bEnabled;
            button_defaults.Enabled = bEnabled;

            // Fields
            field_target.Enabled = bEnabled;

            // Checkboxes
            checkbox_logging.Enabled = bEnabled;
            checkbox_output.Enabled = bEnabled;
            checkbox_forceAction.Enabled = bEnabled;
            checkbox_alwaysOnTop.Enabled = bEnabled;
            checkbox_autoUpdate.Enabled = bEnabled;
            checkbox_disableProgressMonitoring.Enabled = bEnabled;
            checkbox_multiThreading.Enabled = bEnabled;

            // Misc controls
            comboBox_completionAction.Enabled = bEnabled;
            spinner_threadCount.Enabled = bEnabled;

            // Some of the controls are cascading and should only be enabled if it's appropriate
            if (bEnabled)
            {
                if (!checkbox_logging.Checked)
                    button_logToBrowse.Enabled = false;
                if (!checkbox_multiThreading.Checked)
                    spinner_threadCount.Enabled = false;
                if (comboBox_completionAction.SelectedIndex < 2)
                    checkbox_forceAction.Enabled = false;
            }
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
            DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(checkbox_multiThreading.Checked, spinner_threadCount.Value);
            DeletionOps.Logging log = new DeletionOps.Logging(checkbox_logging.Checked, field_logTo.Text);
            DeletionOps.Delete(target, mt, log, checkbox_output.Checked);
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
			SetControls();
			Icon = SharedResources.Properties.Resources.icon;
			toolStripLabel.Text = SharedResources.Properties.Resources.SelectTip;
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
            ChangeControlStates(false);
			bool cont = true;

            target = new Target(field_target.Text);

			if (target.Exists)
			{
				if (target.IsCritical)
					cont = CriticalTargetWarning();

				if (cont)
				{
					try
					{
						// If the user wants to monitor progress and the target is a directory
						// we need to setup the progress bar
						if (!checkbox_disableProgressMonitoring.Checked && target.IsDirectory)
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
					ChangeControlStates(true);
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
            SetControls();
        }

        /// <summary>
        /// Configures the cascading logging controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkbox_logging_CheckedChanged(object sender, EventArgs e)
        {
            SetLogging(checkbox_logging.Checked);
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
            NetworkUtils.LaunchURL("http://filexile.sourceforge.net/help.htm");
        }

        /// <summary>
        /// Configures the cascading multithreading controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkbox_multiThreading_CheckedChanged(object sender, EventArgs e)
        {
            SetMultiThreading(checkbox_multiThreading.Checked);
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
            if (File.Exists(@".\FilExile Help.chm"))
                Help.ShowHelp(this, @".\FilExile Help.chm");
            else
            {
                if (MessageBox.Show(SharedResources.Properties.Resources.HelpFileNotFound, SharedResources.Properties.Resources.Error, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    NetworkUtils.LaunchURL("http://filexile.sourceforge.net/help.htm");
            }
        }

        /// <summary>
        /// When the text in the Target box changes, change the toolstrip text to guide the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void field_target_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(field_target.Text))
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
            ChangeControlStates(true);
            progressBar.Visible = false;

            // Remove the now empty directory
            if (Directory.Exists(field_target.Text))
                Directory.Delete(field_target.Text);

            // Set the target field back to blank
            field_target.Text = "";

            // Run the specified completion action
            CompletionActions.RunCompletionEvent((CompletionActions.Actions) comboBox_completionAction.SelectedIndex, true, checkbox_forceAction.Checked);
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
            field_target.Text = "";

            //Grab the filenames from the drop
            string[] filenames = (string[]) e.Data.GetData(DataFormats.FileDrop);

            //If the user tries to drop multiple items, let them know we will only process one
            if (filenames.Length > 1)
                MessageBox.Show(SharedResources.Properties.Resources.MultiFileError, SharedResources.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

            //Set the target to the first filename
            field_target.Text = filenames[0];
        }

        /// <summary>
        /// Makes sure the user is selecting a valid number of threads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spinner_threadCount_ValueChanged(object sender, EventArgs e)
        {
            if (spinner_threadCount.Value < 1 || spinner_threadCount.Value > 128)
            {
                MessageBox.Show(SharedResources.Properties.Resources.InvalidThreadCount, SharedResources.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                spinner_threadCount.Value = 8;
            }

        }
    }
    #endregion
}