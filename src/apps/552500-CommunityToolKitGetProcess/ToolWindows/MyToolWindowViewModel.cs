using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolKitGetProcess.Infra;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        private string _buildStatusDescription = BuildStatus.NoSolutionWithProjectsCurrentlyOpened.Description;

        public string BuildStatusDescription
        {
            get { return _buildStatusDescription; }
            set { 
                _buildStatusDescription = value; 
                OnPropertyChanged();
            }
        }

        public string BuildState
        {
            get { 

                ThreadHelper.ThrowIfNotOnUIThread();

                var buildStateFullString = DteTwoInstance.Solution.SolutionBuild.BuildState.ToString();
                var buildStateString = buildStateFullString.Remove(0, "vsBuildState".Length);
                return buildStateString;

            }
        }

        public DTE2 DteTwoInstance
        {
            get;
            private set;
        }

        private Dictionary<string, bool> _multipleProjectBuildStatus = new Dictionary<string, bool>();
        
        public MyToolWindowViewModel()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            ProcessMode = GlobalConsts.NoSolutionOpen;
            ProcessName = GlobalConsts.NoProcessRunning;
            ProcessPath = GlobalConsts.NoProcessRunning;

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


            //var debuggerEventsInstance = DteTwoInstance.Events.DebuggerEvents;
            //Assumes.Present(debuggerEventsInstance);
            //debuggerEventsInstance.OnContextChanged += DebuggerEventsInstance_OnContextChanged;
            //debuggerEventsInstance.OnExceptionThrown += DebuggerEventsInstance_OnExceptionThrown;
            //debuggerEventsInstance.OnExceptionNotHandled += DebuggerEventsInstance_OnExceptionNotHandled;
            //debuggerEventsInstance.OnEnterRunMode += DebuggerEventsInstance_OnEnterRunMode;
            //debuggerEventsInstance.OnEnterDesignMode += DebuggerEventsInstance_OnEnterDesignMode;
            //debuggerEventsInstance.OnEnterBreakMode += DebuggerEventsInstance_OnEnterBreakMode;

            CommunityToolKitGetProcessPackage.OnEnterRunMode += DebuggerEventsInstance_OnEnterRunMode;
            CommunityToolKitGetProcessPackage.OnEnterDesignMode += DebuggerEventsInstance_OnEnterDesignMode;
            CommunityToolKitGetProcessPackage.OnEnterBreakMode += DebuggerEventsInstance_OnEnterBreakMode;
            CommunityToolKitGetProcessPackage.OnContextChanged += DebuggerEventsInstance_OnContextChanged;
            CommunityToolKitGetProcessPackage.OnExceptionThrown += DebuggerEventsInstance_OnExceptionThrown;
            CommunityToolKitGetProcessPackage.OnExceptionNotHandled += DebuggerEventsInstance_OnExceptionNotHandled;

            SetSolutionWithProjectsStatus();
            // VS.MessageBox.Show($"From ctor: {IsSolutionWithProjectsOpenedInVs}");
        }

        private void SetSolutionWithProjectsStatus()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var solutionCount = DteTwoInstance.Solution.Count;
            var projectCountInSolution = DteTwoInstance.Solution.Projects.Count;

            if (solutionCount > 0 && projectCountInSolution > 0)
                IsSolutionWithProjectsOpenedInVs = true;
            else
                IsSolutionWithProjectsOpenedInVs = false;


            if (IsSolutionWithProjectsOpenedInVs)
            {
                BuildStatusDescription = BuildStatus.SolutionWithProjectsOpened_ButNoBuildEvenFiredYet.Description;
                ProcessMode = GlobalConsts.DesignMode;
            }
            else
            {
                BuildStatusDescription = BuildStatus.NoSolutionWithProjectsCurrentlyOpened.Description;
                ProcessMode = GlobalConsts.NoSolutionOpen;
            }

            OnPropertyChanged(nameof(BuildState));
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
            OnPropertyChanged(nameof(BuildState));
            BuildStatusDescription = BuildStatus.BuildProjectConfigDone.Description;
           
            _multipleProjectBuildStatus.Add(Project, Success);
            // VS.MessageBox.Show("On Build Project Config Done");
        }

        private void BuildEventsInstance_OnBuildProjectConfigBegin(string Project, string ProjectConfig, string Platform, string SolutionConfig)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            OnPropertyChanged(nameof(BuildState));
            BuildStatusDescription = BuildStatus.BuildProjectConfigBegin.Description;
            // VS.MessageBox.Show("On Build Project Config Begin");
        }

        private void BuildEventsInstance_OnBuildBegin(vsBuildScope Scope, vsBuildAction Action)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            OnPropertyChanged(nameof(BuildState));
            BuildStatusDescription = BuildStatus.BuildBegin.Description;
            _multipleProjectBuildStatus = new();
            // VS.MessageBox.Show("On Build Begin");
        }

        private void BuildEventsInstance_OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            OnPropertyChanged(nameof(BuildState));
            var _buildFailure = _multipleProjectBuildStatus.Values.Where(projectBuildStatus => projectBuildStatus == false).Count() > 0;
            if (_buildFailure)
                BuildStatusDescription = BuildStatus.BuildDoneWithFailure.Description;
            else
                BuildStatusDescription = BuildStatus.BuildDoneWithSuccess.Description;

                // VS.MessageBox.Show("On Build Done");
        }
        #endregion

        #region Debugger Events
        private void DebuggerEventsInstance_OnEnterBreakMode(dbgEventReason Reason, ref dbgExecutionAction ExecutionAction)
        {

            // VS.MessageBox.Show("On Enter Break Mode from VM");
            SetDebuggerProperties();
        }

        private void DebuggerEventsInstance_OnEnterDesignMode(dbgEventReason Reason)
        {
            // VS.MessageBox.Show("On Enter Design Mode from VM");
            SetDebuggerProperties();
        }

        private void DebuggerEventsInstance_OnEnterRunMode(dbgEventReason Reason)
        {
            // VS.MessageBox.Show("On Enter Run Mode From VM");
            SetDebuggerProperties();
        }

        private void DebuggerEventsInstance_OnExceptionNotHandled(string ExceptionType, string Name, int Code, string Description, ref dbgExceptionAction ExceptionAction)
        {
            // VS.MessageBox.Show("On Exception Not Handled from VM");
            SetDebuggerProperties();
        }

        private void DebuggerEventsInstance_OnExceptionThrown(string ExceptionType, string Name, int Code, string Description, ref dbgExceptionAction ExceptionAction)
        {
            // VS.MessageBox.Show("On Exception Thrown from VM");
            SetDebuggerProperties();
        }

        private void DebuggerEventsInstance_OnContextChanged(Process NewProcess, Program NewProgram, EnvDTE.Thread NewThread, StackFrame NewStackFrame)
        {
            // VS.MessageBox.Show("On Context Changed from VM");
            SetDebuggerProperties();
        }
        #endregion

        private void SetDebuggerProperties()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var currentModeStringAndRunningProcessTuple = DteTwoInstance.GetCurrentModeAndRunningProcess();
            ProcessMode = currentModeStringAndRunningProcessTuple.Item1;

            if (DteTwoInstance.Debugger.CurrentMode == dbgDebugMode.dbgRunMode
                || DteTwoInstance.Debugger.CurrentMode == dbgDebugMode.dbgBreakMode)
                IsProcessRunning = true;
            else
            {
                ProcessId = 0;
                ProcessName = GlobalConsts.NoProcessRunning;
                ProcessPath = GlobalConsts.NoProcessRunning;
                IsProcessRunning = false;
            }

            if (currentModeStringAndRunningProcessTuple.Item2 == null)
            {
                ProcessId = 0;
                ProcessName = GlobalConsts.NoProcessRunning;
                ProcessPath = GlobalConsts.NoProcessRunning;
                IsProcessBeingDebugged = false;
                return;
            }
            else
                IsProcessBeingDebugged = true;

            var programCount = currentModeStringAndRunningProcessTuple.Item2.Programs.Count; 
            
            if (programCount > 0)
            {
                IsProcessBeingDebugged = currentModeStringAndRunningProcessTuple.Item2.Programs.Item(1).IsBeingDebugged;
            }

            ProcessId = currentModeStringAndRunningProcessTuple.Item2.ProcessID;
            
            ProcessPath = currentModeStringAndRunningProcessTuple.Item2.Name;

            ProcessName = Path.GetFileName(ProcessPath);

            


            //var localProcesses = DteTwoInstance.Debugger.LocalProcesses;
            //var currentProcess = DteTwoInstance.Debugger.CurrentProcess;

            //var localProcessCount = localProcesses.Count;

            //var currentProcessId = currentProcess.ProcessID;

            //var currentProcessName = currentProcess.Name;


        }

        private bool _IsProcessRunning;

        public bool IsProcessRunning
        {
            get { return _IsProcessRunning; }
            set
            {

                _IsProcessRunning = value;
                OnPropertyChanged();
            }
        }

        private bool _IsProcessBeingDebugged;

        public bool IsProcessBeingDebugged
        {
            get { return _IsProcessBeingDebugged; }
            set { 
            
                _IsProcessBeingDebugged = value; 
                OnPropertyChanged();
            }
        }


        private int _processId;

        public int ProcessId
        {
            get { return _processId; }
            set { 
                _processId = value;
                OnPropertyChanged();
            }
        }

        private string _processName;

        public string ProcessName
        {
            get { return _processName; }
            set
            {
                _processName = value;
                OnPropertyChanged();
            }
        }

        private string _processPath;

        public string ProcessPath
        {
            get { return _processPath; }
            set
            {
                _processPath = value;
                OnPropertyChanged();
            }
        }

        private string _processMode;

        public string ProcessMode
        {
            get { return _processMode; }
            set
            {
                _processMode = value;
                OnPropertyChanged();
            }
        }

    }
}
