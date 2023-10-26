using CommunityToolkit.Mvvm.ComponentModel;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityToolKitGetProcess
{
    public partial class MyToolWindowViewModel: ObservableObject
    {
        // [ObservableProperty]
        private bool _isSolutionWithProjectsOpenedInVs;

        public bool IsSolutionWithProjectsOpenedInVs
        {
            get { return _isSolutionWithProjectsOpenedInVs; }
            set
            {
                _isSolutionWithProjectsOpenedInVs = value;
                OnPropertyChanged();
            }
        }

        private string _buildStatus = "Build not started";

        public string BuildStatus
        {
            get { return _buildStatus; }
            set 
            { 
                _buildStatus = value; 
                OnPropertyChanged();
            }
        }

        private string _buildState = "Build not started";

        public string BuildState
        {
            get { return _buildState; }
            set
            {
                _buildState = value;
                OnPropertyChanged();
            }
        }

        public static DTE2 DteTwoInstance
        {
            get;
            private set;
        }
        public MyToolWindowViewModel()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE));

            DteTwoInstance = dte as DTE2;

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
            BuildEventsInstance.OnBuildProjConfigBegin += BuildEventsInstance_OnBuildProjectConfigBegin;
            BuildEventsInstance.OnBuildProjConfigDone += BuildEventsInstance_OnBuildProjectConfigDone;


            var DebuggerEventsInstance = DteTwoInstance.Events.DebuggerEvents;
            Assumes.Present(DebuggerEventsInstance);
            DebuggerEventsInstance.OnContextChanged += DebuggerEventsInstance_OnContextChanged;
            DebuggerEventsInstance.OnExceptionThrown += DebuggerEventsInstance_OnExceptionThrown;
            DebuggerEventsInstance.OnExceptionNotHandled += DebuggerEventsInstance_OnExceptionNotHandled;
            DebuggerEventsInstance.OnEnterRunMode += DebuggerEventsInstance_OnEnterRunMode;
            DebuggerEventsInstance.OnEnterDesignMode += DebuggerEventsInstance_OnEnterDesignMode;
            DebuggerEventsInstance.OnEnterBreakMode += DebuggerEventsInstance_OnEnterBreakMode;

            SetSolutionWithProjectsStatus();
            VS.MessageBox.Show($"From ctor: {IsSolutionWithProjectsOpenedInVs}");
        }

        private void SetSolutionWithProjectsStatus()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var solutionCount = DteTwoInstance.Solution.Count;
            var projectCountInSolution = DteTwoInstance.Solution.Projects.Count;

            if (solutionCount == 1 && projectCountInSolution == 1)            
                IsSolutionWithProjectsOpenedInVs = true;
            
            else                          
                IsSolutionWithProjectsOpenedInVs = false;
            
            BuildState = DteTwoInstance.Solution.SolutionBuild.BuildState.ToString();
            //VS.MessageBox.Show($"Status: {IsSolutionWithProjectsOpenedInVs}");

        }

        #region Solution Events
        private void SolutionEventsInstance_AfterClosing()
        {
            SetSolutionWithProjectsStatus();
            //VS.MessageBox.Show($"After Closing: {IsSolutionWithProjectsOpenedInVs}");
        }

        private void SolutionEventsInstance_BeforeClosing()
        {
            SetSolutionWithProjectsStatus();
            //VS.MessageBox.Show($"Before Closing: {IsSolutionWithProjectsOpenedInVs}");
        }

        private void SolutionEventsInstance_Opened()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            SetSolutionWithProjectsStatus();
            //VS.MessageBox.Show($"Opened. The solution count is {DteTwoInstance.Solution.Count}, : {IsSolutionWithProjectsOpenedInVs}");
        }

        private void SolutionEventsInstance_ProjectAdded(EnvDTE.Project Project)
        {
            SetSolutionWithProjectsStatus();
            //VS.MessageBox.Show($"Project Added: {IsSolutionWithProjectsOpenedInVs}");
        }

        private void SolutionEventsInstance_ProjectRemoved(EnvDTE.Project Project)
        {
            SetSolutionWithProjectsStatus();
            //VS.MessageBox.Show("Project Removed: {IsSolutionWithProjectsOpenedInVs}");
        }
        private void SolutionEventsInstance_ProjectRenamed(EnvDTE.Project Project, string OldName)
        {
            SetSolutionWithProjectsStatus();
            //VS.MessageBox.Show($"Project Renamed: {IsSolutionWithProjectsOpenedInVs}");
        }

        private void SolutionEventsInstance_QueryCloseSolution(ref bool fCancel)
        {
            SetSolutionWithProjectsStatus();
            //VS.MessageBox.Show($"Query Close Solution: {IsSolutionWithProjectsOpenedInVs}");
        }

        private void SolutionEventsInstance_Renamed(string OldName)
        {
            SetSolutionWithProjectsStatus();
            //VS.MessageBox.Show($"Renamed: {IsSolutionWithProjectsOpenedInVs}");
        }
        #endregion

        #region Build Events
        private void BuildEventsInstance_OnBuildProjectConfigDone(string Project, string ProjectConfig, string Platform, string SolutionConfig, bool Success)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            BuildState = DteTwoInstance.Solution.SolutionBuild.BuildState.ToString();
            BuildStatus = "Build Project Config Done";
            //VS.MessageBox.Show("On Build Project Config Done");
        }

        private void BuildEventsInstance_OnBuildProjectConfigBegin(string Project, string ProjectConfig, string Platform, string SolutionConfig)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            BuildState = DteTwoInstance.Solution.SolutionBuild.BuildState.ToString();
            BuildStatus = "Build Project Config Begin";
            //VS.MessageBox.Show("On Build Project Config Begin");
        }

        private void BuildEventsInstance_OnBuildBegin(vsBuildScope Scope, vsBuildAction Action)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            BuildState = DteTwoInstance.Solution.SolutionBuild.BuildState.ToString();
            BuildStatus = "Build Begin";
            //VS.MessageBox.Show("On Build Begin");
        }

        private void BuildEventsInstance_OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            BuildState = DteTwoInstance.Solution.SolutionBuild.BuildState.ToString();
            BuildStatus = "Build Done";
            //VS.MessageBox.Show("On Build Done");
        }
        #endregion

        #region Debugger Events
        private void DebuggerEventsInstance_OnEnterBreakMode(dbgEventReason Reason, ref dbgExecutionAction ExecutionAction)
        {
            VS.MessageBox.Show("On Enter Break Mode ");
        }

        private void DebuggerEventsInstance_OnEnterDesignMode(dbgEventReason Reason)
        {
            VS.MessageBox.Show("On Enter Design Mode");
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
