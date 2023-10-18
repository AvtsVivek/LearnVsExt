using EnvDTE;
using EnvDTE100;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace ActiveProcessWithToolWindow.Commands
{
    /// <summary>
    /// Interaction logic for AsyncToolWindowControl.
    /// </summary>
    public partial class AsyncToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncToolWindowControl"/> class.
        /// </summary>
        public AsyncToolWindowControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.VerifyAccess();
            //MessageBox.Show(
            //    string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
            //    "AsyncToolWindow");

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

            ///////////////////////////////////////////////////////////////////////


            var currentModeStringAndRunningProcessTuple = GetCurrentModeAndRunningProcess(dte2);

            if (currentModeStringAndRunningProcessTuple.Item2 == null)
            {
                MessageBox.Show($"No Process is currently running." + Environment.NewLine +
                    $"The current Mode is {currentModeStringAndRunningProcessTuple.Item1}", "ActiveProcess");
                return;
            }

            var currentProcessIsBeingDebugged = currentModeStringAndRunningProcessTuple.Item2.Programs.Item(1).IsBeingDebugged;

            MessageBox.Show($"Process Id is {currentModeStringAndRunningProcessTuple.Item2.ProcessID}. " + Environment.NewLine +
                $"And name is {currentModeStringAndRunningProcessTuple.Item2.Name}." + Environment.NewLine +
                $"The current Mode is {currentModeStringAndRunningProcessTuple.Item1}" + Environment.NewLine +
                (currentProcessIsBeingDebugged ? $"And this is being debugged" : "And this is NOT being debugged"), "ActiveProcess"
                );


            var runningProcess = currentModeStringAndRunningProcessTuple.Item2;

            var runningProcessDte = currentModeStringAndRunningProcessTuple.Item2.DTE;

        }

        private Tuple<string, Process> GetCurrentModeAndRunningProcess(DTE2 dte2)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var currentModeStringAndRunningProcessTuple = new Tuple<string, Process>(string.Empty, null);

            if (dte2 == null)
                return currentModeStringAndRunningProcessTuple;

            if (dte2.Debugger == null)
                return currentModeStringAndRunningProcessTuple;

            var currentModeString = string.Empty;
            switch (dte2.Debugger.CurrentMode)
            {
                case dbgDebugMode.dbgDesignMode:
                    currentModeString = "Design Mode";
                    break;
                case dbgDebugMode.dbgBreakMode:
                    currentModeString = "Break Mode";
                    break;
                case dbgDebugMode.dbgRunMode:
                    currentModeString = "Run Mode";
                    break;
            }

            currentModeStringAndRunningProcessTuple = new Tuple<string, Process>(currentModeString, null);

            var debugger5 = dte2.Debugger as Debugger5;
            if (debugger5 == null)
                return currentModeStringAndRunningProcessTuple;

            var processes = debugger5.DebuggedProcesses;
            if (processes == null)
                return currentModeStringAndRunningProcessTuple;

            if (processes.Count == 0)
                return currentModeStringAndRunningProcessTuple;

            var runningProcess = processes.Item(1);

            currentModeStringAndRunningProcessTuple = new Tuple<string, Process>(currentModeString, runningProcess);

            return currentModeStringAndRunningProcessTuple;
        }

    }
}