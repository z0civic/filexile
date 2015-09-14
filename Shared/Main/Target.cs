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

        public string path = string.Empty;
        private string[] criticalDirectories = { "*:\\", "*:\\Users", "*:\\Windows\\*", "*:\\Program*", 
                                                 "*:\\Users\\*\\AppData", "*:\\ProgramData", "*:\\Windows", "*:\\Documents" };

        #endregion

        // ------------------------------------------------------------------------------------

        #region Constructors

        public Target()
        {
        }

        public Target(string path)
        {
            this.path = path;
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
                    bool retVal = ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory);
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
                bool retval = false;

                foreach (string pattern in criticalDirectories)
                {
                    if (StringUtils.IsLike(this.path, pattern))
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
        public bool Exists
        {
            get
            {
                bool retval = false;

                if (this.IsDirectory)
                {
                    retval = Directory.Exists(path);
                }
                else
                {
                    retval = File.Exists(path);
                }

                return retval;
            }
        }

        /// <summary>
        /// Returns the number of files in a directory
        /// </summary>
        public int NumberOfFiles
        {
            get
            {
                return GetFiles(path).Count;
            }
        }

        /// <summary>
        /// Returns the parent directory of the path
        /// </summary>
        public string ParentDirectory
        {
            get
            {
				string retVal = "";
				DirectoryInfo di = new DirectoryInfo(path);
				retVal = di.FullName;

				int end = retVal.LastIndexOf(@"\");
				if (end > 0)
				{
					retVal = retVal.Substring(0, end);
					if (retVal.EndsWith(@":"))
						retVal += @"\";
				}

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
                int end = path.LastIndexOf(@"\");
                return path.Substring(end + 1);
            }
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>
        /// Recursively returns a collection of files from a path
        /// </summary>
        /// <param name="currentPath">The path to search from</param>
        /// <returns>A collection of files</returns>
        private List<string> GetFiles(string currentPath)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly));
                foreach (var directory in Directory.GetDirectories(currentPath))
                    files.AddRange(GetFiles(directory));
            }
            catch { }

            return files;
        }

        #endregion

    }
}