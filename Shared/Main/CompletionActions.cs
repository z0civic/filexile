using System.Windows.Forms;

namespace Shared
{
    class CompletionActions
    {
        /// <summary>
        /// Class for hodling the various completion actions that can be performed by FilExile
        /// </summary>
        // ------------------------------------------------------------------------------------

        #region Enums

        // The various completion actions available to the user
        public enum Actions { DO_NOTHING, PLAY_SOUND, RESTART, SHUTDOWN };

        #endregion

        // ------------------------------------------------------------------------------------

        /// <summary>
        /// Runs the passed completion action (taken from the Completion Action combo box)
        /// </summary>
        /// <param name="action"></param>
        #region Public methods
        public static void RunCompletionEvent(Actions action, bool bGUI, bool force)
        {
            switch (action)
            {
                case Actions.DO_NOTHING:
                    break;

                case Actions.PLAY_SOUND:
                    //Playing sounds is only available if the user is running the GUI        
                    if (bGUI)
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        MessageBox.Show(SharedResources.Properties.Resources.OpComplete, "FilExile", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    break;

                case Actions.RESTART:
                    WindowsOps.ExitWin(WindowsOps.EWX_REBOOT, force);
                    break;

                case Actions.SHUTDOWN:
                    WindowsOps.ExitWin(WindowsOps.EWX_SHUTDOWN, force);
                    break;

                default:
                    break;
            }
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        private static void Shutdown()
        {
            
        }

        #endregion
    }
}
