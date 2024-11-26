using Autofac;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutofacDIIntro.Infra
{
    public class AutofacEnabledAsyncPackage : AsyncPackage
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
                    Debugger.Break();
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw;
            }

        }

        protected override WindowPane InstantiateToolWindow(Type toolWindowType) => (WindowPane)GetService(toolWindowType);

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
