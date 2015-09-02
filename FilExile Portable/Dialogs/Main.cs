using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FilExile_Portable.Dialogs
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Icon = Properties.Resources.icon;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
    }
}
