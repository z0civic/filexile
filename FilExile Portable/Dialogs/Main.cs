using Shared;
using System;
using System.Windows.Forms;

namespace FilExile_Portable.Dialogs
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Icon = SharedResources.Properties.Resources.icon;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
    }
}
