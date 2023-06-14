using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace ReadOptionsValues
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(ReadOptionsValuesPackage.PackageGuidString)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideOptionPage(typeof(OptionPageGrid), "My Category", "My Grid Page", 0, 0, true)]
    [ProvideOptionPage(typeof(OptionPageCustom), "My Category", "My Custom Page", 0, 0, true)]
    [ProvideOptionPage(typeof(ExternalSearchOptionPage), "My Category", "General", 1, 1, true, new string[] { "External Search Options" })]
    public sealed class ReadOptionsValuesPackage : AsyncPackage
    {
        public const string PackageGuidString = "27988516-9079-46fd-a438-b4b8729e3fe4";

        #region Package Members
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await ReadOptionsValues.Commands.ReadIntOptionCommand.InitializeAsync(this);
        }

        #endregion

        public int OptionInteger
        {
            get
            {
                var optionPageGrid = (OptionPageGrid)GetDialogPage(typeof(OptionPageGrid));
                return optionPageGrid.OptionInteger;
            }
        }

        public string OptionTextString
        {
            get
            {
                var optionPageCustom = (OptionPageCustom)GetDialogPage(typeof(OptionPageCustom));
                return optionPageCustom.OptionString;
            }
        }

        public string OptionUrlString
        {
            get
            {
                var externalSearchOptionPage = (ExternalSearchOptionPage)GetDialogPage(typeof(ExternalSearchOptionPage));
                return externalSearchOptionPage.OptionUrl;
            }
        }

        public bool UseVsBrowserValue
        {
            get
            {
                var externalSearchOptionPage = (ExternalSearchOptionPage)GetDialogPage(typeof(ExternalSearchOptionPage));
                return externalSearchOptionPage.UseVSBrowser;
            }
        }
    }
}
