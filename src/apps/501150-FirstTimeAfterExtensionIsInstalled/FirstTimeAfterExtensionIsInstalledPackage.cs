using FirstTimeAfterExtensionIsInstalled.Commands;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace FirstTimeAfterExtensionIsInstalled
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideAutoLoad(PackageGuids.autoloadString, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideUIContextRule(PackageGuids.autoloadString, 
        name: "Auto Load",
        expression: "!HasLoadedTermName",
        termNames: new[] { "HasLoadedTermName" },
        termValues: new[] { "UserSettingsStoreQuery:FirstTimeAfterExtensionIsInstalled\\HasLoaded" })]
    public sealed class FirstTimeAfterExtensionIsInstalledPackage : AsyncPackage
    {
        /// <summary>
        /// FirstTimeAfterExtensionIsInstalledPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "6505ef2e-d410-4e1b-8648-4daebd364e6d";

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            

            var settingsManager = new ShellSettingsManager(this);
            var writableUserSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
            var readOnlyUserSettingsStore = settingsManager.GetReadOnlySettingsStore(SettingsScope.UserSettings);

            var hasLoadedPropertyExists = readOnlyUserSettingsStore.PropertyExists(@"FirstTimeAfterExtensionIsInstalled", "HasLoaded");

            if (!hasLoadedPropertyExists)
            {
                writableUserSettingsStore.CreateCollection("FirstTimeAfterExtensionIsInstalled");
                // writableUserSettingsStore.SetInt32("FirstTimeAfterExtensionIsInstalled", "VsRunCount", 1);
                writableUserSettingsStore.SetBoolean("FirstTimeAfterExtensionIsInstalled", "HasLoaded", true);

                var message = string.Format(CultureInfo.CurrentCulture, "Thanks for trying out. Good Day");
                var title = "Thanks!!";

                // Show a message box to prove we were here
                VsShellUtilities.ShowMessageBox(
                    this,
                    message,
                    title,
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }

            await SomeCommand.InitializeAsync(this);

        }

        #endregion
    }
}
