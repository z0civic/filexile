using System;
using System.Windows.Forms;
using Shared;

namespace FilExile.Dialogs
{
	public partial class RobocopyDlg : Form
	{
		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public RobocopyDlg()
		{
			InitializeComponent();

		}

		#endregion

		// ------------------------------------------------------------------------------------

		#region Private methods

		// ------------------------------------------------------------------------------------

		#region Events

		/// <summary>
		/// Sets the lables, etc. based on resources
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RobocopyDlg_Load(object sender, EventArgs e)
		{
			Text = SharedResources.Properties.Resources.Error;
			pb_ErrorIcon.Image = SharedResources.Properties.Resources.error_icon;
			lblRobocopyError.Text = SharedResources.Properties.Resources.RobocopyNotFound;
			btn_Ignore.Text = SharedResources.Properties.Resources.Ignore;
			btn_Close.Text = SharedResources.Properties.Resources.Close;
		}

		/// <summary>
		/// Sets the dialog result and closes the dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Ignore_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Ignore;
			Close();
		}

		/// <summary>
		/// Sets the dialog results and closes the dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Close_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Abort;
			Close();
		}

		private void lnklbl_downloadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			NetworkUtils.LaunchUrl(CommonStrings.RobocopyUrl);
		}
	}
	#endregion
	#endregion
}
