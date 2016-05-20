using System.Reflection;
using System.Windows.Forms;

namespace Shared
{
	internal sealed partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            Text = $"About {AssemblyTitle}";
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = $"Version {AssemblyVersion}";
            labelCopyright.Text = AssemblyCopyright;
            labelGNU.Text = SharedResources.Properties.Resources.GNUTitle;
            textBoxDescription.Text = SharedResources.Properties.Resources.GNU;
        }

        #region Assembly Attribute Accessors

		private static string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
	            if (attributes.Length <= 0)
		            return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
	            var titleAttribute = (AssemblyTitleAttribute)attributes[0];
	            return titleAttribute.Title != "" ? titleAttribute.Title : System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

		private static string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

		private static string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

		private static string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        #endregion
    }
}
