using System;
using System.Diagnostics;
using System.IO;

namespace Shared
{
    internal sealed class CommandLineInterface
    {
        /// <summary>Class for running FilExile operations from the CLI rather than the GUI</summary>
        // ------------------------------------------------------------------------------------

        #region Fields

        //Booleans
        private static bool bBatch = false;              // If this is a batch job
        private static bool bQuiet = false;              // Quiet mode - no output
        private static bool bLogging = false;            // Logging enabled
        private static bool bMultithreading = true;      // Multithreading enabled
        private static bool bForceAction = false;        // Force completion action

        //Strings
        private static string strJobFile = string.Empty;   // Job file
        private static string strLogTo = string.Empty;     // Logfile
        private static string strMTOptions = string.Empty; // Multithreading options
        private static string strCommand = string.Empty;   // Completion command

        //Other
        private static int intNumThreads = 8;              // Number of threads
        private static int error = 0;                   // Error tracking

		private static CompletionAction ca;				// Completion action

        #endregion

        // ------------------------------------------------------------------------------------

        #region Public methods

        /// <summary>
		/// Parses the passed command line arguments, handles if a batch job was specified,
		/// runs the deletion operation. Main entry point for the class.
		/// </summary>
		/// <param name="path">0th argument passed in (file or folder to delete)</param>
		/// <param name="cla">The structured command line arguments</param>
        public static void Run(string path, CommandLineArgs cla)
        {
			ca = new CompletionAction();

            // First, parse the command line arguments and assign them to varibles
            ParseArgs(cla);

            // If we aren't running a batch job...
            if (!bBatch)
            {
                Target target = new Target(path);

                // If the target exists
                if (target.Exists)
                {
                    // Try deleting the target
                    try
                    {
                        DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(bMultithreading, intNumThreads);
                        DeletionOps.Logging log = new DeletionOps.Logging(bLogging, strLogTo);
                        error = DeletionOps.Delete(target, mt, log, !bQuiet);
                    }
                    catch (Exception ex)
                    {
                        // If the user specifies quiet mode, don't display exception
                        if (!bQuiet)
                            Console.WriteLine(ex.ToString());
						error = 1;
                    }
                    finally
                    {
                        // If it was a success and no command is specified, run the completion action
                        if (error == 0 && string.IsNullOrEmpty(strCommand))
                            ca.Run(false, bForceAction);
                        // Otherwise, run the custom command
                        else if (error == 0 && !string.IsNullOrEmpty(strCommand))
                            RunCommand();
                    }
                }
                // If the target doesn't exist and we're not in quiet mode, write the error
                else if (!target.Exists && !bQuiet)
                    Console.WriteLine(SharedResources.Properties.Resources.TargetNotFound);
            }
            else
            {
				// If the user passed a job file and it exists, execute it
                if (File.Exists(strJobFile))
                    RunJobDeletion();
				// Otherwise display an error
                else
                    if (!bQuiet) Console.WriteLine(SharedResources.Properties.Resources.JobFileNotFound);
            }
        }

        /// <summary>
        /// Displays the command line usage information
        /// </summary>
        public static void DisplayHelp()
        {
            Console.WriteLine(SharedResources.Properties.Resources.Help);
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>
        /// Creates a StreamReader to parse the job file. Reads in each line as a separate file or directory
		/// to delete. 
        /// </summary>
        private static void RunJobDeletion()
        {
            StreamReader din = File.OpenText(strJobFile);
            string str = string.Empty;
            Target target = new Target();
            DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(bMultithreading, intNumThreads);
            DeletionOps.Logging log = new DeletionOps.Logging(bLogging, strLogTo);

			// While there are still more lines to read and no errors have been encountered
            while (!string.IsNullOrEmpty((str = din.ReadLine())) && error == 0)
            {
				// Set the target's path based on the line
                target.path = str;
				// Run the deletion
                error = DeletionOps.Delete(target, mt, log, !bQuiet);
            }
        }

        /// <summary>
        /// Parses the CommandLineArgs and assigns their arguments to variables
        /// </summary>
        /// <param name="cla">CommandLineArgs</param>
        private static void ParseArgs(CommandLineArgs cla)
        {
            // Job file
            bBatch = cla.HasFlag("job");
            if (bBatch)
                cla.GetFlagAndArguments("job", ref strJobFile);

            // Quiet mode
            bQuiet = cla.HasFlag("q");

            // Logging
            bLogging = cla.HasFlag("l");
            if (bLogging)
                cla.GetFlagAndArguments("l", ref strLogTo);

            // Multithreading
            if (cla.HasFlag("mt"))
            {
                cla.GetFlagAndArguments("mt", ref strMTOptions);

                if (int.TryParse(strMTOptions, out intNumThreads))
                {
                    if (intNumThreads > 0)
                        bMultithreading = true;
                    else
                        bMultithreading = false;
                    if (intNumThreads > 128)
                        intNumThreads = 8;
                }
                else if (string.Equals(strMTOptions, "off"))
                    bMultithreading = false;
            }

            // Completion action
            if (cla.HasFlag("end"))
            {
				int iCompletionAction = 0;
                // Grab the arguments passed with the flag
                cla.GetFlagAndArguments("end", ref strCommand);
                // If the user entered a number, they were trying to choose a predetermined completion action
                if (int.TryParse(strCommand, out iCompletionAction))
                {
					// If they didn't choose properly, select the default
					if (!ca.IsValidOptionForCLI(iCompletionAction))
						ca.Value = 0;
					else
						ca.Value = iCompletionAction;
                }

                //Check for the force flag (we only care about this if they have specified a completion action)
                bForceAction = cla.HasFlag("f");
            }
        }

        /// <summary>
        /// Attempts to run the custom command specified by the user
        /// </summary>
        private static void RunCommand()
        {
            // Try to run the user's custom command
            try
            {
                Process.Start("CMD.exe", strCommand);
            }
            // If it fails, write the exception, regardless of quiet mode
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion
    }
}
