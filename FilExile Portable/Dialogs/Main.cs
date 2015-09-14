using Shared;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FilExile_Portable.Dialogs
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
				string resourceName = new AssemblyName(args.Name).Name + ".dll";
				string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

				using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
				{
					Byte[] assemblyData = new Byte[stream.Length];
					stream.Read(assemblyData, 0, assemblyData.Length);
					return Assembly.Load(assemblyData);
				}
			};

			InitializeComponent();
        }

		#endregion

		// ------------------------------------------------------------------------------------

		#region Objects

		// The target that is going to be deleted
		private Target target;

		#endregion

		// ------------------------------------------------------------------------------------

		#region Fields

		// Holds the initial number of files in the target directory
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
			DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(true, 8);
			DeletionOps.Logging log = new DeletionOps.Logging(false, "");
			DeletionOps.Delete(target, mt, log, false);
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
			FolderBrowser fb = new FolderBrowser();
			fb.Description = SharedResources.Properties.Resources.FolderBrowserDialogDescription;
			fb.IncludeFiles = true;
			fb.ShowNewFolderButton = false;
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
						if (target.IsDirectory)
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
		/// Opens a link using the default browser to the online FilExile help
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NetworkUtils.LaunchURL("http://filexile.sourceforge.net/help.htm");
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
			if (!string.IsNullOrEmpty(field_target.Text))
			{
				button_delete.Enabled = true;
			}
			else
			{
				button_delete.Enabled = false;
			}
		}

		/// <summary>
		/// Call upon the progress bar to update itself
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_ProgressBar_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;
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
		}

		/// <summary>
		/// Setup the multithreading struct and call the Delete method to remove the files/directories
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_Deletion_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;
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
			CompletionActions.RunCompletionEvent(CompletionActions.Actions.PLAY_SOUND, true, false);
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
