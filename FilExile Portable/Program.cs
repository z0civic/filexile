using System;
using System.Windows.Forms;
using Shared;

namespace FilExile
{
	internal static class Program
    {
		/// <summary>
		/// FilExile Portable is a simple file and directory deletion tool that utilizes Windows'
		/// "Robocopy" to help delete stubborn files and directories that otherwise can't
		/// be removed by normal methods. FilExile Portable does not have all the features
		/// that FilExile has.
		/// </summary>
		// ------------------------------------------------------------------------------------

		#region Fields

		private static CommandLineArgs _cla;

		#endregion

		// ------------------------------------------------------------------------------------

		#region Public methods
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        public static void Main(string[] args)
        {
	        if (args.Length > 0)
	        {
		        _cla = new CommandLineArgs(args);

		        if (_cla.HelpFlag)
			        CommandLineInterface.DisplayHelp();
		        else
			        CommandLineInterface.Run(args[0], _cla);
	        }
	        else
	        {
		        Application.EnableVisualStyles();
		        Application.SetCompatibleTextRenderingDefault(false);
		        Application.Run(new Dialogs.Main());
	        }
        }

		#endregion

        // ------------------------------------------------------------------------------------
	}
}
