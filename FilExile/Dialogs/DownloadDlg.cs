using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FilExile.Dialogs
{
    public partial class DownloadDlg : Form
    {
        public DownloadDlg()
        {
            InitializeComponent();
            this.label_Download.Text = Properties.Resources.NewerVersion;
            this.button_Cancel.Text = Properties.Resources.Cancel;
            this.button_Download.Text = Properties.Resources.Download;
        }

        private void button_Download_Click(object sender, EventArgs e)
        {
            Utilities.NetworkUtils.DownloadLatestVersion();
            Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
