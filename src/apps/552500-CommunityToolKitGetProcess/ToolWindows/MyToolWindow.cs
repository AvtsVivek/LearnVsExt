using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
// using System.Windows;

namespace CommunityToolKitGetProcess
{
    public class MyToolWindow : BaseToolWindow<MyToolWindow>
    {
        public static DTE2 DteTwoInstance
        {
            get;
            private set;
        }

        public MyToolWindow()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            VS.MessageBox.Show("Tool Window activated");

            var dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE));

            var DteTwoInstance = dte as DTE2;

            Assumes.Present(DteTwoInstance);

            var SolutionEventsInstance = DteTwoInstance.Events.SolutionEvents;
            Assumes.Present(SolutionEventsInstance);
            SolutionEventsInstance.AfterClosing += SolutionEventsInstance_AfterClosing;
            SolutionEventsInstance.BeforeClosing += SolutionEventsInstance_BeforeClosing;
            SolutionEventsInstance.Opened += SolutionEventsInstance_Opened;
            SolutionEventsInstance.ProjectAdded += SolutionEventsInstance_ProjectAdded;
            SolutionEventsInstance.ProjectRemoved += SolutionEventsInstance_ProjectRemoved;
            SolutionEventsInstance.ProjectRenamed += SolutionEventsInstance_ProjectRenamed;
            SolutionEventsInstance.QueryCloseSolution += SolutionEventsInstance_QueryCloseSolution;
            SolutionEventsInstance.Renamed += SolutionEventsInstance_Renamed;


            var BuildEventsInstance = DteTwoInstance.Events.BuildEvents;
            Assumes.Present(BuildEventsInstance);
            BuildEventsInstance.OnBuildDone += BuildEventsInstance_OnBuildDone;
            BuildEventsInstance.OnBuildBegin += BuildEventsInstance_OnBuildBegin;
            BuildEventsInstance.OnBuildProjConfigBegin += BuildEventsInstance_OnBuildProjConfigBegin;
            BuildEventsInstance.OnBuildProjConfigDone += BuildEventsInstance_OnBuildProjConfigDone;


            var DebuggerEventsInstance = DteTwoInstance.Events.DebuggerEvents;
            Assumes.Present(DebuggerEventsInstance);
            DebuggerEventsInstance.OnContextChanged += DebuggerEventsInstance_OnContextChanged;
            DebuggerEventsInstance.OnExceptionThrown += DebuggerEventsInstance_OnExceptionThrown;
            DebuggerEventsInstance.OnExceptionNotHandled += DebuggerEventsInstance_OnExceptionNotHandled;
            DebuggerEventsInstance.OnEnterRunMode += DebuggerEventsInstance_OnEnterRunMode;
            DebuggerEventsInstance.OnEnterDesignMode += DebuggerEventsInstance_OnEnterDesignMode;
            DebuggerEventsInstance.OnEnterBreakMode += DebuggerEventsInstance_OnEnterBreakMode;

            // DteTwoInstance.Solution.Close();

        }
        public override string GetTitle(int toolWindowId) => "My Tool Window";

        public override Type PaneType => typeof(Pane);

        public override Task<System.Windows.FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            return Task.FromResult<System.Windows.FrameworkElement>(new MyToolWindowControl());
        }

        [Guid("26d9d120-54cd-48e3-aaa9-5872af860726")]
        internal class Pane : ToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }

        #region SolutionEvents
        private void SolutionEventsInstance_AfterClosing()
        {
            VS.MessageBox.Show("After Closing");
        }

        private void SolutionEventsInstance_BeforeClosing()
        {
            VS.MessageBox.Show("Before Closing");
        }

        private void SolutionEventsInstance_Opened()
        {
            VS.MessageBox.Show("Opened");
        }

        private void SolutionEventsInstance_ProjectAdded(EnvDTE.Project Project)
        {
            VS.MessageBox.Show("Project Added");
        }

        private void SolutionEventsInstance_ProjectRemoved(EnvDTE.Project Project)
        {
            VS.MessageBox.Show("Project Removed");
        }
        private void SolutionEventsInstance_ProjectRenamed(EnvDTE.Project Project, string OldName)
        {
            VS.MessageBox.Show("Project Renamed");
        }

        private void SolutionEventsInstance_QueryCloseSolution(ref bool fCancel)
        {
            VS.MessageBox.Show("Query Close Solution");
        }

        private void SolutionEventsInstance_Renamed(string OldName)
        {
            VS.MessageBox.Show("Renamed");
        }
        #endregion

        #region BuildEvents
        private void BuildEventsInstance_OnBuildProjConfigDone(string Project, string ProjectConfig, string Platform, string SolutionConfig, bool Success)
        {
            VS.MessageBox.Show("On Build Proj Config Done");
        }

        private void BuildEventsInstance_OnBuildProjConfigBegin(string Project, string ProjectConfig, string Platform, string SolutionConfig)
        {
            VS.MessageBox.Show("On Build Proj Config Begin");
        }

        private void BuildEventsInstance_OnBuildBegin(vsBuildScope Scope, vsBuildAction Action)
        {
            VS.MessageBox.Show("On Build Begin");
        }

        private void BuildEventsInstance_OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
        {
            VS.MessageBox.Show("On Build Done");
        }
        #endregion

        #region DebuggerEvents
        private void DebuggerEventsInstance_OnEnterBreakMode(dbgEventReason Reason, ref dbgExecutionAction ExecutionAction)
        {
            VS.MessageBox.Show("On Enter Break Mode ");
        }

        private void DebuggerEventsInstance_OnEnterDesignMode(dbgEventReason Reason)
        {
            VS.MessageBox.Show("On Enter Deisng Mode");
        }

        private void DebuggerEventsInstance_OnEnterRunMode(dbgEventReason Reason)
        {
            VS.MessageBox.Show("On Enter Run Mode");
        }

        private void DebuggerEventsInstance_OnExceptionNotHandled(string ExceptionType, string Name, int Code, string Description, ref dbgExceptionAction ExceptionAction)
        {
            VS.MessageBox.Show("On Exception Not Handled");
        }

        private void DebuggerEventsInstance_OnExceptionThrown(string ExceptionType, string Name, int Code, string Description, ref dbgExceptionAction ExceptionAction)
        {
            VS.MessageBox.Show("On Exception Thrown");
        }

        private void DebuggerEventsInstance_OnContextChanged(Process NewProcess, Program NewProgram, EnvDTE.Thread NewThread, StackFrame NewStackFrame)
        {
            VS.MessageBox.Show("On Context Changed");
        }
        #endregion
    }
}