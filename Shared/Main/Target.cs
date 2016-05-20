using System;
using System.Collections.Generic;
using System.IO;

namespace Shared
{
    internal sealed class Target
    {
        /// <summary>
        /// Class for hodling the "target" to be deleted. 
        /// </summary>
        // ------------------------------------------------------------------------------------

        #region Fields

        public string Path = string.Empty;
        // Directories considered system critical and probably shouldn't be deleted without careful thought
        private readonly string[] _criticalDirectories = { "*:\\", "*:\\Users", "*:\\Windows\\*", "*:\\Program*", 
                                                 "*:\\Users\\*\\AppData", "*:\\ProgramData", "*:\\Windows", "*:\\Documents" };

        #endregion

        // ------------------------------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Default contructor
        /// </summary>
        public Target()
        {
        }

        /// <summary>
        /// Constructor with full-qualified path
        /// </summary>
        /// <param name="path">String to the fully-qualified path</param>
        public Target(string path)
        {
            Path = path;
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Properties

        /// <summary> 
        /// If the Target is a directory. 
        /// </summary>
        public bool IsDirectory
        {
            get
            {
                try
                {
                    bool retVal = ((File.GetAttributes(Path) & FileAttributes.Directory) == FileAttributes.Directory);
                    return retVal;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary> 
        /// If the Target is a critical to system operation and deletion could be undesirable.
        /// </summary>
        public bool IsCritical
        {
            get
            {
                var retval = false;

                foreach (var pattern in _criticalDirectories)
                {
                    if (StringUtils.IsLike(Path, pattern))
                    {
                        retval = true;
                    }
                }

                return retval;
            }
        }

        /// <summary>
        /// If the Target exists, regardless of whether or not the Target is a directory or file
        /// </summary>
        public bool Exists => IsDirectory ? Directory.Exists(Path) : File.Exists(Path);

	    /// <summary>
        /// Returns the number of files in a directory
        /// </summary>
        public int NumberOfFiles => GetFiles(Path).Count;

	    /// <summary>
        /// Returns the parent directory of the path
        /// </summary>
        public string ParentDirectory
        {
            get
            {
	            var di = new DirectoryInfo(Path);
				var retVal = di.FullName;

				var end = retVal.LastIndexOf(@"\", StringComparison.Ordinal);
	            if (end <= 0) return retVal;
	            retVal = retVal.Substring(0, end);
	            if (retVal.EndsWith(@":"))
		            retVal += @"\";

	            return retVal;
            }
        }

        /// <summary>
        /// Returns the file name of the path
        /// </summary>
        public string FileName
        {
            get
            {
                var end = Path.LastIndexOf(@"\", StringComparison.Ordinal);
                return Path.Substring(end + 1);
            }
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>
        /// Recursively returns a collection of files from a path. This is used for counting
		/// the number of files in a directory for progress operations
        /// </summary>
        /// <param name="currentPath">The path to search from</param>
        /// <returns>A strongly-typed collection of files (as string paths)</returns>
        private static List<string> GetFiles(string currentPath)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly));
                foreach (var directory in Directory.GetDirectories(currentPath))
                    files.AddRange(GetFiles(directory));
            }
            // If an error occurs during counting, we don't especially care as it's just for
            // displaying deletion progress - wouldn't want to crash or throw exception
            catch
            {
	            // ignored
            }

	        return files;
        }

        #endregion

    }
}