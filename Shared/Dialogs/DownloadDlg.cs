using System;
using System.Windows.Forms;

namespace Shared
{
    public partial class DownloadDlg : Form
    {
        public DownloadDlg()
        {
            InitializeComponent();
            this.label_Download.Text = Shared.Resources.NewerVersion;
            this.button_Cancel.Text = Shared.Resources.Cancel;
            this.button_Download.Text = Shared.Resources.Download;
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
