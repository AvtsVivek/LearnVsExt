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
    public delegate void OnEnterRunModeHandler(dbgEventReason Reason);
    public delegate void OnEnterDesignModeHandler(dbgEventReason Reason);
    public delegate void OnEnterBreakModeHandler(dbgEventReason Reason, ref dbgExecutionAction ExecutionAction);
    public delegate void OnContextChangedHandler(Process NewProcess, Program NewProgram, EnvDTE.Thread NewThread, StackFrame NewStackFrame);
    public delegate void OnExceptionThrownHandler(string ExceptionType, string Name, int Code, string Description, ref dbgExceptionAction ExceptionAction);
    public delegate void OnExceptionNotHandledEventHandler(string ExceptionType, string Name, int Code, string Description, ref dbgExceptionAction ExceptionAction);

    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideToolWindow(typeof(MyToolWindow.Pane), Orientation = ToolWindowOrientation.Right, Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.CommunityToolKitGetProcessString)]
    //[ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    //[ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class CommunityToolKitGetProcessPackage : ToolkitPackage
    {
        public static event OnEnterRunModeHandler OnEnterRunMode;
        public static event OnEnterDesignModeHandler OnEnterDesignMode;
        public static event OnEnterBreakModeHandler OnEnterBreakMode;
        public static event OnContextChangedHandler OnContextChanged;
        public static event OnExceptionThrownHandler OnExceptionThrown;
        public static event OnExceptionNotHandledEventHandler OnExceptionNotHandled;
        public EnvDTE.DebuggerEvents DebuggerEventsInstance
        {
            [DispId(302)]
            get;
            set;
        }

        public DTE2 DteTwoInstance
        {
            get;
            private set;
        }

        public CommunityToolKitGetProcessPackage()
        {
            
        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await this.RegisterCommandsAsync();

            this.RegisterToolWindows();

            DteTwoInstance = await GetServiceAsync(typeof(DTE)) as DTE2;

            DebuggerEventsInstance = DteTwoInstance.Events.DebuggerEvents;

            DebuggerEventsInstance.OnContextChanged += DebuggerEventsInstance_OnContextChanged;

            DebuggerEventsInstance.OnExceptionThrown += DebuggerEventsInstance_OnExceptionThrown;

            DebuggerEventsInstance.OnExceptionNotHandled += DebuggerEventsInstance_OnExceptionNotHandled;

            DebuggerEventsInstance.OnEnterRunMode += DebuggerEventsInstance_OnEnterRunMode;

            DebuggerEventsInstance.OnEnterDesignMode += DebuggerEventsInstance_OnEnterDesignMode;

            DebuggerEventsInstance.OnEnterBreakMode += DebuggerEventsInstance_OnEnterBreakMode;
        }

        private void DebuggerEventsInstance_OnEnterBreakMode(dbgEventReason Reason, ref dbgExecutionAction ExecutionAction)
        {
            // VS.MessageBox.Show("On Enter Break Mode from package ");
            OnEnterBreakMode(Reason, ref ExecutionAction);
        }

        private void DebuggerEventsInstance_OnEnterDesignMode(dbgEventReason Reason)
        {
            // VS.MessageBox.Show("On Enter Design Mode from package");
            OnEnterDesignMode(Reason);
        }

        private void DebuggerEventsInstance_OnEnterRunMode(dbgEventReason Reason)
        {
            // VS.MessageBox.Show("On Enter Run Mode from package");
            OnEnterRunMode(Reason);
        }

        private void DebuggerEventsInstance_OnExceptionNotHandled(string ExceptionType, string Name, int Code, string Description, ref dbgExceptionAction ExceptionAction)
        {
            // VS.MessageBox.Show("On Exception Not Handled from package");
            OnExceptionNotHandled(ExceptionType, Name, Code, Description, ref ExceptionAction);
        }

        private void DebuggerEventsInstance_OnExceptionThrown(string ExceptionType, string Name, int Code, string Description, ref dbgExceptionAction ExceptionAction)
        {
            // VS.MessageBox.Show("On Exception Thrown from package");
            OnExceptionThrown(ExceptionType, Name, Code, Description, ref ExceptionAction);
        }

        private void DebuggerEventsInstance_OnContextChanged(Process NewProcess, Program NewProgram, EnvDTE.Thread NewThread, StackFrame NewStackFrame)
        {
            // VS.MessageBox.Show("On Context Changed from package");
            OnContextChanged(NewProcess, NewProgram, NewThread, NewStackFrame);
        }
    }
}