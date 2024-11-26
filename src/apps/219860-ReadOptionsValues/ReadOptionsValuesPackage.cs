using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace ReadOptionsValues
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(guid: PackageGuidString)]
    [InstalledProductRegistration(productName: "#110", productDetails: "#112", productId: "1.0", IconResourceID = 400)]
    [ProvideMenuResource(resourceID: "Menus.ctmenu", version: 1)]
    [ProvideOptionPage(pageType: typeof(OptionPageGrid), categoryName: "My Category", pageName: "My Grid Page", categoryResourceID: 0, pageNameResourceID: 0, supportsAutomation: true)]
    [ProvideOptionPage(pageType: typeof(OptionPageCustom), categoryName: "My Category", pageName: "My Custom Page", categoryResourceID: 0, pageNameResourceID: 0, supportsAutomation: true)]
    [ProvideOptionPage(pageType: typeof(ExternalSearchOptionPage), categoryName: "My Category", pageName: "General", categoryResourceID: 1, pageNameResourceID: 1, supportsAutomation: true, keywords: new string[] { "External Search Options" })]
    public sealed class ReadOptionsValuesPackage : AsyncPackage
    {
        public const string PackageGuidString = "27988516-9079-46fd-a438-b4b8729e3fe4";

        #region Package Members
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await Commands.ReadIntOptionCommand.InitializeAsync(package: this);
        }

        #endregion

        public int OptionInteger
        {
            get
            {
                var optionPageGrid = (OptionPageGrid)GetDialogPage(dialogPageType: typeof(OptionPageGrid));
                return optionPageGrid.OptionInteger;
            }
        }

        public string OptionTextString
        {
            get
            {
                var optionPageCustom = (OptionPageCustom)GetDialogPage(dialogPageType: typeof(OptionPageCustom));
                return optionPageCustom.OptionString;
            }
        }

        public string OptionUrlString
        {
            get
            {
                var externalSearchOptionPage = (ExternalSearchOptionPage)GetDialogPage(dialogPageType: typeof(ExternalSearchOptionPage));
                return externalSearchOptionPage.OptionUrl;
            }
        }

        public bool UseVsBrowserValue
        {
            get
            {
                var externalSearchOptionPage = (ExternalSearchOptionPage)GetDialogPage(dialogPageType: typeof(ExternalSearchOptionPage));
                return externalSearchOptionPage.UseVSBrowser;
            }
        }
    }
}
