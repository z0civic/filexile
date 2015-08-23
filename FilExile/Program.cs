using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        #region Enums

        public enum ErrorCodes {SUCCESS, NOT_FOUND};

        #endregion

        // ------------------------------------------------------------------------------------

        #region Fields

        // These fields hold the settings read in from the app.config file so we don't have to
        // reference the file over and over
        public static bool autoUpdate;
        public static bool logging;
        public static bool output;
        public static bool advanced;
        public static string logTo;
        public static decimal completionAction;
        public static bool forceAction;
        public static bool debugMode;
        public static decimal threadCount;
        public static bool multiThreading;
        public static bool disableRobocopyCheck;
        public static bool alwaysOnTop;
        public static bool disableProgressMonitoring;

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
                InitializeResources();

                //If we're running in VS debug mode, set the debug flag
                #if DEBUG
                    debugMode = true;
                #endif

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
                    Application.Run(new Main());
                }
            }

            finally
            {
                //TODO: Save any app.config changes
            }
        }

        /// <summary>
        /// Default settings used whenever the app.config cannot be read or the user resets to defaults
        /// </summary>
        public static void DefaultSettings()
        {
            advanced = false;
            autoUpdate = true;
            debugMode = false;
            disableRobocopyCheck = false;
            forceAction = false; ;
            logging = false;
            logTo = "";
            multiThreading = true;
            output = false;
            threadCount = 8;
            completionAction = 0;
            alwaysOnTop = false;
            disableProgressMonitoring = false;
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>
        /// Reads in the variables from the app.config file and assigns them to global variables
        /// </summary>
        private static void InitializeResources()
        {
            try
            {
                // Read in the application settings and set global variables for use elsewhere
                advanced = Properties.Settings.Default.disableSafety;
                autoUpdate = Properties.Settings.Default.autoUpdate;
                debugMode = Properties.Settings.Default.debugMode;
                disableRobocopyCheck = Properties.Settings.Default.disableRobocopyCheck;
                forceAction = Properties.Settings.Default.forceAction;
                logging = Properties.Settings.Default.logging;
                logTo = Properties.Settings.Default.logTo;
                multiThreading = Properties.Settings.Default.multiThreading;
                output = Properties.Settings.Default.showOutput;
                threadCount = Properties.Settings.Default.threadCount;
                completionAction = Properties.Settings.Default.completionAction;
                alwaysOnTop = Properties.Settings.Default.alwaysOnTop;
                disableProgressMonitoring = Properties.Settings.Default.disableProgressMonitoring;
            }
            catch (Exception)
            {
                // If something happened to the app.config file, assume defaults
                DefaultSettings();
            }
        }

        #endregion
    }
}
