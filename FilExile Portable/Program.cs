using Shared;
using System;
using System.Windows.Forms;

namespace FilExile_Portable
{
    static class Program
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

        #region Enums

        public enum ErrorCodes { SUCCESS, NOT_FOUND };

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
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    cla = new CommandLineArgs(args);
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Dialogs.Main());
                }
            }

            finally
            {

            }
        }

        #endregion
    }
}
