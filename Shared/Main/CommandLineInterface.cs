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
        private static bool batch = false;              // If this is a batch job
        private static bool quiet = false;              // Quiet mode - no output
        private static bool logging = false;            // Logging enabled
        private static bool multithreading = true;      // Multithreading enabled
        private static bool forceAction = false;        // Force completion action

        //Strings
        private static string jobFile = string.Empty;   // Job file
        private static string logTo = string.Empty;     // Logfile
        private static string mtOptions = string.Empty; // Multithreading options
        private static string acOptions = string.Empty; // Completion action options
        private static string command = string.Empty;   // Custom command

        //Other
        private static int numThreads = 8;              // Number of threads
        private static int completionAction = 0;        // Completion action
        private static int error = 0;					// Error tracking

        #endregion

        // ------------------------------------------------------------------------------------

        #region Public methods

        /// <summary>
        /// Runs the main deletion operation
        /// </summary>
        /// <param name="cla"></param>
        public static void Run(string path, CommandLineArgs cla)
        {
            // First, parse the command line arguments and assign them to varibles
            ParseArgs(cla);

            // If we aren't running a batch job...
            if (!batch)
            {
                Target target = new Target(path);
                // If the target exists
                if (target.Exists)
                {
                    // Try deleting the target
                    try
                    {
                        DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(multithreading, numThreads);
                        DeletionOps.Logging log = new DeletionOps.Logging(logging, logTo);
                        error = DeletionOps.Delete(target, mt, log, !quiet);
                    }
                    catch (Exception ex)
                    {
                        // If the user specifies quiet mode, don't display exception
                        if (!quiet)
                            Console.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        // If it was a success and no command is specified, run the completion action
                        if (error == 0 && string.IsNullOrEmpty(command))
                            CompletionActions.RunCompletionEvent((CompletionActions.Actions)completionAction, false, forceAction);
                        // Otherwise, run the custom command
                        else if (error == 0 && !string.IsNullOrEmpty(command))
                            RunCommand();
                    }
                }
                // If the target doesn't exist and we're not in quiet mode, write the error
                else if (!target.Exists && !quiet)
                    Console.WriteLine(SharedResources.Properties.Resources.TargetNotFound);
            }
            else
            {
				// If the user passed a job file and it exists, execute it
                if (File.Exists(jobFile))
                    RunJobDeletion();
				// Otherwise display an error
                else
                    if (!quiet) Console.WriteLine(SharedResources.Properties.Resources.JobFileNotFound);
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
            StreamReader din = File.OpenText(jobFile);
            string str = string.Empty;
            Target target = new Target();
            DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(multithreading, numThreads);
            DeletionOps.Logging log = new DeletionOps.Logging(logging, logTo);

			// While there are still more lines to read and no errors have been encountered
            while (!string.IsNullOrEmpty((str = din.ReadLine())) && error == 0)
            {
				// Set the target's path based on the line
                target.path = str;
				// Run the deletion
                error = DeletionOps.Delete(target, mt, log, !quiet);
            }
        }

        /// <summary>
        /// Parses the CommandLineArgs and assigns their arguments to variables
        /// </summary>
        /// <param name="cla">CommandLineArgs</param>
        private static void ParseArgs(CommandLineArgs cla)
        {
            // Job file
            batch = cla.HasFlag("job");
            if (batch)
                cla.GetFlagAndArguments("job", ref jobFile);

            // Quiet mode
            quiet = cla.HasFlag("q");

            // Logging
            logging = cla.HasFlag("l");
            if (logging)
                cla.GetFlagAndArguments("l", ref logTo);

            // Multithreading
            if (cla.HasFlag("mt"))
            {
                cla.GetFlagAndArguments("mt", ref mtOptions);

                if (int.TryParse(mtOptions, out numThreads))
                {
                    if (numThreads > 0)
                        multithreading = true;
                    else
                        multithreading = false;
                    if (numThreads > 128)
                        numThreads = 8;
                }
                else if (string.Equals(mtOptions, "off"))
                    multithreading = false;
            }

            // Completion action
            if (cla.HasFlag("end"))
            {
                // Grab the arguments passed with the flag
                cla.GetFlagAndArguments("end", ref acOptions);
                // If the user entered a number, they were trying to choose a predetermined completion action
                if (int.TryParse(acOptions, out completionAction))
                {
                    // If they didn't choose properly, select the default
                    if (completionAction != 0 && completionAction != 2 && completionAction != 3)
                        completionAction = 0;
                }
                else
                {
                    // Otherwise, accept the string they passed as a valid command which will run at completion
                    command = acOptions;
                }

                //Check for the force flag (we only care about this if they have specified a completion action)
                forceAction = cla.HasFlag("f");
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
                Process.Start("CMD.exe", command);
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
