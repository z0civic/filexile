using Shared;
using System;
using System.Windows.Forms;

namespace FilExile
{
	internal static class Program
    {
        /// <summary>
        /// FilExile is a simple file and directory deletion tool that utilizes Windows'
        /// "Robocopy" to help delete stubborn files and directories that otherwise can't
        /// be removed by normal methods.
        /// </summary>
        // ------------------------------------------------------------------------------------

        #region Objects

        private static CommandLineArgs _cla;

		#endregion

		// ------------------------------------------------------------------------------------

		#region Fields

		#endregion

		// ------------------------------------------------------------------------------------

		#region Public methods

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        public static void Main(string[] args)
        {
            try
            {
                //If any command line arguments pass, prepare them for parsing
                if (args.Length > 0)
                {
                    _cla = new CommandLineArgs(args);
                    AttachConsole(AttachParentProcess);

                    if (_cla.HelpFlag)
                        CommandLineInterface.DisplayHelp();
                    else
                        CommandLineInterface.Run(args[0], _cla);
                }
                //Otherwise, launch the FilExile GUI
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Dialogs.Main());
                }
            }

            finally
            {
                FreeConsole();
            }
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        // Import kernel32.dll to attach a console
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern int FreeConsole();
        private const int AttachParentProcess = -1;

        #endregion
    }
}
