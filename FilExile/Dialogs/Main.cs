using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FilExile
{
    public partial class Main : Form
    {
        #region Constructor

        public Main()
        {
            InitializeComponent();
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Fields

        private static Int32 startingSize;

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
            this.SetControls();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            this.button_browse.Enabled = false;
            this.button_delete.Visible = false;
            this.field_target.Enabled = false;

            Target target = new Target(this.field_target.Text);

            if (target.Exists)
            {
                if (!this.checkbox_disableProgressMonitoring.Checked)
                {
                    this.progressBar.Visible = true;
                    if (target.IsDirectory)
                    {
                        //TODO: Count files in directory
                    }
                }

                DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(this.checkbox_multiThreading.Checked, this.spinner_threadCount.Value);
                int retval = DeletionOps.Delete(target, mt, this.checkbox_logging.Checked, this.field_logTo.Text);
            }
            else 
            { 
                //TODO: Throw file/directory not found exception
            }

        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            FolderBrowser fb = new FolderBrowser();
            fb.Description = "Select a file or directory to delete...";
            fb.IncludeFiles = true;
            fb.ShowNewFolderButton = false;
            if (fb.ShowDialog() == DialogResult.OK) 
                this.field_target.Text = fb.SelectedPath;
        }

        private void button_logToBrowse_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            this.saveFileDialog.Title = "Save FilExile log file as...";
            this.saveFileDialog.FileName = "FilExile_output";
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.AddExtension = true;
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
                this.field_logTo.Text = saveFileDialog.FileName;
        }

        private void button_defaults_Click(object sender, EventArgs e)
        {
            Program.DefaultSettings();
            this.SetControls();
        }

        private void checkbox_logging_CheckedChanged(object sender, EventArgs e)
        {
            this.button_logToBrowse.Enabled = this.checkbox_logging.Checked;
        }

        private void SetControls()
        {
            this.checkbox_alwaysOnTop.Checked = Program.alwaysOnTop;
            this.checkbox_autoUpdate.Checked = Program.autoUpdate;
            this.checkbox_forceAction.Checked = Program.forceAction;
            if (Program.logging)
            {
                this.checkbox_logging.Checked = true;
                this.button_logToBrowse.Enabled = true;
            }
            else
            {
                this.checkbox_logging.Checked = false;
                this.button_logToBrowse.Enabled = false;
            }
            this.checkbox_multiThreading.Checked = Program.multiThreading;
            this.checkbox_output.Checked = Program.output;
            this.field_logTo.Text = Program.logTo;
            this.spinner_threadCount.Value = Program.threadCount;
            this.comboBox_completionAction.Text = this.comboBox_completionAction.Items[(int)Program.completionAction].ToString();
            this.checkbox_disableProgressMonitoring.Checked = Program.disableProgressMonitoring;
        }

        #endregion
    }
}
