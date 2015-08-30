using System;
using System.IO;
using System.Windows.Forms;

namespace FilExile
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
            this.Icon = Properties.Resources.icon;
            this.toolStripLabel.Text = Properties.Resources.SelectTip;
            this.button_delete.Enabled = false;
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Fields

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>
        /// On load, set the controls based on the app.config file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            SetControls();
        }

        /// <summary>
        /// Based on the values read in from the app.config, set the various controls
        /// to the user defined values.
        /// </summary>
        private void SetControls()
        {
            //Options - Logging
            SetLogging(Properties.Settings.Default.logging);
            field_logTo.Text = Properties.Settings.Default.logTo;

            //Options - Automatically check for updates
            checkbox_autoUpdate.Checked = Properties.Settings.Default.autoUpdate;

            //Options - Completion action
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

            //Advanced - Output
            checkbox_output.Checked = Properties.Settings.Default.showOutput;

            //Advanced - Multithreading
            SetMultiThreading(Properties.Settings.Default.multiThreading);
            int tc = Properties.Settings.Default.threadCount;
            if (tc < 1 || tc > 128)
                tc = 8;
            spinner_threadCount.Value = tc;

            //Advanced - Always on top
            checkbox_alwaysOnTop.Checked = Properties.Settings.Default.alwaysOnTop;

            //Advaned - Progress monitor
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
            field_logTo.Enabled = bEnabled;
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
            //Buttons
            button_browse.Enabled = bEnabled;
            button_delete.Enabled = bEnabled;
            button_logToBrowse.Enabled = bEnabled;
            button_defaults.Enabled = bEnabled;

            //Fields
            field_target.Enabled = bEnabled;
            field_logTo.Enabled = bEnabled;

            //Checkboxes
            checkbox_logging.Enabled = bEnabled;
            checkbox_output.Enabled = bEnabled;
            checkbox_forceAction.Enabled = bEnabled;
            checkbox_alwaysOnTop.Enabled = bEnabled;
            checkbox_autoUpdate.Enabled = bEnabled;
            checkbox_disableProgressMonitoring.Enabled = bEnabled;
            checkbox_multiThreading.Enabled = bEnabled;

            //Misc controls
            comboBox_completionAction.Enabled = bEnabled;
            spinner_threadCount.Enabled = bEnabled;

            //Some of the controls are cascading and should only be enabled if it's appropriate
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

        #endregion

        // ------------------------------------------------------------------------------------

        #region Events

        /// <summary>
        /// When the user clicks the delete button, disable the controls and start the
        /// deletion operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_delete_Click(object sender, EventArgs e)
        {
            ChangeControlStates(false);
            Target target = new Target(field_target.Text);

            if (target.Exists)
            {
                if (!checkbox_disableProgressMonitoring.Checked)
                {
                    progressBar.Visible = true;
                    if (target.IsDirectory)
                    {
                        //TODO: Count files in directory
                    }
                }

                DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(checkbox_multiThreading.Checked, spinner_threadCount.Value);
                int retval = DeletionOps.Delete(target, mt, checkbox_logging.Checked, field_logTo.Text);
            }
            else 
            { 
                //TODO: Throw file/directory not found exception
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
            fb.Description = Properties.Resources.FolderBrowserDialogDescription;
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
            saveFileDialog.Filter = Properties.Resources.SaveFileDialogFilter;
            saveFileDialog.Title = Properties.Resources.SaveFileDialogTitle;
            saveFileDialog.FileName = Properties.Resources.SaveFileDialogFileName;
            saveFileDialog.DefaultExt = Properties.Resources.SaveFileDialogDefaultExt;
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
            Dialogs.AboutBox about = new Dialogs.AboutBox();
            about.ShowDialog();
        }

        /// <summary>
        /// Opens a link using the default browser to the online FilExile help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.NetworkUtils.LaunchURL("http://filexile.sourceforge.net/help.htm");
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
            Utilities.NetworkUtils.InitiateVersionCheck(true);
        }

        /// <summary>
        /// Tries to display the local help file. If it isn't found, prompts to launch the online
        /// help instead.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(".\\FilExile Help.chm"))
                Help.ShowHelp(this, ".\\FilExile Help.chm");
            else
            {
                if (MessageBox.Show(Properties.Resources.HelpFileNotFound, Properties.Resources.Error, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Utilities.NetworkUtils.LaunchURL("http://filexile.sourceforge.net/help.htm");
            }
        }

        /// <summary>
        /// When the text in the Target box changes, change the toolstrip text to guide the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void field_target_TextChanged(object sender, EventArgs e)
        {
            if (field_target.Text != "")
            {
                button_delete.Enabled = true;
                toolStripLabel.Text = Properties.Resources.DeleteTip;
            }
            else
            {
                button_delete.Enabled = false;
                toolStripLabel.Text = Properties.Resources.SelectTip;
            }
        }
    }
    #endregion
}