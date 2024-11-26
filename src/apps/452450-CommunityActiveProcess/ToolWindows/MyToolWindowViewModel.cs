using CommunityActiveProcess.MvvmInfra;
using EnvDTE;
using EnvDTE100;
using EnvDTE80;
using Microsoft;
using System.Windows.Input;
using CommunityActiveProcess.ToolWindows;

namespace CommunityActiveProcess
{
    public class MyToolWindowViewModel: BaseViewModel
    {
        public string Number { get; set; } = "One";
        
        private ICommand? _invokeButtonClick;

        public ICommand InvokeButtonClick
        {
            get
            {
                return _invokeButtonClick ??= new RelayCommand(InvokeButtonClickExecute, CanInvokeButtonClickExecute);
            }
        }

        private bool CanInvokeButtonClickExecute(object obj)
        {
            return true;
        }

        private async void InvokeButtonClickExecute(object obj)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE));

            var dte2 = dte as DTE2;

            ///////////////////////////////////////////////////////////////////////
            // The following currentProcess is always null.
            var currentProcess = dte2.Debugger.CurrentProcess;

            var debugger5 = dte2.Debugger as Debugger5;

            // The following currentProcess5 is always null.
            var currentProcess5 = debugger5.CurrentProcess;

            // The following currentProgram5 is always null.
            var currentProgram5 = debugger5.CurrentProgram;

            // The following vsDebugger object is not null, but it has only methods.
            // I am not aware how to use these methods. Need to find out.
            var vsDebugger = await VS.Services.GetDebuggerAsync();
            ///////////////////////////////////////////////////////////////////////


            var currentModeStringAndRunningProcessTuple = dte2.GetCurrentModeAndRunningProcess();

            if (currentModeStringAndRunningProcessTuple.Item2 == null)
            {
                VS.MessageBox.Show("ActiveProcess", $"No Process is currently running." + Environment.NewLine +
                    $"The current Mode is {currentModeStringAndRunningProcessTuple.Item1}");
                return;
            }

            var currentProcessIsBeingDebugged = currentModeStringAndRunningProcessTuple.Item2.Programs.Item(1).IsBeingDebugged;

            VS.MessageBox.Show("ActiveProcess", $"Process Id is {currentModeStringAndRunningProcessTuple.Item2.ProcessID}. " + Environment.NewLine +
                $"And name is {currentModeStringAndRunningProcessTuple.Item2.Name}." + Environment.NewLine +
                $"The current Mode is {currentModeStringAndRunningProcessTuple.Item1}" + Environment.NewLine +
                (currentProcessIsBeingDebugged ? $"And this is being debugged" : "And this is NOT being debugged")
                );


            var runningProcess = currentModeStringAndRunningProcessTuple.Item2;

            var runningProcessDte = currentModeStringAndRunningProcessTuple.Item2.DTE;

        }

    }
}