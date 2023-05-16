using ButtonWithVisibilityConstraints.Commands;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace ButtonWithVisibilityConstraints
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
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideUIContextRule(PackageGuids.uiContextSupportedFilesString,
        name: "Supported Files",
        expression: "CSharp | VisualBasic",
        termNames: new[] { "CSharp", "VisualBasic" },
        termValues: new[] { "HierSingleSelectionName:.cs$", "HierSingleSelectionName:.vb$" })]
    // Note you can use the following as well. Here we are using ActiveEditorContentType instead of HierSingleSelectionName
    // The difference is the following. When you use HierSingleSelectionName, you must open a solution in visual studio and then 
    // open files from that solution. Only then the Visibility of the command works as expected. So the context is a file belonging to a solution.
    // On the other hand, with ActiveEditorContentType, you can open file from any arbitraty location as well. 
    // So use the following and see.
    // termValues: new[] { "ActiveEditorContentType:cs", "ActiveEditorContentType:vb" })]
    public sealed class ButtonWithVisibilityConstraintsPackage : AsyncPackage
    {
        /// <summary>
        /// ButtonWithVisibilityConstraintsPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "508ffd1a-37af-4445-be4b-b321feed260d";

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
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await ButtonWithVisibilityConstraints.Commands.CsVbFileVisibleCommand.InitializeAsync(this);
        }

        #endregion
    }
}
