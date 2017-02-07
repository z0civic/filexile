using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FilExile.Dialogs
{
    public partial class Main : Form
    {
        #region Constructor
        
        /// <summary>Default constructor</summary>
        public Main()
        {
            InitializeComponent();

            // If specified in the app.config file, perform an update check
            if (Properties.Settings.Default.autoUpdate)
                NetworkUtils.InitiateVersionCheck(false);
			// If specified in the app.config file, place FilExile always on top
	        if (Properties.Settings.Default.alwaysOnTop)
		        TopMost = true;
        }

		#endregion

		// ------------------------------------------------------------------------------------

		#region Fields

		//The target that is going to be deleted
		private Target m_target;
		// Holds the initial number of files in the target directory when tracking progress
		private int m_iNumFiles;

		#endregion

		// ------------------------------------------------------------------------------------

		#region Properties

		/// <summary>If verbose output should be displayed in the console.</summary>
		private bool ShowOutput => checkbox_output.Checked;

	    /// <summary>If logging to a file is enabled.</summary>
		private bool LoggingEnabled
		{
			get { return checkbox_logging.Checked; }
			set { EnableDisableLogFilePanel(value); }
		}

		/// <summary>File (including path) to log to.</summary>
		private string LogTo => field_logTo.Text;

	    /// <summary>If multithreaded Robocopy operations are enabled.</summary>
		private bool MultiThreadingEnabled
		{
			get { return checkbox_multiThreading.Checked; }
			set	{ EnableDisableMultithreadingPanel(value);	}
		}

	    private decimal ThreadCount => spinner_threadCount.Value;

	    /// <summary>If FilExile should remain on top of other applications.</summary>
	    private bool AlwaysOnTop => checkbox_alwaysOnTop.Checked;

	    /// <summary>If progress monitoring should be disabled (enhances performance).</summary>
		private bool DisableProgressMonitoring => checkbox_disableProgressMonitoring.Checked;

	    /// <summary>String value of the target to be deleted.</summary>
		private string Target
		{
			get { return field_target.Text; }
			set { field_target.Text = value; }
		}

		/// <summary>Tooltip displayed in the bottom-left corner.</summary>
		private string Tooltip
		{
			set { toolStripLabel.Text = value; }
		}

		#endregion

		// ------------------------------------------------------------------------------------

		#region Private methods

		/// <summary>Displays a warning message when a user attempts to delete a critical directory</summary>
		/// <returns>If the user wants to continue</returns>
		private static bool CriticalTargetWarning()
		{
			var dlg = new SafetyDlg();
			dlg.ShowDialog();
			return dlg.DialogResult == DialogResult.Yes;
		}

		/// <summary>Associates the completion aciton combobox with databinding</summary>
		private void InitiateComboBox()
		{
			var dataSource = new List<CompletionAction>
			{
				new CompletionAction() {Name = SharedResources.Properties.Resources.DoNothing, Value = 0},
				new CompletionAction() {Name = SharedResources.Properties.Resources.PlaySound, Value = 1},
				new CompletionAction() {Name = SharedResources.Properties.Resources.Reboot, Value = 2},
				new CompletionAction() {Name = SharedResources.Properties.Resources.Shutdown, Value = 3}
			};

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
	    private void ProgressBarOperation(BackgroundWorker worker)
	    {
		    while (true)
		    {
			    var filesRemaining = m_target.NumberOfFiles;
			    if ((m_iNumFiles - filesRemaining) >= 0)
				    worker.ReportProgress(m_iNumFiles - filesRemaining);
			    if (!backgroundWorker_Deletion.IsBusy) return;
		    }
	    }

	    /// <summary>
        /// Sets up the Multithreading and Logging structs based on the control configurations and begins the deletion operation
        /// </summary>
        private void RunDeletion()
	    {
		    if (ShowOutput)
			    AllocConsole();

            var mt = new DeletionOps.MultithreadingSetup(MultiThreadingEnabled, ThreadCount);
            var log = new DeletionOps.Logging(LoggingEnabled, LogTo);
            DeletionOps.Delete(m_target, mt, log, ShowOutput);
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

			if (WindowsOps.WindowsXpOrLower())
			{
				checkbox_multiThreading.Checked = false;
				checkbox_multiThreading.Enabled = false;
				EnableDisableMultithreadingPanel(false);
			}

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
            m_target = new Target(Target);

			if (m_target.Exists)
			{
				bool cont = true;
				if (m_target.IsCritical && !Properties.Settings.Default.disableSafety)
					cont = CriticalTargetWarning();

				if (cont)
				{
					try
					{
						// Disable the controls
						Enabled = false;
						UseWaitCursor = true;

						// If the user wants to monitor progress and the target is a directory
						// we need to setup the progress bar
						if (!DisableProgressMonitoring && m_target.IsDirectory)
						{
							m_iNumFiles = m_target.NumberOfFiles;
							progressBar.Maximum = m_iNumFiles;
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
					finally
					{
						Enabled = true;
						UseWaitCursor = false;
					}
				}
			}
			else
			{
				//Target doesn't exist, display an error
				MessageBox.Show(SharedResources.Properties.Resources.TargetNotFound, SharedResources.Properties.Resources.Error);
			}
		}

        /// <summary>Launch a special dialog that allows the user to select either a file or a folder.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_browse_Click(object sender, EventArgs e)
        {
	        var fb = new FolderBrowser
	        {
		        Description = SharedResources.Properties.Resources.FolderBrowserDialogDescription,
		        IncludeFiles = true,
		        ShowNewFolderButton = false
	        };
	        if (fb.ShowDialog() == DialogResult.OK)
                field_target.Text = fb.SelectedPath;
        }
       

        /// <summary>Launch a save file dialog that allows the user to specify the log file location.</summary>
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
		
        /// <summary>Resets all settings to their default values.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_defaults_Click(object sender, EventArgs e)
        {
			if (MessageBox.Show(this, SharedResources.Properties.Resources.ResetToDefaults, SharedResources.Properties.Resources.Confirm, MessageBoxButtons.YesNo) == DialogResult.Yes)
				Properties.Settings.Default.Reset();
        }

        /// <summary>Show the "About" dialog.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        /// <summary>Opens a link using the default browser to the online FilExile help.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetworkUtils.LaunchUrl(CommonStrings.HelpUrl);
        }

        /// <summary>Configures the cascading multithreading controls.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkbox_multiThreading_CheckedChanged(object sender, EventArgs e)
        {
           MultiThreadingEnabled = checkbox_multiThreading.Checked;
        }

        /// <summary>Prevents the "Force" checkbox from being enabled and checked when it isn't appropriate.</summary>
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

        /// <summary>Initiates a manual version check.</summary>
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
                    NetworkUtils.LaunchUrl(CommonStrings.HelpUrl);
            }
        }

        /// <summary>When the text in the Target box changes, change the toolstrip text to guide the user.</summary>
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

        /// <summary>Call upon the progress bar to update itself.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressBar_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker) sender;
            ProgressBarOperation(worker);
        }

        /// <summary>Update the progress bar and status strip with the latest progress.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressBar_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            statusStrip.Text = e.ProgressPercentage + "/" + m_iNumFiles;
        }

        /// <summary>Call the Delete method to remove the files/directories.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_Deletion_DoWork(object sender, DoWorkEventArgs e)
        {
            RunDeletion();
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
            progressBar.Visible = false;

            // Remove the now empty directory
	        if (Directory.Exists(Target))
	        {
                Directory.Delete(Target, true);
			}

			// Set the target field back to blank
			Target = "";

			// Run the specified completion action
			var ca = comboBox_completionAction.SelectedItem as CompletionAction;
	        ca?.Run(true, checkbox_forceAction.Checked);
        }

        /// <summary>Indicates that the drag over event will be accepted.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>Handles the DragDrop event.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            //Clear anything that might be in the target field
            Target = string.Empty;

			//TODO: In the future, it'd be nice to be able to handle multiple files at once

            //Grab the filenames from the drop
            var filenames = (string[]) e.Data.GetData(DataFormats.FileDrop);

            //If the user tries to drop multiple items, let them know we will only process one
            if (filenames.Length > 1)
                MessageBox.Show(SharedResources.Properties.Resources.MultiFileError, SharedResources.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

            //Set the target to the first filename
            Target = filenames[0];
        }

		/// <summary>Configures the cascading logging controls.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkbox_logging_CheckedChanged(object sender, EventArgs e)
		{
			LoggingEnabled = checkbox_logging.Checked;
		}

		/// <summary>Enables or disables the log file panel based on the passed value.</summary>
		/// <param name="enable">True to enable the panel and all contained controls</param>
		private void EnableDisableLogFilePanel(bool enable)
	    {
		    foreach (Control c in panel_LogFile.Controls)
		    {
			    c.Enabled = enable;
			    var box = c as TextBox;
			    if (box != null)
				    box.ReadOnly = !box.Parent.Enabled;
			}
	    }

		/// <summary>Enables or disables the log multithreading panel based on the passed value.</summary>
		/// <param name="enable">True to enable the panel and all contained controls</param>
		private void EnableDisableMultithreadingPanel(bool enable)
		{
			foreach (Control c in panel_Multithreading.Controls)
			{
				c.Enabled = enable;
				var down = c as NumericUpDown;
				if (down != null)
					down.ReadOnly = !down.Parent.Enabled;
			}
		}

		/// <summary>When the dialog closes, save the properties that have changed.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Main_Closing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.Save();
		}

		/// <summary>When the value of the Always On Top checkbox changes.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkbox_alwaysOnTop_CheckedChanged(object sender, EventArgs e)
		{
			TopMost = AlwaysOnTop;
		}

		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AllocConsole();
	}
    
	#endregion

}