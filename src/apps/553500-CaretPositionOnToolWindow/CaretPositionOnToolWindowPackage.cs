global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using CaretPositionOnToolWindow.Infra;
using System.Runtime.InteropServices;
using System.Threading;

namespace CaretPositionOnToolWindow
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.CaretPositionOnToolWindowString)]
    [ProvideToolWindow(typeof(ToolWindows.CaretPositionToolWindow), 
        Orientation = ToolWindowOrientation.Left, Style = VsDockStyle.Tabbed, 
        Window = EnvDTE.Constants.vsWindowKindServerExplorer)]
    public sealed class CaretPositionOnToolWindowPackage : AutofacEnabledAsyncPackage
    {
        public CaretPositionOnToolWindowPackage()
        {
            RegisterModule<BusinessServicesModule>();
        }
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await ToolWindows.CaretPositionToolWindowCommand.InitializeAsync(this);
            await base.InitializeAsync(cancellationToken, progress);
        }
    }
}