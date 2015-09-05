using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace Shared
{
    /// <summary>
    /// Contains a set of static utility methods for network calls
    /// </summary>
    internal sealed class NetworkUtils
    {
        #region Constants

        //A remote string that tells FilExile what the current version is
        private const string VERSION_URL = "http://filexile.sourceforge.net/version.txt";
        //A remote string that tells FilExile where the latest version can be downloaded
        private const string DOWNLOAD_URL = "http://filexile.sourceforge.net/downloadURL.txt";

        #endregion

        // ------------------------------------------------------------------------------------

        #region Public methods

        /// <summary>
        /// Verifies a string and the opens the URL in the user's default browser
        /// </summary>
        /// <param name="url">The URL to launch</param>
        public static void LaunchURL(string url)
        {
            //Make sure the string isn't null or empty and looks like an actual URL
            if (!string.IsNullOrEmpty(url) && StringUtils.Equals(StringUtils.Left(url,4),"http"))
                System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// Initiates a version check to determine if the user is using the current version or not
        /// </summary>
        public static void InitiateVersionCheck(bool UserInitiated)
        {
            WebClient webClient = new WebClient();
            Uri filExileVersionSite = new Uri(VERSION_URL);
            try
            {
                if (UserInitiated)
                    webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(manualVersionCheckComplete);
                else
                    webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(automatedVersionCheckComplete);

                webClient.DownloadStringAsync(filExileVersionSite);
            }
            catch (WebException)
            {
                //TODO: Handle exception
            }
        }

        /// <summary>
        /// Tries to download the latest version by contacting SourceForge for the download URL
        /// and then launching it
        /// </summary>
        public static void DownloadLatestVersion()
        {
            LaunchURL(GetDownloadURL());
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>
        /// Retrieves the download URL from the SourceForge server
        /// </summary>
        /// <returns></returns>
        private static string GetDownloadURL()
        {
            try
            {
                WebClient webClient = new WebClient();
                Uri filExileDownloadSite = new Uri(DOWNLOAD_URL);
                Stream urlStream = webClient.OpenRead(filExileDownloadSite);
                StreamReader sr = new StreamReader(urlStream);
                string retVal = sr.ReadToEnd();
                urlStream.Close();
                return retVal;
            }
            catch (WebException)
            {
                //We weren't able to contact the server
                MessageBox.Show(SharedResources.Properties.Resources.ConnectionError,SharedResources.Properties.Resources.Error);
                return null;
            }
        }

        /// <summary>
        /// When a manual version check completes, either prompt the user for download or tell them they
        /// are using the latest version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void manualVersionCheckComplete(Object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (StringUtils.Equals(e.Result, Assembly.GetExecutingAssembly().GetName().Version.ToString()))
                    MessageBox.Show(SharedResources.Properties.Resources.LatestVersion);
                else
                {
                    DownloadDlg dl = new DownloadDlg();
                    dl.ShowDialog();
                }
            }
        }

        /// <summary>
        /// When an automated version check completes, only prompt the user for download. We don't want to
        /// annoy the user by always telling them they're using the latest version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void automatedVersionCheckComplete(Object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (!StringUtils.Equals(e.Result, Assembly.GetExecutingAssembly().GetName().Version.ToString()))
                {
                    DownloadDlg dl = new DownloadDlg();
                    dl.ShowDialog();
                }
            }
        }

        #endregion 
    }
}
