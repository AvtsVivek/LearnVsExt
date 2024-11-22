using Autofac;
using System.Threading;

namespace AutofacComnityToolKitNotWorking.Infra
{
    public class AutofacEnabledAsyncPackage : ToolkitPackage
    {
        private IContainer container;
        private readonly ContainerBuilder containerBuilder;
        public AutofacEnabledAsyncPackage()
        {
            containerBuilder = new ContainerBuilder();
        }

        public void RegisterModule<TModule>() where TModule : Autofac.Core.IModule, new()
        {
            containerBuilder.RegisterModule<TModule>();
        }

        protected override Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            container = containerBuilder.Build();
            return base.InitializeAsync(cancellationToken, progress);
        }

        protected override object GetService(Type serviceType)
        {
            try
            {
                if (container?.IsRegistered(serviceType) ?? false)
                {
                    return container.Resolve(serviceType);
                }
                var service = base.GetService(serviceType);
                if (service != null)
                {
                    return service;
                }
                else
                {
                    // The service is not available. 
                    System.Diagnostics.Debugger.Break();
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw;
            }

        }

        protected override WindowPane InstantiateToolWindow(Type toolWindowType)
        {
            var windowPaneService = (WindowPane)GetService(toolWindowType);

            return windowPaneService;
        }


        protected override void Dispose(bool disposing)
        {
            try
            {
                container?.Dispose();
            }
            catch { }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }
}
