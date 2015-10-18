using System;
using System.Windows.Forms;

namespace Shared
{
    public partial class DownloadDlg : Form
    {
        public DownloadDlg()
        {
            InitializeComponent();
            label_Download.Text = SharedResources.Properties.Resources.NewerVersion;
            button_Cancel.Text = SharedResources.Properties.Resources.Cancel;
            button_Download.Text = SharedResources.Properties.Resources.Download;
        }

        private void button_Download_Click(object sender, EventArgs e)
        {
            NetworkUtils.DownloadLatestVersion();
            Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
