using System;
using System.Collections.Generic;
using System.Text;

namespace FilExile.Utilities
{
    public sealed class CommandLineArgs
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

        private readonly string[] m_args;

        // These flags are standard launch flags.
        // The standard flags are for logging, quiet mode, and job file processing
        private static readonly string[] m_standardLaunchFlags = { "l", "q", "job"};

        #endregion

        // ------------------------------------------------------------------------------------
        
        #region Contruction/Finalization

        /// <summary>Constructor</summary>
        /// <param name="args">Command line arguments</param>
        public CommandLineArgs(string[] args)
        {
            m_args = args;
        }

        /// <summary>Constructor</summary>
        /// <param name="strArgs">Command line arguments as a string</param>
        public CommandLineArgs(string strArgs)
        {
            m_args = strArgs.Split(' ');
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Properties

        public bool HelpFlag
        {
            get { return HasFlag("?"); }
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Public methods

        public override string ToString()
        {
            //Check if we got any command line arguments
            if ((m_args == null) || m_args.Length == 0)
                return "Not set";

            StringBuilder result = new StringBuilder();

            foreach (string arg in m_args)
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

        /// <summary>Gets whether the command-line includes any arguments other than logging, quiet mode,
        ///          jobfile mode, or debugging.
        /// </summary>
        /// <returns>True if any other flags are present</returns>
        public bool HasCommandsOtherThanStandardLaunchCommands()
        {
            bool retval = false;

            if (m_args != null)
            {
                for (int i = 0; i < m_args.Length; i++)
                {
                    // Check all flags, and also always check the first argument
                    if (((i == 0) || IsAFlag(m_args[i])) && !IsStandardLaunchCommand(m_args[i]))
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        /// <summary>Gets whether a flag is set and the argument value that follows</summary>
        /// <param name="flag">The flag to check</param>
        /// <param name="argument1">The argument following the flag (if any)</param>
        /// <returns>True if the flag is set</returns>
        public bool GetFlagAndArguments(string flag, ref string argument1)
        {
            string arg2 = "";
            string arg3 = "";
            return GetFlagAndArguments(flag, ref argument1, ref arg2, ref arg3);
        }

        /// <summary>Determines if a flag is set to a particular value</summary>
        /// <param name="flag">The flag to check</param>
        /// <param name="trueString">The value that indicates "true"</param>
        /// <returns>True if flag is set to the trueString value.</returns>
        public bool GetFlagArgumentAsBool(string flag, string trueString)
        {
            bool retval = false;

            string value = "";
            if (GetFlagAndArguments(flag, ref value))
            {
                retval = (string.Compare(value.Trim(), trueString.Trim(), true) == 0);
            }

            return retval;
        }

        /// <summary>Gets whether a flag is set and the argument values that follow</summary>
        /// <param name="flag">The flag to check</param>
        /// <param name="argument1">The argument following the flag (if any)</param>
        /// <param name="argument2">The second argument following the flag (if any)</param>
        /// <returns>True if the flag is set</returns>
        public bool GetFlagAndArguments(string flag, ref string argument1, ref string argument2)
        {
            string arg3 = "";
            return GetFlagAndArguments(flag, ref argument1, ref argument2, ref arg3);
        }

        /// <summary>Gets whether a flag is set and the argument values that follow</summary>
        /// <param name="flag">The flag to check</param>
        /// <param name="argument1">The argument following the flag (if any)</param>
        /// <param name="argument2">The second argument following the flag (if any)</param>
        /// <param name="argument3">The third argument following the flag (if any)</param>
        /// <returns>True if the flag is set</returns>
        public bool GetFlagAndArguments(string flag, ref string argument1, ref string argument2, ref string argument3)
        {
            bool retval = false;

            // Gets arguments until runs out or hits another flag
            int pos = IndexOfFlag(flag);
            if (pos >= 0)
            {
                retval = true;
                pos++;
                if ((pos < m_args.Length) && !IsAFlag(m_args[pos]))
                {
                    argument1 = m_args[pos].Trim();
                    pos++;
                    if ((pos < m_args.Length) && !IsAFlag(m_args[pos]))
                    {
                        argument2 = m_args[pos].Trim();
                        pos++;
                        if ((pos < m_args.Length) && !IsAFlag(m_args[pos]))
                        {
                            argument3 = m_args[pos].Trim();
                        }
                    }
                }
            }

            return retval;
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
            int retval = -1;

            // If caller put in / or - in front of flag, remove it
            flag = flag.Trim();
            if ((flag.Length > 1) && ((flag[0] == '/') || (flag[0] == '-')))
                flag = flag.Substring(1);

            if (m_args != null)
            {
                for (int i = 0; i < m_args.Length; i++)
                {
                    if (IsFlag(m_args[i], flag))
                    {
                        retval = i;
                        break;
                    }
                }
            }

            return retval;
        }

        /// <summary>Returns true if the passed flag has the passed value</summary>
        /// <param name="flagToCheck">The flag to check for</param>
        /// <param name="value">The value to check for</param>
        private bool IsFlag(string flagToCheck, string value)
        {
            bool retval = false;

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
        private bool IsAFlag(string flag)
        {
            bool retval = ((flag.Length > 0) && ((flag[0] == '/') || (flag[0] == '-')));

            return retval;
        }

        /// <summary>Checks to see if the passed argument is one of the standard launch commands</summary>
        /// <param name="argument">Argument to check</param>
        /// <returns>True if in the standard list</returns>
        private bool IsStandardLaunchCommand(string argument)
        {
            bool bStandard = false;

            foreach (string strFlag in m_standardLaunchFlags)
            {
                if (IsFlag(argument, strFlag))
                {
                    bStandard = true;
                    break;
                }
            }

            return bStandard;
        }

        #endregion
    }
}
