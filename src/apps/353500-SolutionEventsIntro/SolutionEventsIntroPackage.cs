using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using Task = System.Threading.Tasks.Task;

namespace SolutionEventsIntro
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
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class SolutionEventsIntroPackage : AsyncPackage
    {
        /// <summary>
        /// SolutionEventsIntroPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "9dca66b7-52d6-454c-86b0-1a3acba1b82d";

        public SolutionEvents SolutionEventsInstance { 
            [DispId(302)] 
            get;
            set;
        }

        public static DTE2 DteTwoInstance
        {
            get;
            private set;
        }

        public SolutionEventsIntroPackage()
        {          

        }


        private void SolutionEvents_AfterClosing()
        {
            MessageBox.Show("After Closing");
        }

        private void SolutionEvents_BeforeClosing()
        {
            MessageBox.Show("Before Closing");
        }

        private void SolutionEvents_Opened()
        {
            MessageBox.Show("Opened");
        }

        private void SolutionEvents_ProjectAdded(Project Project)
        {
            MessageBox.Show("Project Added");
        }

        private void SolutionEvents_ProjectRemoved(Project Project)
        {
            MessageBox.Show("Project Removed");
        }
        private void SolutionEvents_ProjectRenamed(Project Project, string OldName)
        {
            MessageBox.Show("Project Renamed");
        }

        private void SolutionEvents_QueryCloseSolution(ref bool fCancel)
        {
            MessageBox.Show("Query Close Solution");
        }

        private void SolutionEvents_Renamed(string OldName)
        {
            MessageBox.Show("Renamed");
        }

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

            // ThreadHelper.ThrowIfNotOnUIThread();

            DteTwoInstance = await GetServiceAsync(typeof(DTE)) as DTE2;

            SolutionEventsInstance = DteTwoInstance.Events.SolutionEvents;

            SolutionEventsInstance.AfterClosing += SolutionEvents_AfterClosing;
            SolutionEventsInstance.BeforeClosing += SolutionEvents_BeforeClosing;
            SolutionEventsInstance.Opened += SolutionEvents_Opened;
            SolutionEventsInstance.ProjectAdded += SolutionEvents_ProjectAdded;
            SolutionEventsInstance.ProjectRemoved += SolutionEvents_ProjectRemoved;
            SolutionEventsInstance.ProjectRenamed += SolutionEvents_ProjectRenamed;
            SolutionEventsInstance.QueryCloseSolution += SolutionEvents_QueryCloseSolution;
            SolutionEventsInstance.Renamed += SolutionEvents_Renamed;
        }

        #endregion
    }
}
