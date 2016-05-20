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
    internal static class NetworkUtils
    {
        // ------------------------------------------------------------------------------------

        #region Public methods

        /// <summary>
        /// Verifies a string and the opens the URL in the user's default browser
        /// </summary>
        /// <param name="url">The URL to launch</param>
        public static void LaunchUrl(string url)
        {
            //Make sure the string isn't null or empty and looks like an actual URL
            if (!string.IsNullOrEmpty(url) && Equals(StringUtils.Left(url,4),"http"))
                System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// Initiates a version check to determine if the user is using the current version or not
        /// </summary>
        public static void InitiateVersionCheck(bool bUserInitiated)
        {
            var webClient = new WebClient();
            var filExileVersionSite = new Uri(CommonStrings.VersionUrl);
            try
            {
                if (bUserInitiated)
                    webClient.DownloadStringCompleted += ManualVersionCheckComplete;
                else
                    webClient.DownloadStringCompleted += AutomatedVersionCheckComplete;

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
            LaunchUrl(GetDownloadUrl());
        }

        #endregion

        // ------------------------------------------------------------------------------------

        #region Private methods

        /// <summary>
        /// Retrieves the download URL from the SourceForge server
        /// </summary>
        /// <returns></returns>
        private static string GetDownloadUrl()
        {
            try
            {
	            var retVal = string.Empty;
                var webClient = new WebClient();
                var filExileDownloadSite = new Uri(CommonStrings.DownloadUrl);
                var urlStream = webClient.OpenRead(filExileDownloadSite);
	            if (urlStream != null)
	            {
					var sr = new StreamReader(urlStream);
					retVal = sr.ReadToEnd();
					urlStream.Close();
				}

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
        private static void ManualVersionCheckComplete(object sender, DownloadStringCompletedEventArgs e)
        {
	        if (e.Error != null) return;
	        if (Equals(e.Result, Assembly.GetExecutingAssembly().GetName().Version.ToString()))
		        MessageBox.Show(SharedResources.Properties.Resources.LatestVersion);
	        else
	        {
		        var dl = new DownloadDlg();
		        dl.ShowDialog();
	        }
        }

	    /// <summary>
        /// When an automated version check completes, only prompt the user for download. We don't want to
        /// annoy the user by always telling them they're using the latest version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AutomatedVersionCheckComplete(object sender, DownloadStringCompletedEventArgs e)
        {
		    if (e.Error != null) return;
		    if (Equals(e.Result, Assembly.GetExecutingAssembly().GetName().Version.ToString())) return;
		    var dl = new DownloadDlg();
		    dl.ShowDialog();
        }

        #endregion 
    }
}
