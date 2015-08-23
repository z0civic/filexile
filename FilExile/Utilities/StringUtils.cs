using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;

namespace FilExile
{
    /// <summary>
    /// Contains a set of static utility methods for string manipulation
    /// </summary>
    [ReusableAttribute(ReusableCategory.Utility, "Methods used for localization and manipulation of string.")]
    public static class StringUtils
    {
        #region Public methods

        /// <summary>Returns the left X characters of a string</summary>
        /// <param name="str">The string</param>
        /// <param name="nCharacters">Number of characters</param>
        public static string Left(string str, int nCharacters)
        {
            return str.Substring(0, nCharacters);
        }

        /// <summary>Returns the right X characters of a string</summary>
        /// <param name="str">The string</param>
        /// <param name="nCharacters">Number of characters</param>
        public static string Right(string str, int nCharacters)
        {
            return str.Substring(str.Length - nCharacters);
        }

        /// <summary>If a string is too long, chop down and add a ...</summary>
        /// <param name="str">The string</param>
        /// <param name="nLength">The length</param>
        /// <returns>Either the original or ellipsed string</returns>
        public static string EllipseText(string str, int nLength)
        {
            if (nLength < str.Length)
            {
                if (nLength <= 3)
                    str = "...".Substring(0, nLength);
                else
                    str = str.Substring(0, nLength - 3) + "...";
            }

            return str;
        }

        /// <summary>Compares the right-most characters of the longest string with the other string</summary>
        /// <param name="str1">First string</param>
        /// <param name="str2">Second string</param>
        /// <param name="bIgnoreCase">If true, ignores case</param>
        /// <returns>True if equal</returns>
        public static bool CompareRight(string str1, string str2, bool bIgnoreCase)
        {
            bool bRetval;

            if (str1.Length >= str2.Length)
                bRetval = (string.Compare(Right(str1, str2.Length), str2, bIgnoreCase) == 0);
            else
                bRetval = (string.Compare(Right(str2, str1.Length), str1, bIgnoreCase) == 0);

            return bRetval;
        }

        /// <summary>Compares the left-most characters of the longest string with the other string</summary>
        /// <param name="str1">First string</param>
        /// <param name="str2">Second string</param>
        /// <param name="bIgnoreCase">If true, ignores case</param>
        /// <returns>True if equal</returns>
        public static bool CompareLeft(string str1, string str2, bool bIgnoreCase)
        {
            bool bRetval;

            if (str1.Length >= str2.Length)
                bRetval = (string.Compare(Left(str1, str2.Length), str2, bIgnoreCase) == 0);
            else
                bRetval = (string.Compare(Left(str2, str1.Length), str1, bIgnoreCase) == 0);

            return bRetval;
        }

        /// <summary>Removes carriage return and line feed characters from the passed string</summary>
        /// <param name="str">String</param>
        /// <returns>String without CRs or LFs</returns>
        public static string StripCrLfs(string str)
        {
            str = str.Replace("\r", "");
            str = str.Replace("\n", "");

            return str;
        }

        /// <summary>If the string is quoted, removes the quotes (single or double). If not, does nothing</summary>
        /// <param name="str">The string</param>
        /// <returns>The string without quotes</returns>
        public static string RemoveQuotes(string str)
        {
            if (str == null)
                str = "";

            str = str.Trim();

            if (str.Length > 1)
            {
                if (((str[0] == '"') && (str[str.Length - 1] == '"')) ||
                    ((str[0] == '\'') && (str[str.Length - 1] == '\'')))
                {
                    str = str.Substring(1, str.Length - 2);
                }

            }

            return str;
        }

        /// <summary>If the passed string is null, returns blank. Otherwise returns the string</summary>
        /// <param name="str"></param>
        /// <returns>A non-null string</returns>
        public static string NotNull(string str)
        {
            if (str == null)
                return "";

            return str;
        }

        /// <summary>Trims the blanks from the end of a string.  Also returns "" if the string was null.</summary>
        /// <param name="strToTrim">string to trim</param>
        /// <returns>trimmed string</returns>
        public static string Trim(string strToTrim)
        {
            if (!string.IsNullOrEmpty(strToTrim))
                return strToTrim.Trim();
            else
                return "";
        }

        /// <summary>
        /// Uses regex to determine if a passed string is similar to a pattern string
        /// </summary>
        /// <param name="str">String to check against pattern</param>
        /// <param name="pattern">A pattern string with wildcards</param>
        /// <returns>True if there's a match</returns>
        public static bool IsLike(string str, string pattern)
        {
            return new Regex("^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$", 
                RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(str);
        }

        #endregion
    }
}
