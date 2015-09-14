﻿using System;
using System.Windows.Forms;

namespace FilExile.Dialogs
{
	public partial class SafetyDlg : Form
	{
		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public SafetyDlg()
		{
			InitializeComponent();
		}

		#endregion

		// ------------------------------------------------------------------------------------

		#region Private methods

		/// <summary>
		/// Saves the disable safety option to the app.config file
		/// </summary>
		private void SaveOption()
		{

		}

		// ------------------------------------------------------------------------------------

		#region Events

		/// <summary>
		/// Sets the labels, etc. based on resources
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SafetyDlg_Load(object sender, EventArgs e)
		{
			Text = SharedResources.Properties.Resources.Warning;
			pictureBox_WarningIcon.Image = SharedResources.Properties.Resources.warning_icon;
			label_SafetyWarning.Text = SharedResources.Properties.Resources.CriticalTarget;
			label_Continue.Text = SharedResources.Properties.Resources.Continue;
		}

		/// <summary>
		/// Saves the option and closes the dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_No_Click(object sender, EventArgs e)
		{
			SaveOption();
			Close();
		}

		/// <summary>
		/// Saves the option and closes the dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Yes_Click(object sender, EventArgs e)
		{
			SaveOption();
			Close();
		}
	}
	#endregion
	#endregion
}
