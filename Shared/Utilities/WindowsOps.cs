using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Shared
{
    internal static class WindowsOps
    {
		/// <summary>Class for handling Windows shutdown/reboot options</summary>
		// ------------------------------------------------------------------------------------

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr
        phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string host, string name,
        ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
        ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool ExitWindowsEx(int flg, int rea);

	    private const int SePrivilegeEnabled = 0x00000002;
	    private const int TokenQuery = 0x00000008;
	    private const int TokenAdjustPrivileges = 0x00000020;
	    private const string SeShutdownName = "SeShutdownPrivilege";
        internal const int EwxShutdown = 0x00000001;
        internal const int EwxReboot = 0x00000002;
	    private const int EwxForce = 0x00000004;

		/// <summary>
		/// Statically available method for shutting down or rebooting Windows
		/// </summary>
		/// <param name="flg">What operation (e.x.: EWX_SHUTDOWN)</param>
		/// <param name="force">Whether to force the operation</param>
        public static void ExitWin(int flg, bool force)
        {
			TokPriv1Luid tp;
            var hproc = GetCurrentProcess();
            var htok = IntPtr.Zero;
            OpenProcessToken(hproc, TokenAdjustPrivileges | TokenQuery, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SePrivilegeEnabled;
            LookupPrivilegeValue(null, SeShutdownName, ref tp.Luid);
            AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            if (force)
                ExitWindowsEx(flg | EwxForce, 0);
            else
                ExitWindowsEx(flg, 0);
        }

		/// <summary>
		/// Verifies that Robocopy is present on the system.
		/// </summary>
		/// <returns>True, if Robocopy is present</returns>
	    public static bool VerifyRobocopy()
		{
			bool retVal = true;

			string winDir = Environment.ExpandEnvironmentVariables("%WINDIR%");
			string progDir = Environment.ExpandEnvironmentVariables("%PROGRAMFILES%");

			if (!File.Exists(winDir + "\\System32\\robocopy.exe") && !File.Exists(progDir + "\\Windows Resource Kits\\Tools\\robocopy.exe"))
			{
				retVal = false;
			}

			return retVal;
		}

		/// <summary>
		/// Returns whether or not the current operating system is XP or earlier
		/// </summary>
		/// <returns>True, if the OS version is XP or earlier</returns>
	    public static bool WindowsXpOrLower()
	    {
		    bool retVal = false;
		    OperatingSystem os = Environment.OSVersion;

			if (os.Version.Major < 6)
			{
				retVal = true;
			}

			return retVal;
	    }
    }
}