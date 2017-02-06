using System.Text.RegularExpressions;

namespace Shared
{
    /// <summary>Contains a set of static utility methods for string manipulation</summary>
    internal static class StringUtils
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
        private static string Right(string str, int nCharacters)
        {
            return str.Substring(str.Length - nCharacters);
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