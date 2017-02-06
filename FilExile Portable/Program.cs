using System;
using System.Windows.Forms;
using Shared;
using FilExile.Dialogs;

namespace FilExile
{
	internal static class Program
    {
		/// <summary>
		/// FilExile Portable is a simple file and directory deletion tool that utilizes Windows'
		/// "Robocopy" to help delete stubborn files and directories that otherwise can't
		/// be removed by normal methods. FilExile Portable does not have all the features
		/// of the full version of FilExile.
		/// </summary>
		// ------------------------------------------------------------------------------------

		#region Fields

		private static CommandLineArgs _cla;

		#endregion

		// ------------------------------------------------------------------------------------

		#region Public methods

		/// <summary>The main entry point for the application.</summary>
		[STAThread]
        public static void Main(string[] args)
        {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Verify that robocopy is present on the system
			if (!WindowsOps.VerifyRobocopy())
			{
				// Display error message if Robocopy isn't found
				var dlg = new RobocopyDlg();
				dlg.ShowDialog();

				// If the user didn't choose to ignore, close FilExile
				if (dlg.DialogResult != DialogResult.Ignore)
				{
					Application.Exit();
					Environment.Exit(1);
				}
			}

			// If any command line arguments are passed, prepare them for parsing
			if (args.Length > 0)
	        {
		        _cla = new CommandLineArgs(args);

		        if (_cla.HelpFlag)
			        CommandLineInterface.DisplayHelp();
		        else
			        CommandLineInterface.Run(args[0], _cla);
	        }
			// Otherwise, launch the FilExile GUI
			else
			{
		        Application.Run(new Main());
	        }
        }

		#endregion

        // ------------------------------------------------------------------------------------
	}
}
