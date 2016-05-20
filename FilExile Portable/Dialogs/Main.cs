using Shared;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
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
			// Hooking in an assembly resolve event handler because SharedResources.dll (which holds the
			// strings, icons, etc.) is embedded into the executable for FilExile Portable. This allows
			// the assembly to be loaded without throwing a null reference error
			AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
			{
				var resourceName = new AssemblyName(args.Name).Name + ".dll";
				var resource = Array.Find(GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

				using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
				{
					if (stream == null) return null;
					var assemblyData = new byte[stream.Length];
					stream.Read(assemblyData, 0, assemblyData.Length);
					return Assembly.Load(assemblyData);
				}
			};

			InitializeComponent();
        }

		#endregion

		// ------------------------------------------------------------------------------------

		#region Fields

		// Holds the initial number of files in the target directory
		private int _iNumFiles;
		// The target that is going to be deleted
		private Target _target;

		#endregion

		// ------------------------------------------------------------------------------------

		#region Private methods

		/// <summary>
		/// Displays a warning message when a user attempts to delete a critical directory
		/// </summary>
		/// <returns>If the user wants to continue</returns>
		private static bool CriticalTargetWarning()
		{
			var dlg = new SafetyDlg();
			dlg.ShowDialog();
			return dlg.DialogResult == DialogResult.Yes;
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

			// Fields
			field_target.Enabled = bEnabled;
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
			    var filesRemaining = _target.NumberOfFiles;
			    if ((_iNumFiles - filesRemaining) >= 0)
				    worker.ReportProgress(_iNumFiles - filesRemaining);
			    if (!backgroundWorker_Deletion.IsBusy) return;
		    }
	    }

	    /// <summary>
		/// Sets up the Multithreading and Logging structs based on the control configurations and begins the deletion operation
		/// </summary>
		private void RunDeletion()
		{
			var mt = new DeletionOps.MultithreadingSetup(true, 8);
			var log = new DeletionOps.Logging(false, "");
			DeletionOps.Delete(_target, mt, log, false);
		}

		// ------------------------------------------------------------------------------------

		#region Events

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
		/// On load, sets the icon and disables the Delete button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Main_Load(object sender, EventArgs e)
		{
			Icon = SharedResources.Properties.Resources.icon;
			button_delete.Enabled = false;
		}

		/// <summary>
		/// Launch a special dialog that allows the user to select either a file or a folder
		/// </summary>
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

			_target = new Target(field_target.Text);

			if (_target.Exists)
			{
				if (_target.IsCritical)
					cont = CriticalTargetWarning();

				if (cont)
				{
					try
					{
						// If the user wants to monitor progress and the target is a directory
						// we need to setup the progress bar
						if (_target.IsDirectory)
						{
							_iNumFiles = _target.NumberOfFiles;
							progressBar.Maximum = _iNumFiles;
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
		/// Opens a link using the default browser to the online FilExile help
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NetworkUtils.LaunchUrl(CommonStrings.HelpUrl);
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
		/// When the text in the Target box changes, enables/disables the Delete button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void field_target_TextChanged(object sender, EventArgs e)
		{
			button_delete.Enabled = !string.IsNullOrEmpty(field_target.Text);
		}

	    /// <summary>
		/// Call upon the progress bar to update itself
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_ProgressBar_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;
			ProgressBarOperation(worker);
		}

		/// <summary>
		/// Update the progress bar and status strip with the latest progress
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_ProgressBar_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
		}

		/// <summary>
		/// Setup the multithreading struct and call the Delete method to remove the files/directories
		/// </summary>
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
			ChangeControlStates(true);
			progressBar.Visible = false;

			// Remove the now empty directory
			if (Directory.Exists(field_target.Text))
				Directory.Delete(field_target.Text);

			// Set the target field back to blank
			field_target.Text = "";

			// Run the specified completion action
			
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
			string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);

			//If the user tries to drop multiple items, let them know we will only process one
			if (filenames.Length > 1)
				MessageBox.Show(SharedResources.Properties.Resources.MultiFileError, SharedResources.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

			//Set the target to the first filename
			field_target.Text = filenames[0];
		}
	}
	#endregion
	#endregion
}
