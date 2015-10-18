using System;
using System.Windows.Forms;

namespace Shared
{
    internal sealed class CompletionAction
    {
		/// <summary>
		/// Class for holding the completion action selected by the user that can be performed by FilExile
		/// </summary>
		// ------------------------------------------------------------------------------------

		#region Accessors

		public string Name { get; set; }
		public int Value { get; set; }

		#endregion

		// ------------------------------------------------------------------------------------

		#region Constructor

		public CompletionAction()
		{
		}

		#endregion

		// ------------------------------------------------------------------------------------

		/// <summary>
		/// Runs the passed completion action (taken from the Completion Action combo box or
		/// /end command line parameter)
		/// </summary>
		/// <param name="bGUI">Whether this was called from the GUI</param>
		/// <param name="bForce">Whether or not to force the action</param>
		#region Public methods
		public void Run(bool bGUI, bool bForce)
        {
            switch (Value)
            {
				case 0:
                    break;

                case 1:
                    //Playing sounds is only available if the user is running the GUI        
                    if (bGUI)
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        MessageBox.Show(SharedResources.Properties.Resources.OpComplete, "FilExile", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    break;

                case 2:
                    WindowsOps.ExitWin(WindowsOps.EWX_REBOOT, bForce);
                    break;

                case 3:
                    WindowsOps.ExitWin(WindowsOps.EWX_SHUTDOWN, bForce);
                    break;

                default:
                    break;
            }
        }

		/// <summary>
		/// Checks if the passed integer successfully maps as a completion action value
		/// </summary>
		/// <param name="iNum">Integer value</param>
		/// <returns>True if the integer maps to a valid completion action</returns>
		public bool IsValidOption(int iNum)
		{
			return (iNum >= 0 && iNum <= 3);
		}

		/// <summary>
		/// Checks if the passed integer successfully maps to the Completion Action enum. Does
		/// not consider '1' to be a valid option as sounds cannot be played from the CLI
		/// </summary>
		/// <param name="iNum">Integer value</param>
		/// <returns>True if the integer maps to the Completion Action enum for the CLI</returns>
		public bool IsValidOptionForCLI(int iNum)
		{
			if (iNum == 1)
				return false;
			else
				return IsValidOption(iNum);
		}

        #endregion
    }
}
