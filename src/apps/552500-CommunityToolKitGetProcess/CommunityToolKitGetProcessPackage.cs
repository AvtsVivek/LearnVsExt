global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using System.Runtime.InteropServices;
using System.Threading;

namespace CommunityToolKitGetProcess
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideToolWindow(typeof(MyToolWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.CommunityToolKitGetProcessString)]
    public sealed class CommunityToolKitGetProcessPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await this.RegisterCommandsAsync();

            this.RegisterToolWindows();

            var DteTwoInstance = await GetServiceAsync(typeof(DTE)) as DTE2;

            var DebuggerEventsInstance = DteTwoInstance.Events.DebuggerEvents;

            //DebuggerEventsInstance.OnContextChanged += DebuggerEventsInstance_OnContextChanged;

            //DebuggerEventsInstance.OnExceptionThrown += DebuggerEventsInstance_OnExceptionThrown;

            //DebuggerEventsInstance.OnExceptionNotHandled += DebuggerEventsInstance_OnExceptionNotHandled;

            DebuggerEventsInstance.OnEnterRunMode += DebuggerEventsInstance_OnEnterRunMode;
        }

        private void DebuggerEventsInstance_OnEnterRunMode(dbgEventReason Reason)
        {
            VS.MessageBox.Show("asdf");
        }
    }
}