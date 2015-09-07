using System;
using System.Diagnostics;
using System.IO;

namespace Shared
{
    internal sealed class DeletionOps
    {
        /// <summary>
        /// Class for handling deletion operations within FilExile whether from the GUI or CLI
        /// </summary>
        // ------------------------------------------------------------------------------------

        #region Structs

        /// <summary>
        /// Struct for holding multithreading configuration information (enabled, numThreads)
        /// </summary>
        public struct MultithreadingSetup
        {
            public bool threadingEnabled;
            public decimal numThreads;

            public MultithreadingSetup(bool threadingEnabled, decimal numThreads)
            {
                this.threadingEnabled = threadingEnabled;
                this.numThreads = numThreads;
            }
        }

        /// <summary>
        /// Struct for holding logging configuration information (enabled, logfile)
        /// </summary>
        public struct Logging
        {
            public bool enabled;
            public string logTo;

            public Logging(bool enabled, string logTo)
            {
                this.enabled = enabled;
                this.logTo = logTo;
            }
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Fields

        private static string systemTempDir = Environment.GetEnvironmentVariable("temp");           //Windows %TEMP% system variable
        private static string systemDrive = Environment.GetEnvironmentVariable("systemdrive");      //Windows %SYSTEMDRIVE% system variable
        private static string robocopyCommand = string.Empty;

        #endregion

        // ------------------------------------------------------------------------------------

        #region Public methods

        /// <summary>
        /// Handles the deletion of the passed target
        /// </summary>
        /// <param name="target">The file or directory to be deleted</param>
        /// <returns>An error code based on how the operation turned out</returns>
        public static int Delete(Target target, MultithreadingSetup mt, Logging log, bool output)
        {
            int retval = (int) ErrorCodes.SUCCESS;

            if (target.Exists)
            {
                // Create empty directory for mirroring
                string emptyDir = systemTempDir + @"\FilExile_temp$";
                Directory.CreateDirectory(emptyDir);

                // Sometimes there's an extra backslash on the end of the path
                // and we need to trim it off
                if (target.path.EndsWith(@"\"))
                    target.path = target.path.TrimEnd('\\');

                if (target.IsDirectory)
                {
                    robocopyCommand = PrepareRobocopyCommand(mt, log, emptyDir, target.path);
                }
                else
                {
                    // For files there is an extra step we have to take...
                    // We need to create another temporary directory. We are going to use Robocopy
                    // to place the file in this temporary directory by itself. This is to
                    // prevent the actual diretory mirror command from wiping out everything else
                    // where the file was found.
                    string secondEmptyDir = systemTempDir + @"\FilExile_singleFile_temp$";
                    Directory.CreateDirectory(secondEmptyDir);

                    string fileCopyCmd = PrepareFileCopyCommand(target.ParentDirectory, secondEmptyDir, target.FileName);
                    RunRobocopy(fileCopyCmd, output);

                    robocopyCommand = PrepareRobocopyCommand(mt, log, emptyDir, secondEmptyDir);
                }

                // This is where the main deletion operation occurs - Uses Robocopy to mirror
                // the empty directory we created onto the target. This will make any files within
                // the target appear as "extra files" and forcibly remove them via Robocopy which
                // can handle all sorts of nasty files that Windows will sometimes choke on
                RunRobocopy(robocopyCommand, output);
            }
            else
            {
                retval = (int) ErrorCodes.NOT_FOUND;
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

            if (mt.threadingEnabled)
            {
                if (mt.numThreads < 1 || mt.numThreads > 128)
                    mt.numThreads = 8;
                retVal += " /MT:" + mt.numThreads;
            }

            if (!string.IsNullOrEmpty(log.logTo) && log.enabled)
            {
                retVal += @" /TEE /V /LOG+:" + log.logTo + "\"";
            }

            return retVal;
        }

        /// <summary>
        /// Prepares Robocopy arguments to forcibly move filename from the source to the destination
        /// </summary>
        /// <param name="source">Source path</param>
        /// <param name="destination">Destination path</param>
        /// <param name="filename">Name of file to move</param>
        /// <returns>Prepared Robocopy arguments to move a file from the source to the destination</returns>
        private static string PrepareFileCopyCommand(string source, string destination, string filename)
        {
            string retval = "\"" + source + "\" \"" + destination + "\" /MOV /NJH /NP /IF \"" + filename + "\"";
            return retval;
        }

        /// <summary>
        /// Creates a new Robocopy process and runs the passed arguements
        /// </summary>
        /// <param name="arguments">Command line arguments to pass to the process</param>
        /// <param name="output">Whether or not to display output</param>
        private static bool RunRobocopy(string arguments, bool output)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "robocopy.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                if (output)
                    p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = arguments;

                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
                p.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Redirects the standard output to the allocated console for viewing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="outLine"></param>
        private static void OutputHandler(Object sender, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                Console.WriteLine(outLine.Data);
            }
        }

        #endregion

    }
}
