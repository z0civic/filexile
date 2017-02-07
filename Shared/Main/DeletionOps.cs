using System;
using System.Diagnostics;
using System.IO;

namespace Shared
{
    internal static class DeletionOps
    {
        /// <summary>Class for handling deletion operations within FilExile whether from the GUI or CLI.</summary>
        // ------------------------------------------------------------------------------------

        #region Structs

        /// <summary>Struct for holding multithreading configuration information (enabled, numThreads).</summary>
        public struct MultithreadingSetup
        {
            public readonly bool ThreadingEnabled;
            public decimal NumThreads;

            public MultithreadingSetup(bool threadingEnabled, decimal numThreads)
            {
                ThreadingEnabled = threadingEnabled;
                NumThreads = numThreads;
            }
        }
		
        /// <summary>Struct for holding logging configuration information (enabled, logfile). </summary>
        public struct Logging
        {
            public readonly bool Enabled;
            public readonly string LogTo;

            public Logging(bool enabled, string logTo)
            {
                Enabled = enabled;
                LogTo = logTo;
            }
        }

		#endregion

		// ------------------------------------------------------------------------------------

		#region Fields

		// Windows %TEMP% system variable
		private static readonly string SystemTempDir = Environment.GetEnvironmentVariable("TEMP");           
        private static string _robocopyCommand;

		#endregion

		// ------------------------------------------------------------------------------------

		#region Public methods

		/// <summary>Handles the deletion of the passed target</summary>
		/// <param name="target">The file or directory to be deleted</param>
		/// <param name="mt">Multithreading configuration</param>
		/// <param name="log">Logging configuration</param>
		/// <param name="output">Output enabled</param>
		/// <returns>An error code based on how the operation turned out</returns>
		public static int Delete(Target target, MultithreadingSetup mt, Logging log, bool output)
        {
            int retval = (int) ErrorCodes.Success;

            if (target.Exists)
            {
                // Create empty directory for mirroring
                string emptyDir = SystemTempDir + @"\FilExile_temp$";
	            string secondEmptyDir = "";
                Directory.CreateDirectory(emptyDir);

                // Sometimes there's an extra backslash on the end of the path
                // and we need to trim it off
                if (target.Path.EndsWith(@"\"))
                    target.Path = target.Path.TrimEnd('\\');

                if (target.IsDirectory)
                {
                    _robocopyCommand = PrepareRobocopyCommand(mt, log, emptyDir, target.Path);
                }
                else
                {
                    // For single files there is an extra step we have to take...
                    // We need to create another temporary directory. We are going to use Robocopy
                    // to place the file in this temporary directory by itself. This is to
                    // prevent the actual diretory mirror command from wiping out everything else
                    // where the file was found.
                    secondEmptyDir = SystemTempDir + @"\FilExile_singleFile_temp$";
                    Directory.CreateDirectory(secondEmptyDir);

                    string fileCopyCmd = PrepareFileCopyCommand(target.ParentDirectory, secondEmptyDir, target.FileName);
                    RunRobocopy(fileCopyCmd, output);

                    _robocopyCommand = PrepareRobocopyCommand(mt, log, emptyDir, secondEmptyDir);
                }

                // This is where the main deletion operation occurs - Uses Robocopy to mirror
                // the empty directory we created onto the target. This will make any files within
                // the target appear as "extra files" and forcibly remove them via Robocopy which
                // can handle all sorts of nasty files that Windows will sometimes choke on
                RunRobocopy(_robocopyCommand, output);

				// Delete the temporary directory/directories created
	            if (Directory.Exists(emptyDir))
	            {
		            Directory.Delete(emptyDir, true);
	            }
				if (Directory.Exists(secondEmptyDir))
				{
		            Directory.Delete(secondEmptyDir, true);
	            }
            }
            else
            {
                retval = (int) ErrorCodes.NotFound;
            }

            return retval;
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>
        /// Takes the two passed directories and creates Robocopy arguments to mirror the
        /// empty directory onto the target diretory
        /// </summary>
        /// <param name="mt">Multithreading struct (enabled, numThreads)</param>
        /// <param name="log">Logging struct (enabled, logfile)</param>
        /// <param name="emptyDir">Empty directory to mirror from</param>
        /// <param name="targetDir">Targeted directory to be eliminated</param>
        /// <returns>Prepared Robocopy arguments to mirror the emptyDir onto the targetDir</returns>
        private static string PrepareRobocopyCommand(MultithreadingSetup mt, Logging log, string emptyDir, string targetDir)
        {
            string retVal = string.Empty;

            retVal += "\"" + emptyDir + "\" \"" + targetDir + "\" /MIR /NJH /NP";

            if (mt.ThreadingEnabled)
            {
                if (mt.NumThreads < 1 || mt.NumThreads > 128)
                    mt.NumThreads = 8;
                retVal += " /MT:" + mt.NumThreads;
            }

            if (!string.IsNullOrEmpty(log.LogTo) && log.Enabled)
            {
                retVal += @" /TEE /V /TS /LOG+:" + log.LogTo + "\"";
            }

            return retVal;
        }

        /// <summary>Prepares Robocopy arguments to forcibly move filename from the source to the destination.</summary>
        /// <param name="source">Source path</param>
        /// <param name="destination">Destination path</param>
        /// <param name="filename">Name of file to move</param>
        /// <returns>Prepared Robocopy arguments to move a file from the source to the destination</returns>
        private static string PrepareFileCopyCommand(string source, string destination, string filename)
        {
            var retval = "\"" + source + "\" \"" + destination + "\" /MOV /NJH /NP /IF \"" + filename + "\"";
            return retval;
        }

	    /// <summary>Creates a new Robocopy process and runs the passed arguements.</summary>
	    /// <param name="arguments">Command line arguments to pass to the process</param>
	    /// <param name="output">Whether or not to display output</param>
	    private static void RunRobocopy(string arguments, bool output)
        {
            try
            {
	            var p = new Process
	            {
		            StartInfo =
		            {
			            FileName = "robocopy.exe",
			            UseShellExecute = false,
			            RedirectStandardOutput = true
		            }
	            };
	            if (output)
                    p.OutputDataReceived += OutputHandler;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = arguments;

                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
                p.Close();
            }
            catch
            {
	            // ignored
            }
        }

        /// <summary>Redirects the standard output to the allocated console for viewing.</summary>
        /// <param name="sender"></param>
        /// <param name="outLine"></param>
        private static void OutputHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                Console.WriteLine(outLine.Data);
            }
        }

        #endregion

    }
}
