using Shared;
using System;
using System.Windows.Forms;

namespace FilExile
{
    class Program
    {
        /// <summary>
        /// FilExile is a simple file and directory deletion tool that utilizes Windows'
        /// "Robocopy" to help delete stubborn files and directories that otherwise can't
        /// be removed by normal methods.
        /// </summary>
        // ------------------------------------------------------------------------------------

        #region Objects

        private static CommandLineArgs cla;

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
                    cla = new CommandLineArgs(args);
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
                //TODO: Save any app.config changes
            }
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        #endregion
    }
}
