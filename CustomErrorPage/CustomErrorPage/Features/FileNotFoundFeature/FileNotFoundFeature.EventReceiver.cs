using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;

namespace CustomErrorPage.Features.FileNotFoundFeature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("18670ae5-8df4-4802-abb0-43e9ff20b5ba")]
    public class FileNotFoundFeatureEventReceiver : SPFeatureReceiver
    {
        private const string fileNotFoundPage = "Connolly404.html";

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = properties.Feature.Parent as SPWebApplication;
            updateFileNotFoundPage(webApp, fileNotFoundPage);
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = properties.Feature.Parent as SPWebApplication;
            updateFileNotFoundPage(webApp, string.Empty);
        }

        private void updateFileNotFoundPage(SPWebApplication webApp, string page)
        {
            try { webApp.FileNotFoundPage = page; webApp.Update(); }
            catch { throw new ApplicationException("Unable to set File Not Found Page"); }
        }
    }
}
