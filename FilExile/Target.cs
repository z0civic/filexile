using System;
using System.IO;

namespace FilExile
{
    public class Target
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

        #endregion

        // ------------------------------------------------------------------------------------

    }
}
