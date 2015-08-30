using System;
using System.IO;

namespace FilExile
{
    public class DeletionOps
    {
        /// <summary>
        /// Class for handling deletion operations within FilExile whether from the GUI or CLI
        /// </summary>
        // ------------------------------------------------------------------------------------

        #region Structs

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

        #endregion

        // ------------------------------------------------------------------------------------

        #region Fields

        private static string systemTempDir = Environment.GetEnvironmentVariable("temp");
        private static string systemDrive = Environment.GetEnvironmentVariable("systemdrive");
        private static string emptyDir = string.Empty;
        private static string robocopyCommand = string.Empty;

        #endregion

        // ------------------------------------------------------------------------------------

        #region Public methods

        /// <summary>
        /// Handles the deletion of the passed target
        /// </summary>
        /// <param name="target">The file or directory to be deleted</param>
        /// <returns>An error code based on how the operation turned out</returns>
        public static int Delete(Target target, MultithreadingSetup mt, bool logging, string logTo)
        {
            int retval = (int)Program.ErrorCodes.SUCCESS;

            if (target.Exists)
            {
                // Create empty directory for mirroring
                emptyDir = systemTempDir + "\\FilExile_temp$";
                Directory.CreateDirectory(emptyDir);

                // Sometimes there's an extra backslash on the end of the path
                // and we need to trim it off
                if (target.path.EndsWith("\\"))
                    target.path = target.path.TrimEnd('\\');

                if (target.IsDirectory)
                {
                    robocopyCommand = PrepareRobocopyCommand(mt, logging, logTo, target);
                }
                else
                {

                }
            }
            else
            {
                retval = (int)Program.ErrorCodes.NOT_FOUND;
            }

            return retval;
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Protected methods

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>
        /// Based on the parameters, returns a properly formatted Robocopy command string that
        /// will be used to remove the "extra" files/directories
        /// </summary>
        /// <returns>The formatted command string</returns>
        private static string PrepareRobocopyCommand(MultithreadingSetup mt, bool logging, string logfile, Target target)
        {
            string retVal = string.Empty;

            retVal += "\"" + emptyDir + "\" \"" + target.path + "\" /MIR /NJH /NP";

            if (mt.threadingEnabled)
            {
                if (mt.numThreads < 1 || mt.numThreads > 128)
                    mt.numThreads = 8;
                retVal += " /MT:" + mt.numThreads;
            }

            if (!string.IsNullOrEmpty(logfile) && logging)
            {
                retVal += " /TEE /V /LOG+:\"" + logfile + "\"";
            }

            return retVal;
        }

        #endregion

    }
}
