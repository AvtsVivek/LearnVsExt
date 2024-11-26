using Autofac;
using AutofacComnityToolKitNotWorking.Services;

namespace AutofacComnityToolKitNotWorking.Infra
{
    public class BusinessServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MyToolWindow>();
            builder.RegisterType<ToolWindowPane>();
            builder.RegisterType<MyToolWindowControl>();
            builder.RegisterType<MyToolWindowViewModel>().SingleInstance();
            builder.RegisterType<GreeterService>().As<IGreeterService>().SingleInstance();
        }
    }
}
