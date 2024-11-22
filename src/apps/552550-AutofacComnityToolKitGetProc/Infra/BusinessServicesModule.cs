using Autofac;
using AutofacComnityToolKitGetProc.Services;

namespace AutofacComnityToolKitGetProc.Infra
{
    public class BusinessServicesModule : Module
    {
        // What the hell is the following for?
        //private HttpClient CreateHttpClient()
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.ParseAdd(Constants.AcceptHeaderValue);
        //    client.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.UserAgentHeaderValue);
        //    return client;
        //}

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MyToolWindow>();
            builder.RegisterType<ToolWindowPane>();
            builder.RegisterType<MyToolWindowControl>();
            builder.RegisterType<MyToolWindowViewModel>().SingleInstance();
            builder.RegisterType<GreeterService>().As<IGreeterService>().SingleInstance();

            //builder.RegisterType<GistClientService>().As<IGistClientService>();
            //builder.RegisterType<WpfAuthenticationHandler>().As<IAuthenticationHandler>();

            //builder.RegisterType<WpfErrorHandler>().As<IErrorHandler>().SingleInstance();
            //builder.RegisterType<AsyncOperationStatusManager>().As<IAsyncOperationStatusManager>().SingleInstance();

            // builder.Register<HttpClient>(ctx => CreateHttpClient()).SingleInstance();
        }
    }
}
