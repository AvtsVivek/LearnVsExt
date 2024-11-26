using EnvDTE;
using EnvDTE100;
using EnvDTE80;


namespace CommunityToolKitGetProcess
{
    public static class DteExtensions
    {
        public static Tuple<string, Process> GetCurrentModeAndRunningProcess(this DTE2 dte2)
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
