using System;
using System.Diagnostics;
using System.IO;

namespace Shared
{
	/// <summary>Class for running FilExile operations from the CLI rather than the GUI</summary>
	internal static class CommandLineInterface
    {
        // ------------------------------------------------------------------------------------

        #region Fields

        //Booleans
        private static bool _batch;						 // If this is a batch job
        private static bool _quiet;						 // Quiet mode - no output
        private static bool _logging;					 // Logging enabled
        private static bool _multithreading = true;		 // Multithreading enabled
        private static bool _forceAction;				 // Force completion action

        //Strings
        private static string _jobFile;					 // Job file
        private static string _logTo;					 // Logfile
        private static string _mtOptions;				 // Multithreading options
        private static string _command;					 // Completion command

        //Other
        private static int _numThreads = 8;              // Number of threads
        private static int _error;						 // Error tracking

		private static CompletionAction _ca;			 // Completion action

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
			_ca = new CompletionAction();

            // First, parse the command line arguments and assign them to varibles
            ParseArgs(cla);

            // If we aren't running a batch job...
            if (!_batch)
            {
                Target target = new Target(path);

                // If the target exists
                if (target.Exists)
                {
                    // Try deleting the target
                    try
                    {
                        DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(_multithreading, _numThreads);
                        DeletionOps.Logging log = new DeletionOps.Logging(_logging, _logTo);
                        _error = DeletionOps.Delete(target, mt, log, !_quiet);
                    }
                    catch (Exception ex)
                    {
                        // If the user specifies quiet mode, don't display exception
                        if (!_quiet)
                            Console.WriteLine(ex.ToString());
						_error = 1;
                    }
                    finally
                    {
						// If it was a success
	                    if (_error == 0)
	                    {
							CleanupDirectory(target);

							// No command is specified, run the completion action
							if (string.IsNullOrEmpty(_command))
								_ca.Run(false, _forceAction);
							// Otherwise, run the custom command
							else if (!string.IsNullOrEmpty(_command))
								RunCommand();
						}
                    }
                }
                // If the target doesn't exist and we're not in quiet mode, write the error
                else if (!target.Exists && !_quiet)
                    Console.WriteLine(SharedResources.Properties.Resources.TargetNotFound);
            }
            else
            {
				// If the user passed a job file and it exists, execute it
                if (File.Exists(_jobFile))
                    RunJobDeletion();
				// Otherwise display an error
                else
                    if (!_quiet) Console.WriteLine(SharedResources.Properties.Resources.JobFileNotFound);
            }
        }

        /// <summary>Displays the command line usage information.</summary>
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
            StreamReader din = File.OpenText(_jobFile);
            string str;
            Target target = new Target();
            DeletionOps.MultithreadingSetup mt = new DeletionOps.MultithreadingSetup(_multithreading, _numThreads);
            DeletionOps.Logging log = new DeletionOps.Logging(_logging, _logTo);

			// While there are still more lines to read and no errors have been encountered
            while (!string.IsNullOrEmpty((str = din.ReadLine())) && _error == 0)
            {
				// Set the target's path based on the line
                target.Path = str;
				// Run the deletion
                _error = DeletionOps.Delete(target, mt, log, !_quiet);
            }
        }

		/// <summary>Deletes the original target directory (if it's still there).</summary>
		/// <param name="target">The original <see cref="Target"/></param>
	    private static void CleanupDirectory(Target target)
	    {
			if (target.IsDirectory && target.Exists)
			{
				Directory.Delete(target.Path);
			}
		}

        /// <summary>Parses the CommandLineArgs and assigns their arguments to variables.</summary>
        /// <param name="cla">CommandLineArgs</param>
        private static void ParseArgs(CommandLineArgs cla)
        {
            // Job file
            _batch = cla.HasFlag("job");
            if (_batch)
                cla.GetFlagAndArguments("job", ref _jobFile);

            // Quiet mode
            _quiet = cla.HasFlag("q");

            // Logging
            _logging = cla.HasFlag("l");
            if (_logging)
                cla.GetFlagAndArguments("l", ref _logTo);

            // Multithreading
            if (cla.HasFlag("mt"))
            {
                cla.GetFlagAndArguments("mt", ref _mtOptions);

                if (int.TryParse(_mtOptions, out _numThreads))
                {
	                _multithreading = _numThreads > 0;
	                if (_numThreads > 128)
                        _numThreads = 8;
                }
                else if (string.Equals(_mtOptions, "off"))
                    _multithreading = false;
            }

            // Completion action
	        if (!cla.HasFlag("end")) return;
	        int iCompletionAction;
	        // Grab the arguments passed with the flag
	        cla.GetFlagAndArguments("end", ref _command);
	        // If the user entered a number, they were trying to choose a predetermined completion action
	        if (int.TryParse(_command, out iCompletionAction))
	        {
		        // If they didn't choose properly, select the default
		        _ca.Value = !_ca.IsValidOptionForCli(iCompletionAction) ? 0 : iCompletionAction;
	        }

	        //Check for the force flag (we only care about this if they have specified a completion action)
	        _forceAction = cla.HasFlag("f");
        }

        /// <summary>Attempts to run the custom command specified by the user.</summary>
        private static void RunCommand()
        {
            // Try to run the user's custom command
            try
            {
                Process.Start(_command);
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
