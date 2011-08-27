using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;

namespace CustomErrorPage.Features.FileNotFoundFeature
{
    [Guid("18670ae5-8df4-4802-abb0-43e9ff20b5ba")]
    public class FileNotFoundFeatureEventReceiver : SPFeatureReceiver
    {
        private const string fileNotFoundPage = "Connolly404.html";
        private const string defaultPageProperty = "Connolly.DefaultErrorPage";

        //SET TO CUSTOM
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = properties.Feature.Parent as SPWebApplication;

            //GET DEFAULT
            string defaultErrorPage = webApp.FileNotFoundPage;
            if (!webApp.Properties.ContainsKey(defaultPageProperty))
            {
                webApp.Properties[defaultPageProperty] = defaultErrorPage;
                webApp.Update();
            }

            
            updateFileNotFoundPage(webApp, fileNotFoundPage);
        }

        //RESET TO DEFAULT
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = properties.Feature.Parent as SPWebApplication;
            string defaultpage = webApp.Properties[defaultPageProperty] as string;
            updateFileNotFoundPage(webApp, defaultpage);
        }

        //SET PROPERTY
        private void updateFileNotFoundPage(SPWebApplication webApp, string page)
        {
            try { webApp.FileNotFoundPage = page; webApp.Update(); }
            catch { throw new ApplicationException("Unable to set File Not Found Page"); }
        }
    }
}
