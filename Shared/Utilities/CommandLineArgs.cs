using System.Text;

namespace Shared
{
    internal sealed class CommandLineArgs
    {
        /// <summary>
        /// Class for handling command line arguments
        /// </summary>
        /// <remarks>
        /// The following arguments are supported directly:
        /// 
        /// /?     - Help (on command-line arguments)
        /// /l     - Enables verbose logging
        /// /q     - Enables quiet mode (no prompts)
        /// /job   - Handles job files (multiple deletion operations)
        /// /debug - Enables debugging
        /// </remarks>
        // ------------------------------------------------------------------------------------

        #region Fields

        private readonly string[] _args;

        #endregion

        // ------------------------------------------------------------------------------------
        
        #region Contruction/Finalization

        /// <summary>Constructor</summary>
        /// <param name="args">Command line arguments</param>
        public CommandLineArgs(string[] args)
        {
			_args = args;
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Properties

        public bool HelpFlag => HasFlag("?");

	    #endregion

        // ------------------------------------------------------------------------------------

        #region Public methods

        public override string ToString()
        {
            //Check if we got any command line arguments
            if ((_args == null) || _args.Length == 0)
                return "Not set";

            var result = new StringBuilder();

            foreach (var arg in _args)
            {
                result.Append(arg);
                result.Append(" ");
            }

            return result.ToString();
        }

        /// <summary>Returns true if the particular flag is set.</summary>
        /// <param name="flag">The flag to check (ex: ? for /? or -?)</param>
        /// <returns>True if the flag is set</returns>
        public bool HasFlag(string flag)
        {
            return (IndexOfFlag(flag) != -1);
        }

	    /// <summary>Gets whether a flag is set and the argument value that follows</summary>
	    /// <param name="flag">The flag to check</param>
	    /// <param name="argument1">The argument following the flag (if any)</param>
	    /// <returns>True if the flag is set</returns>
	    public void GetFlagAndArguments(string flag, ref string argument1)
        {
            var arg2 = "";
            var arg3 = "";
		    GetFlagAndArguments(flag, ref argument1, ref arg2, ref arg3);
        }

	    /// <summary>Gets whether a flag is set and the argument values that follow</summary>
	    /// <param name="flag">The flag to check</param>
	    /// <param name="argument1">The argument following the flag (if any)</param>
	    /// <param name="argument2">The second argument following the flag (if any)</param>
	    /// <param name="argument3">The third argument following the flag (if any)</param>
	    /// <returns>True if the flag is set</returns>
	    private void GetFlagAndArguments(string flag, ref string argument1, ref string argument2, ref string argument3)
        {
		    // Gets arguments until runs out or hits another flag
            var pos = IndexOfFlag(flag);
		    if (pos < 0) return;
		    pos++;
		    if ((pos >= _args.Length) || IsAFlag(_args[pos])) return;
		    argument1 = _args[pos].Trim();
		    pos++;
		    if ((pos >= _args.Length) || IsAFlag(_args[pos])) return;
		    argument2 = _args[pos].Trim();
		    pos++;
		    if ((pos < _args.Length) && !IsAFlag(_args[pos]))
		    {
			    argument3 = _args[pos].Trim();
		    }
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Protected methods

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>Returns the index of the specified flag (or -1 if not found)</summary>
        /// <param name="flag">Flag to find</param>
        private int IndexOfFlag(string flag)
        {
            var retval = -1;

            // If caller put in / or - in front of flag, remove it
            flag = flag.Trim();
            if ((flag.Length > 1) && ((flag[0] == '/') || (flag[0] == '-')))
                flag = flag.Substring(1);

	        if (_args == null) return retval;
	        for (var i = 0; i < _args.Length; i++)
	        {
		        if (!IsFlag(_args[i], flag)) continue;
		        retval = i;
		        break;
	        }

	        return retval;
        }

        /// <summary>Returns true if the passed flag has the passed value</summary>
        /// <param name="flagToCheck">The flag to check for</param>
        /// <param name="value">The value to check for</param>
        private static bool IsFlag(string flagToCheck, string value)
        {
            var retval = false;

            // Must be the length of the flag plus one for the / or -
            if (flagToCheck.Trim().Length == (value.Length + 1))
            {
                retval = (((flagToCheck[0] == '/') || (flagToCheck[0] == '-')) &&
                    StringUtils.CompareRight(flagToCheck, value, true));
            }

            return retval;
        }

        /// <summary>Checks to see if a passed value is a flag (starts with / or - )</summary>
        /// <param name="flag">Flag to check</param>
        private static bool IsAFlag(string flag)
        {
            return ((flag.Length > 0) && ((flag[0] == '/') || (flag[0] == '-')));
        }

        #endregion
    }
}
